using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankManager : MonoBehaviour
{
    public enum FacilitiesStatus
    {
        UPGRADED,
        NORMAL,
        BROKEN
    };

    public DialogueManager dialogueManager;
    private GameManager gameManager;
    [SerializeField] private FacilityManager facilityManager;
    public float currentBalance;
    public float risk; //Max at 100, min 0
    private float goal = 100000000f; //1 krillion, when currentBalance hits this, win; if hist 0, lose

    public float increaseRateUtilities;
    public float increaseRateRides;

    public int numItemsBought;

    [Header("Costs Facilities/Rides")]
    public float costOfFood;
    public float costOfRide;

    [Header("People")]
    public float attendees;
    public float ticketPrice;
    public float employees;
    public float salary;
    public float increaseRatePeople;
    public float subtractRatePeople;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI totalBalanceTx;
    [SerializeField] private TextMeshProUGUI riskTx;
    [SerializeField] Canvas storeCanvas;

    [SerializeField] private TextMeshProUGUI ticketText;
    [SerializeField] private TextMeshProUGUI attractionText;
    [SerializeField] private TextMeshProUGUI facilityText;
    [SerializeField] private TextMeshProUGUI maintenanceText;
    [SerializeField] private TextMeshProUGUI salaryText;
    [SerializeField] private TextMeshProUGUI legalText;
    [SerializeField] private TextMeshProUGUI assetText;
    [SerializeField] private TextMeshProUGUI liabilityText;


    [Header("Facilities")]
    public FacilitiesStatus plumbing;
    public FacilitiesStatus food;
    public FacilitiesStatus electrical;
    public FacilitiesStatus janitorial;


    [Header("Totals")]
    public float ticketGains;
    public float attractionGains;
    public float facilitiesGains;
    public float maintenanceLosses;
    public float salaryLosses;
    public float legalLosses;
    public float assetTotals;
    public float liabilityTotals;

    [Header("Animators")]
    public  Animator ticketAnim;
    public Animator attractionAnim;
    public Animator facilityAnim;
    public Animator maintenanceAnim;
    public Animator salaryAnim;
    public Animator legalAnim;

    [Header("Microgames")]
    [SerializeField] private MicroGameBaseManager[] microgames;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gameManager = FindAnyObjectByType<GameManager>();

        totalBalanceTx.text = "$" + currentBalance.ToString();
        riskTx.text = risk.ToString() + "%";
        StartCoroutine(WaitToPayEmployees());
        //StartCoroutine(WaitToChargeTickets());
        StartCoroutine(IncreaseEmployeesAndAttendees());
        StartCoroutine(ChargeForFood());

        StartCoroutine(BreakFacilities());
        StartCoroutine(BreakRides());

    }

    #region Increase/Decrease variables
   
    void CountNumBought()
    {
        numItemsBought = 0;
        for(int i = 0; i < microgames.Length; i++)
        {
            if(microgames[i].bought)
            {
                numItemsBought++;
            }
        }

        for(int i = 0; i < facilityManager.bought.Length; i++)
        {
            if(facilityManager.bought[i] == true)
            {
                numItemsBought++;
            }
        }
    }
    
    public void DecreaseBalance(float _cost)
    {
        currentBalance -= _cost;
        totalBalanceTx.text = "$" + currentBalance.ToString(); //When in other scenes this kind of thing does not work, maybe just make map and stuff open over bank sheet, all one scene?
        CheckBalance();
    }

    public void IncreaseBalance(float _gain)
    {

        currentBalance += _gain;
        totalBalanceTx.text = "$" + currentBalance.ToString();
        CheckBalance();
    }


    public void CheckBalance()
    {
        if (currentBalance >= goal)
        {
            //Win
            dialogueManager.ReplaceText(dialogueManager.win);
        }
        else if(currentBalance <= 0)
        {
            //Lose
            dialogueManager.ReplaceText(dialogueManager.lose);
        }
    }

    public void DecreaseRisk(float _amount)
    {
        if(risk > 0)
        risk -= _amount;
        riskTx.text = risk.ToString() + "%";
    }

 
    public void IncreaseRisk(float _increase)
    {
        if(risk < 100)
        risk += _increase;
        riskTx.text = risk.ToString() + "%";
    }

    //BigFuntionBigDeal
    public void ChangeBankStatement()
    {
        //Update text when any of these variables change, animations and color changes should not be done here, instead done where the variable value gets chabged
        ticketText.text = "$" + ticketGains.ToString();
        attractionText.text = "$" + attractionGains.ToString();
        facilityText.text = "$" + facilitiesGains.ToString();
        maintenanceText.text = "$" + maintenanceLosses.ToString();
        salaryText.text = "$" + salaryLosses.ToString();
        legalText.text = "$" + legalLosses.ToString();

        assetTotals = ticketGains + attractionGains + facilitiesGains;
        liabilityTotals = maintenanceLosses + salaryLosses + legalLosses;

        assetText.text = "$" + assetTotals;
        liabilityText.text = "$" + liabilityTotals;

    }


    public void ChangeEmployeesAndAttendees(int _employeeChange, int _attendeesChange)
    {
        //both increase depending on how many things are going well in park, so depending on how many things are bought, repaired, maybe make a rapport variable, how public views park?
        //Or every time a thing is bought or fixed calls this?
        //Should risk change/increase w this?


        Debug.Log("Change emloyees");
        employees += _employeeChange;
        attendees += _attendeesChange;

        TicketSales(_attendeesChange);
        IncreaseRisk(2);
    }
    #endregion

    #region UI
    public void OpenCloseStore()
    {
        if (!storeCanvas.enabled)
        {
            storeCanvas.enabled = true;
        }
        else if (storeCanvas.enabled) 
        {
            storeCanvas.enabled = false;
        }
        
    }

    #endregion


    #region Employees and Attendees methods

    void PayEmployees()
    {
        float _temp = salaryLosses;
        //Recurrs every set number of seconds, salary * employees, uses decrease
        DecreaseBalance(employees * salary);
        salaryLosses -= (employees * salary);
        ChangeBankStatement();

        if(salaryLosses != _temp)
        {
            salaryAnim.SetTrigger("LoseMoney");
        }
        
        StartCoroutine(WaitToPayEmployees());
    }

    void TicketSales(int _attendeeChange)
    {
        float _temp = ticketGains;
        //This does not change all in one go like employee salaries, instead more attendees there are the more often they are charged ticket price
        IncreaseBalance(ticketPrice * _attendeeChange);
        ticketGains += ticketPrice;
        ChangeBankStatement();

        if(ticketGains != _temp)
        {
            ticketAnim.SetTrigger("GetMoney");
        }
        
        StartCoroutine(WaitToChargeTickets());
    }
    #endregion


    #region Breaking Things

    public void ChangeFacilityStatus()
    {
        Debug.Log("Facility status changed");
        
        int _plumbingChance;
        int _foodChance;
        int _electricalChance;
        int _janitorialChance;

        //Use facility enum + risk to determine how often this breaks

        //Plumbing:
        if(facilityManager.bought[0] == true)
        {
            if (plumbing == FacilitiesStatus.UPGRADED)
            {
                _plumbingChance = Random.Range(0, 200);
            }
            else
            {
                _plumbingChance = Random.Range(0, 100);

            }

            if (_plumbingChance <= risk)
            {
                int _num = Random.Range(1, 2);

                for (int i = 0; i <= _num; i++)
                {
                    int _index = Random.Range(0, 6);
                    facilityManager.facilityDatas[0].statuses[_index] = false;
                    IncreaseRisk(1);
                }
            }

        }



        //Food
        if(facilityManager.bought[1] == true)
        {
            if (food == FacilitiesStatus.UPGRADED)
            {
                _foodChance = Random.Range(0, 200);
            }
            else
            {
                _foodChance = Random.Range(0, 100);

            }

            if (_foodChance <= risk)
            {
                int _num2 = Random.Range(1, 2);

                for (int i = 0; i <= _num2; i++)
                {
                    int _index2 = Random.Range(0, 6);
                    facilityManager.facilityDatas[1].statuses[_index2] = false;
                    IncreaseRisk(1);
                }
            }

        }


        //Electrical
        if(facilityManager.bought[2] == true)
        {
            if (electrical == FacilitiesStatus.UPGRADED)
            {
                _electricalChance = Random.Range(0, 200);
            }
            else
            {
                _electricalChance = Random.Range(0, 100);

            }

            if (_electricalChance <= risk)
            {
                int _num3 = Random.Range(1, 2);

                for (int i = 0; i <= _num3; i++)
                {
                    int _index3 = Random.Range(0, 6);
                    facilityManager.facilityDatas[2].statuses[_index3] = false;
                    IncreaseRisk(1);
                }
            }
        }
        


        //Janitorial
        if(facilityManager.bought[3] == true)
        {
            if (janitorial == FacilitiesStatus.UPGRADED)
            {
                _janitorialChance = Random.Range(0, 200);
            }
            else
            {
                _janitorialChance = Random.Range(0, 100);

            }

            if (_janitorialChance <= risk)
            {
                int _num4 = Random.Range(1, 2);

                for (int i = 0; i <= _num4; i++)
                {
                    int _index4 = Random.Range(0, 6);
                    facilityManager.facilityDatas[3].statuses[_index4] = false;
                    IncreaseRisk(1);
                }
            }
        }
        

        StartCoroutine(BreakFacilities());

    }

    public void ChangeRideBreakage()
    {
        for(int i = 0; i < microgames.Length; i++)
        {
            if(microgames[i].bought && !microgames[i].isBroken)
            {
                int _chance = Random.Range(0, 100);
                if(_chance <= risk)
                {
                    microgames[i].isBroken = true;
                    IncreaseRisk(1);
                }
            }
        }

        StartCoroutine(BreakRides());
    }

    #endregion


    #region enumerators
    IEnumerator WaitToPayEmployees()
    {
        Debug.Log("Wait to pay");
        yield return new WaitForSeconds(2f); //change based on gameplay

        PayEmployees();
        
    }

    IEnumerator WaitToChargeTickets()
    {
        Debug.Log("Wait to charge");
        yield return new WaitForSeconds(0); //Subject to change

        //TicketSales();
    }


    IEnumerator IncreaseEmployeesAndAttendees()
    {
        CountNumBought();
        if(numItemsBought != 0)
        {
            increaseRatePeople = subtractRatePeople - (attendees + numItemsBought);
        }
        

        yield return new WaitForSeconds(increaseRatePeople); //Increase rate can change based on how good the park is os there is more attendees, or ca be flat
        
        if(numItemsBought != 0)
        {
            if (attendees % 20 == 0)
            {
                ChangeEmployeesAndAttendees(1, 5);
                StartCoroutine(WaitToChargeTickets());
            }
            else
            {
                ChangeEmployeesAndAttendees(0, 5);
            }

        }

        StartCoroutine(IncreaseEmployeesAndAttendees());

    }

    IEnumerator BreakFacilities()
    {
        yield return new WaitForSeconds(increaseRateUtilities);
        ChangeFacilityStatus();
    }

    IEnumerator BreakRides()
    {
        yield return new WaitForSeconds(increaseRateRides);
        ChangeRideBreakage();
    }


    IEnumerator ChargeForFood()
    {
        Debug.Log("In charge for food");
        yield return new WaitForSeconds(4);

        //Charge for food
        if(facilityManager.bought[1])
        {
            for (int i = 0; i < facilityManager.facilityDatas[1].statuses.Length; i++)
            {
                if (facilityManager.facilityDatas[1].statuses[i] == false)
                {
                    break;
                }
                else
                {
                    if(i == facilityManager.facilityDatas[1].statuses.Length - 1)
                    {
                        float _temp = facilitiesGains;
                        Debug.Log("Successfully charged for food");
                        IncreaseBalance(costOfFood * (attendees / 2));
                        facilitiesGains += costOfFood * (attendees / 2);
                        if (facilitiesGains!= _temp)
                        {
                            facilityAnim.SetTrigger("GetMoney");
                        }
                        
                        
                    }
                    continue;
                }
            }
        }

        StartCoroutine(ChargeForFood());     
        
    }

    #endregion
} //END BankManager.cs
