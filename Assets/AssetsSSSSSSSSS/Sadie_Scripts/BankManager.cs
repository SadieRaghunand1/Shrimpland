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

    //Tis is a new comment
    private GameManager gameManager;
    [SerializeField] private FacilityManager facilityManager;
    public float currentBalance;
    public float risk; //Max at 100, min 0
    private float goal = 100000000f; //1 krillion, when currentBalance hits this, win; if hist 0, lose

    public float increaseRateUtilities;

    [Header("People")]
    public float attendees;
    public float ticketPrice;
    public float employees;
    public float salary;
    public float increaseRatePeople;

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


    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        gameManager = FindAnyObjectByType<GameManager>();

        totalBalanceTx.text = "$" + currentBalance.ToString();
        riskTx.text = risk.ToString() + "%";
        StartCoroutine(WaitToPayEmployees());
        //StartCoroutine(WaitToChargeTickets());
        StartCoroutine(IncreaseEmployeesAndAttendees());


        StartCoroutine(BreakFacilities());

    }

    #region Increase/Decrease variables
    public void DecreaseBalance(float _cost)
    {
        currentBalance -= _cost;
        totalBalanceTx.text = "$" + currentBalance.ToString(); //When in other scenes this kind of thing does not work, maybe just make map and stuff open over bank sheet, all one scene?
    }

    public void IncreaseBalance(float _gain)
    {

        currentBalance += _gain;
        totalBalanceTx.text = "$" + currentBalance.ToString();
    }


    public void CheckBalance()
    {
        if (currentBalance >= goal)
        {
            //Win
            Debug.Log("Win");
        }
        else if(currentBalance <= 0)
        {
            //Lose
            Debug.Log("Lose");
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


    #region Facilities

    public void ChangeFacilityStatus()
    {
        Debug.Log("Facility status changed");
        
        int _plumbingChance;
        int _foodChance;
        int _electricalChance;
        int _janitorialChance;

        //Use facility enum + risk to determine how often this breaks

        //Plumbing:
        if (plumbing == FacilitiesStatus.UPGRADED)
        {
            _plumbingChance = Random.Range(0, 200);
        }
        else
        {
            _plumbingChance = Random.Range(0, 100);

        }
       
        if(_plumbingChance <= risk)
        {
            int _num = Random.Range(1, 2);

            for(int i = 0; i <= _num; i++)
            {
                int _index = Random.Range(0, 6);
                facilityManager.facilityDatas[0].statuses[_index] = false;
                IncreaseRisk(1);
            }
        }



        //Food
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


        //Electrical
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


        //Janitorial
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

        StartCoroutine(BreakFacilities());

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
        yield return new WaitForSeconds(increaseRatePeople); //Increase rate can change based on how good the park is os there is more attendees, or ca be flat
        if(attendees % 20 == 0)
        {
            ChangeEmployeesAndAttendees(1, 5);
            StartCoroutine(WaitToChargeTickets());
        }
        else
        {
            ChangeEmployeesAndAttendees(0, 5);
        }

        StartCoroutine(IncreaseEmployeesAndAttendees());

    }

    IEnumerator BreakFacilities()
    {
        yield return new WaitForSeconds(increaseRateUtilities);
        ChangeFacilityStatus();
    }

    #endregion
} //END BankManager.cs
