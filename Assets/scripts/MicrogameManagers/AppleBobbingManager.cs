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

    private List<GameObject> screwsSpawned;

    //private int count = 0;

    public override void StartGame()
    {

        if(isBroken)
        {
            base.StartGame();
            if (count == 0)
            {
                dialogueManager.ReplaceText(dialogueManager.rollerCosterTutorial);
                count++;
            }
            SpawnScrews();
        }
        else
        {
            dialogueManager.ReplaceText(dialogueManager.attractionNotBroken);
        }
       
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

       /* for(int i = 0; i < screwsSpawned.Count; i++)
        {
            if (screwsSpawned[i] != null)
            {
                Destroy(screwsSpawned[i]);
            }
            
        }*/

        StopAllCoroutines();
        //screwsSpawned.Clear();

        base.ResetGame();
    }

    public void SpawnScrews()
    {
        int _index = Random.Range(0, spawnPoints.Length);

        if(isBroken && bought)
        {
            GameObject _screw = Instantiate(screwPrefab, spawnPoints[_index]);
           // screwsSpawned.Add(_screw);
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
