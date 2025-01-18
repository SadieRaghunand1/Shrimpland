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


    public float currentBalance;
    public float risk; //Max at 100, min 0
    private float goal = 100000000f; //1 krillion, when currentBalance hits this, win; if hist 0, lose

    [Header("UI")]
    [SerializeField] TextMeshProUGUI totalBalanceTx;
    [SerializeField] Canvas storeCanvas;

    [Header("Facilities")]
    public FacilitiesStatus plumbing;
    public FacilitiesStatus food;
    public FacilitiesStatus electrical;
    public FacilitiesStatus janitorial;

    private void Start()
    {
        totalBalanceTx.text = "$" + currentBalance.ToString();
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
        }
        else if(currentBalance <= 0)
        {
            //Lose
        }
    }

    public void DecreaseRisk(float _amount)
    {
        if(risk > 0)
        risk -= _amount;
    }

 
    public void IncreaseRisk(float _increase)
    {
        if(risk < 100)
        risk += _increase;
    }
    #endregion

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
}
