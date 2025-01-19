using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Clipboard : MonoBehaviour
{
    public Image currentImage;
    public Sprite[] clipboardImages;
    public int currentIndex = 0;

    public DialogueManager dialogueManager;
    public void NextImage()
    {
        currentIndex++;

        if (currentIndex == 4)
        {
            //StartCoroutine(dialogueManager.PlayDialogue(dialogueManager.intro));
            dialogueManager.ReplaceText(dialogueManager.intro);
            this.gameObject.SetActive(false);
            return;
        }
        else if(currentIndex < clipboardImages.Length)
        {
            currentImage.sprite = clipboardImages[currentIndex];
        }
        

       
    }
}
