using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carosel : MonoBehaviour
{
    
    public DialogueManager dialogueManager;


    public void SFX()
    {
        dialogueManager.ReplaceText(dialogueManager.caroselcantBuy);
    }
}
