using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FacilityManager : MonoBehaviour
{
    [System.Serializable]
    public class FacilityData
    {
        public string facilityName;
        public string[] breakableThings;
        public bool[] statuses;

    }

    public FacilityData[] facilityDatas;
    public int facilityIndex;
        private BankManager bankManager;
    private GameManager gameManager;

    public bool[] bought; //plumbing, food, electrical, janitorial
    public Image[] mapImages;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI[] things;
    [SerializeField] private TextMeshProUGUI[] statusesWritten;
    [SerializeField] private Button[] fixButtons;
    [SerializeField] private Button loadBank;

    [SerializeField] private Canvas map;
    [SerializeField] private Canvas sheet;
    [SerializeField] private Canvas clipboardCanvas;
    public void Start()
    {
        bankManager = FindAnyObjectByType<BankManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        loadBank.onClick.AddListener(() => { gameManager.LoadBankSheet(); });

       
        //Test
       // FillInInfo(facilityIndex);
    }

    public void FillInInfo(int _index)
    {
        if (clipboardCanvas.enabled == true)
        {
            clipboardCanvas.enabled = false;
        }

        bought[_index] = true;
        //mapImages[_index].enabled = false;
        mapImages[_index].color = new Color(255, 255, 255, 0);
        //_index correponds to the index of each facility in above array
        title.text = facilityDatas[_index].facilityName;

        for(int i = 0; i < things.Length; i++)
        {
            things[i].text = facilityDatas[_index].breakableThings[i];
            statusesWritten[i].text = facilityDatas[_index].statuses[i].ToString();
        }
    }


    public void FixCorrespondingFacility(int _buttonIndex)
    {
        if(facilityDatas[facilityIndex].statuses[_buttonIndex] == false)
        {
            facilityDatas[facilityIndex].statuses[_buttonIndex] = true;
            FillInInfo(facilityIndex);

            bankManager.DecreaseBalance(200);
            bankManager.DecreaseRisk(1);
        }
    }

    public void SetFacilityIndex(int _facilityIndex)
    {
        facilityIndex = _facilityIndex;
        map.enabled = false;
        sheet.enabled = true;

        FillInInfo(facilityIndex);
    }

}
