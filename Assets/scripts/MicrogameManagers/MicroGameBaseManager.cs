using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MicroGameBaseManager : MonoBehaviour
{
    public bool isBroken;
    public bool bought;
    public GameObject gameParentObj;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected BankManager bankManager;
    [SerializeField] protected Canvas[] allUIsNotMicrogame;
    [SerializeField] protected Camera mainCamera;
    [SerializeField] protected AudioListener mainAudioListener;

    [SerializeField] private Image mapImage;
    [SerializeField] protected DialogueManager dialogueManager;
    public int count;

    public ClickSound audioUI;
    public virtual void StartGame()
    {
        audioUI.audioSource.Play();
        //mapImage.enabled = false;
        mapImage.color = new Color(255, 255, 255, 0);
        bought = true;
        mainCamera.enabled = false;
        mainAudioListener.enabled = false;

        for(int i = 0; i < allUIsNotMicrogame.Length; i++)
        {
            allUIsNotMicrogame[i].enabled = false;
        }

        gameParentObj.SetActive(true);
    }

    public virtual void ResetGame()
    {

        audioUI.audioSource.Play();
        Debug.Log("Reset called");
        //Base should be at end of ovrride methods
        isBroken = false;
        mainCamera.enabled = true;
        mainAudioListener.enabled = true;

        for (int i = 0; i < allUIsNotMicrogame.Length; i++)
        {
            allUIsNotMicrogame[i].enabled = true;
        }

        gameParentObj.SetActive(false);
    }
}
