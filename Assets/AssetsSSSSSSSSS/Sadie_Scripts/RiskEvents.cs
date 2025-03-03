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
    [SerializeField] private Image outcomeImage;
    [SerializeField] private Sprite[] possibleOutcomes; //0 is won, 1 is lost, 2 is settled
    [SerializeField] private Color paperColor;
    [SerializeField] private Animator outcomeAnim;
 
    private float numRolled;

    [SerializeField] private Transform lawInstancePos;

    [SerializeField] private Animator paperAnim;
    [SerializeField] private Animator judgeAnim;
    [SerializeField] private Animator gavelAnim;

    [SerializeField] private ClickSound audioL;
    private void Start()
    {
        bankManager = FindAnyObjectByType<BankManager>();
        StartCoroutine(CalculateChanceOfLawsuit());
    }


    void ConductLawsuit()
    {
        audioL.PlayClick();
        outcome.text = " ";
        outcomeImage.sprite = null;
        outcomeImage.color = paperColor;
        Debug.Log("Lawsuit called");
        int _index = Random.Range(0, lawsuits.Length - 1);

        currentLawsuit = lawsuits[_index];

        title.text = currentLawsuit.title;
        desc.text = currentLawsuit.desc;
        settlement.text = "$" + currentLawsuit.payout.ToString();
        legalLoss.text = "$" + currentLawsuit.fightFee.ToString();

        paperAnim.SetTrigger("StartLawsuit");
        judgeAnim.SetTrigger("StartLawsuit");
        gavelAnim.SetTrigger("StartLawsuit");
    }


    public void SettleLawsuit()
    {
        bankManager.DecreaseBalance(currentLawsuit.payout);
        bankManager.legalLosses -= currentLawsuit.payout;
        bankManager.ChangeBankStatement();
        bankManager.legalAnim.SetTrigger("LoseMoney");

        /*outcome.color = Color.blue;
        outcome.text = "Settled";*/

        outcomeAnim.SetTrigger("CaseOver");
        outcomeImage.sprite = possibleOutcomes[2];
        outcomeImage.color = Color.white;

        paperAnim.SetTrigger("StopLawsuit");
        judgeAnim.SetTrigger("StopLawsuit");
        gavelAnim.SetTrigger("StopLawsuit");
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
            /* outcome.color = Color.red;
             outcome.text = "Lost";*/

            outcomeAnim.SetTrigger("CaseOver");
            outcomeImage.sprite = possibleOutcomes[1];
            outcomeImage.color = Color.white;
        }
        else
        {
            //Win
            /*outcome.color = Color.green;
            outcome.text = "Win";*/
            outcomeAnim.SetTrigger("CaseOver");
            outcomeImage.sprite = possibleOutcomes[0];
            outcomeImage.color = Color.white;
            
        }

        paperAnim.SetTrigger("StopLawsuit");
        judgeAnim.SetTrigger("StopLawsuit");
        gavelAnim.SetTrigger("StopLawsuit");
    }




    IEnumerator CalculateChanceOfLawsuit()
    {
        float _seconds = Random.Range(rollTimeMin, rollTimeMax);
        yield return new WaitForSeconds(_seconds);

        numRolled = Random.Range(0, 101);

        if(numRolled <= bankManager.risk)
        {
            ConductLawsuit();
        }

        StartCoroutine(CalculateChanceOfLawsuit());
    }



} //END RiskEvents.cs
