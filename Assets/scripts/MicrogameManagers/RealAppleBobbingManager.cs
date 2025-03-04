using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealAppleBobbingManager : MicroGameBaseManager
{
    public tongs2 tongs;
    public Transform ogTongPos;

    

    public override void StartGame()
    {
        if(isBroken)
        {
            base.StartGame();

            if (count == 0)
            {
                dialogueManager.ReplaceText(dialogueManager.shrimpBobbingTutorial);
                count++;
            }

            if (isBroken)
            {
                tongs.moveSpeed = 5f;
            }
            else
            {
                tongs.moveSpeed = 0f;
            }
        }

        else
        {
            dialogueManager.ReplaceText(dialogueManager.attractionNotBroken);
        }

        
    }

    public override void ResetGame()
    {
        float _temp = bankManager.maintenanceLosses;
        bankManager.DecreaseBalance(5000);
        bankManager.maintenanceLosses -= 5000;
        if(_temp != bankManager.maintenanceLosses)
        {
            bankManager.maintenanceAnim.SetTrigger("LoseMoney");
        }

        tongs.gameObject.transform.position = ogTongPos.position;
        base.ResetGame();
    }
}
