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
    [SerializeField] private float rollTimeMin;
    [SerializeField] private float rollTimeMax;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI desc;
    [SerializeField] private TextMeshProUGUI settlement;
    [SerializeField] private TextMeshProUGUI legalLoss;

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
        Debug.Log("Lawsuit called");
        int _index = Random.Range(0, lawsuits.Length - 1);
        //Instantiate(lawsuits[_index], lawInstancePos);

        title.text = lawsuits[_index].title;
        desc.text = lawsuits[_index].desc;
        settlement.text = "$" + lawsuits[_index].payout.ToString();
        legalLoss.text = "$" + lawsuits[_index].fightFee.ToString();

        paperAnim.SetTrigger("StartLawsuit");
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
}
