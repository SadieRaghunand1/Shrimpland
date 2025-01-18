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

    [Header("Facilities")]
    public FacilitiesStatus plumbing;
    public FacilitiesStatus food;
    public FacilitiesStatus electrical;
    public FacilitiesStatus janitorial;

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


    public void ChangeEmployeesAndAttendees(int _employeeChange, int _attendeesChange)
    {
        //both increase depending on how many things are going well in park, so depending on how many things are bought, repaired, maybe make a rapport variable, how public views park?
        //Or every time a thing is bought or fixed calls this?
        //Should risk change/increase w this?


        Debug.Log("Change emloyees");
        employees += _employeeChange;
        attendees += _attendeesChange;

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
        StartCoroutine(WaitToPayEmployees());
    }

    void TicketSales()
    {
        //This does not change all in one go like employee salaries, instead more attendees there are the more often they are charged ticket price
        IncreaseBalance(ticketPrice);
        //StartCoroutine(WaitToChargeTickets());
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

        TicketSales();
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
