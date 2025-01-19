using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private Button loadBank;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        loadBank.onClick.AddListener(() => { gameManager.LoadBankSheet(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
