using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Build indexes will be changed

    [SerializeField] private Canvas mapCanvas;
    [SerializeField] private Canvas facilitySheetCanvas;
    [SerializeField] private Canvas bankSheetCanvas;
    [SerializeField] private Canvas otherCanvas;

    int countBank = 0;
    [SerializeField] private DialogueManager dialogueManager;

    public ClickSound audioUI;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadBankSheet()
    {
        audioUI.audioSource.Play();
        //SceneManager.LoadScene(0);
        mapCanvas.enabled = false;
        facilitySheetCanvas.enabled = false;
        bankSheetCanvas.enabled = true;
        otherCanvas.enabled = false;

        if(countBank == 0)
        {
            dialogueManager.ReplaceText(dialogueManager.bankBalanceTutoial);
            countBank++;
        }
    }

    public void LoadMap()
    {
        StartCoroutine(dialogueManager.HideDialogueBox(0));
        audioUI.audioSource.Play();
        //SceneManager.LoadScene(1);
        mapCanvas.enabled = true;
        facilitySheetCanvas.enabled = false;
        bankSheetCanvas.enabled = false;
        otherCanvas.enabled = true;
    }

    
}
