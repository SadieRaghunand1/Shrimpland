using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WhackAShrimpManager : MicroGameBaseManager
{
    public int spotsFixed;
    public int priceToFix;
    int numberOfBrokenSpots;
    public GameObject[] ListOfHoles;
    public GameObject Button;
    public TMP_Text buttonText;

    public WhackAShrimp[] shrimps;
    public SpriteRenderer[] shrimpRenderers;


    public override void StartGame()
    {

        if(isBroken)
        {
            base.StartGame();

            if (count == 00)
            {
                dialogueManager.ReplaceText(dialogueManager.wackTutorial);
                count++;
            }
            if (isBroken == true)
            {
                Button.SetActive(false);
                numberOfBrokenSpots = Random.Range(1, 4);
                spotsFixed = 0;

                for (int i = 0; i < numberOfBrokenSpots; i++)
                {
                    int x = Random.Range(0, 8);
                    //This nested if statement is a cheeky fix for if the game rolls the same well twice,
                    //it will make the for loop run an extra time
                    if (ListOfHoles[x].GetComponent<WhackAShrimp>().isWellBroken == true)
                    {
                        i--;
                    }
                    ListOfHoles[x].GetComponent<WhackAShrimp>().isWellBroken = true;
                    ListOfHoles[x].GetComponent<WhackAShrimp>().amIFixed = false;
                    Debug.Log("Hole #" + x + " is broken");
                }

            }
        }
        else
        {
            dialogueManager.ReplaceText(dialogueManager.attractionNotBroken);
        }
        
    }

    public void WinCondition()
    {
        if (spotsFixed == numberOfBrokenSpots)
        {
            Debug.Log("You have fixed the ride");
            priceToFix = numberOfBrokenSpots * 10000;
            buttonText.text = "Repair - $" + priceToFix + ",000";
            Button.SetActive(true);
        }
    }


    public override void ResetGame()
    {
        float _temp = bankManager.maintenanceLosses;
        bankManager.DecreaseBalance(priceToFix);
        bankManager.maintenanceLosses -= priceToFix;
        if(bankManager.maintenanceLosses != _temp)
        {
            bankManager.maintenanceAnim.SetTrigger("LoseMoney");
        }

        Button.SetActive(false);
        Color _tempColor = new Color(shrimpRenderers[0].color.r, shrimpRenderers[0].color.b, shrimpRenderers[0].color.g, 0);
        for (int i = 0; i < shrimpRenderers.Length; i++)
        {
            
            shrimpRenderers[i].color = _tempColor;
        }

        base.ResetGame();
    }

    public void QuitGame()
    {
        mainCamera.enabled = true;
        mainAudioListener.enabled = true;

        for (int i = 0; i < allUIsNotMicrogame.Length; i++)
        {
            allUIsNotMicrogame[i].enabled = true;
        }

        gameParentObj.SetActive(false);
    }
}
