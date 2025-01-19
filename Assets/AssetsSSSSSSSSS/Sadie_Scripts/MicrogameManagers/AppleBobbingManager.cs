using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleBobbingManager : MicroGameBaseManager
{
    //ROLLERCOASTER not apple bobbing


    public int countFallen;

    [SerializeField] private GameObject screwPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float cost;
    [SerializeField] private timer timerTime;

    //private int count = 0;

    public override void StartGame()
    {
        base.StartGame();
        SpawnScrews();
    }

    public override void ResetGame()
    {
        Debug.Log(countFallen);

        bankManager.DecreaseRisk(countFallen);
        bankManager.DecreaseBalance(countFallen * cost);
        bankManager.maintenanceLosses -= countFallen * cost;
        bankManager.ChangeBankStatement();
        bankManager.maintenanceAnim.SetTrigger("LoseMoney");
        timerTime.time1 = 0;
        timerTime.elapTime = 0;

        countFallen = 0;
        base.ResetGame();
    }

    public void SpawnScrews()
    {
        int _index = Random.Range(0, spawnPoints.Length);

        if(isBroken && bought)
        {
            Instantiate(screwPrefab, spawnPoints[_index]);
        }
        else
        {
            timerTime.timerText.text = "n/a";
        }

        StartCoroutine(CheckLoopsBeforeEndGame());
    }

    IEnumerator CheckLoopsBeforeEndGame()
    {
        yield return new WaitForSeconds(1);

        SpawnScrews();
    }
}
