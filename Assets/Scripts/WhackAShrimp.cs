using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WhackAShrimp : MonoBehaviour
{
    public bool isWellBroken;
    public bool amIFixed;
    [SerializeField] private Animator animator;
    GameObject shrimp;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }
        

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
    if (isWellBroken == false) {
        Debug.Log("Boioioioioing");
        animator.SetTrigger("Appear");
        }
    else if (isWellBroken == true && amIFixed == false) {
        amIFixed = true;
        Debug.Log("I am fixed!");
            gameManager.spotsFixed++;
            Debug.Log(gameManager.spotsFixed + " holes are fixed");
            gameManager.WinCondition();
        }  

    }

}
