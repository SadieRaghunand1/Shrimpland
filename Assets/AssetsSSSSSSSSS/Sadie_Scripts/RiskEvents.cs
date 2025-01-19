using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RiskEvents : MonoBehaviour
{
    //Manages liklihood of a legal event + what happens in how player responds to it
    private BankManager bankManager;
    [SerializeField] private LawsuitData[] lawsuits;
    private LawsuitData currentLawsuit;
    [SerializeField] private float rollTimeMin;
    [SerializeField] private float rollTimeMax;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private TextMeshProUGUI settlement;
    [SerializeField] private TextMeshProUGUI legalLoss;
    [SerializeField] private TextMeshProUGUI outcome;
 
    private float numRolled;

    [SerializeField] private Transform lawInstancePos;

    [SerializeField] private Animator paperAnim;

    private void Start()
    {
        bankManager = FindAnyObjectByType<BankManager>();
        StartCoroutine(CalculateChanceOfLawsuit());
    }


    void ConductLawsuit()
    {
        outcome.text = " ";
        Debug.Log("Lawsuit called");
        int _index = Random.Range(0, lawsuits.Length - 1);

        currentLawsuit = lawsuits[_index];

        title.text = currentLawsuit.title;
        desc.text = currentLawsuit.desc;
        settlement.text = "$" + currentLawsuit.payout.ToString();
        legalLoss.text = "$" + currentLawsuit.fightFee.ToString();

        paperAnim.SetTrigger("StartLawsuit");
    }


    public void SettleLawsuit()
    {
        bankManager.DecreaseBalance(currentLawsuit.payout);
        bankManager.legalLosses -= currentLawsuit.payout;
        bankManager.ChangeBankStatement();
        bankManager.legalAnim.SetTrigger("LoseMoney");

        outcome.color = Color.blue;
        outcome.text = "Settled";
        paperAnim.SetTrigger("StopLawsuit");
    }

    public void FightLawsuit()
    {
        float _chanceToWin = Random.Range(0, 100);
        Debug.Log("Chnace to win calc to " + _chanceToWin);
        if(_chanceToWin <= currentLawsuit.chanceToWin)
        {
            //Lose case
            bankManager.DecreaseBalance(currentLawsuit.fightFee);
            bankManager.legalLosses -= currentLawsuit.fightFee;
            bankManager.ChangeBankStatement();
            bankManager.legalAnim.SetTrigger("LoseMoney");
            //Some visual indication that the case is lose
            outcome.color = Color.red;
            outcome.text = "Lost";
        }
        else
        {
            //Win
            outcome.color = Color.green;
            outcome.text = "Win";
        }

        paperAnim.SetTrigger("StopLawsuit");
    }




    IEnumerator CalculateChanceOfLawsuit()
    {
        float _seconds = Random.Range(rollTimeMin, rollTimeMax);
        yield return new WaitForSeconds(_seconds);

        numRolled = Random.Range(0, 100);

        if(numRolled <= bankManager.risk)
        {
            ConductLawsuit();
        }

        StartCoroutine(CalculateChanceOfLawsuit());
    }



} //END RiskEvents.cs
