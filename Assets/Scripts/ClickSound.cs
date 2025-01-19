using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource mapAmb;
    public AudioSource bankAmb;
    public Canvas map;
    public Canvas bank;
    public RealAppleBobbingManager bobbingManager;
    public WhackAShrimpManager shrimpManager;
    public AppleBobbingManager rollerManager;

    [SerializeField] private bool playThis;
    public void PlayClick()
    {
        audioSource.Play();
    }


    private void Update()
    {
        if(playThis)
        {
            if (!map.enabled)
            {
                mapAmb.volume = 0f;
            }
            else if (map.enabled)
            {
                mapAmb.volume = 1f;
            }

            if (bank.enabled || bobbingManager.gameParentObj.activeInHierarchy || shrimpManager.gameParentObj.activeInHierarchy || rollerManager.gameParentObj.activeInHierarchy)

            {
                bankAmb.volume = 1f;
            }
            else
            {
                bankAmb.volume = 0f;
            }
        }
       

    }
}
