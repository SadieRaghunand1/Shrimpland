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

    public float currentBalance;
    public float risk; //Max at 100, min 0
    private float goal = 100000000f; //1 krillion, when currentBalance hits this, win; if hist 0, lose

    [Header("People")]
    public float attendees;
    public float ticketPrice;
    public float employees;
    public float salary;
    public float increaseRate;

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


    private void Start()
    {
        totalBalanceTx.text = "$" + currentBalance.ToString();
        riskTx.text = risk.ToString() + "%";
        StartCoroutine(WaitToPayEmployees());
        //StartCoroutine(WaitToChargeTickets());
        StartCoroutine(IncreaseEmployeesAndAttendees());
    }

    #region Increase/Decrease variables
    public void DecreaseBalance(float _cost)
    {
        currentBalance -= _cost;
        totalBalanceTx.text = "$" + currentBalance.ToString();
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
        //Recurrs every set number of seconds, salary * employees, uses decrease
        DecreaseBalance(employees * salary);
        salaryLosses -= (employees * salary);
        ChangeBankStatement();
        StartCoroutine(WaitToPayEmployees());
    }

    void TicketSales(int _attendeeChange)
    {
        //This does not change all in one go like employee salaries, instead more attendees there are the more often they are charged ticket price
        IncreaseBalance(ticketPrice * _attendeeChange);
        ticketGains += ticketPrice;
        ChangeBankStatement();
        StartCoroutine(WaitToChargeTickets());
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
        yield return new WaitForSeconds(increaseRate); //Increase rate can change based on how good the park is os there is more attendees, or ca be flat
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

    #endregion
} //END BankManager.cs
