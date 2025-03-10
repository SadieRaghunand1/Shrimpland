using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    private BankManager bankManager;

    //index 0 is cost, 1 is risk decrease
    [SerializeField] private float[] plumbingStats;
    [SerializeField] private float[] foodStats;
    [SerializeField] private float[] electricalStats;
    [SerializeField] private float[] janitorialStats;

    // Start is called before the first frame update
    void Start()
    {
        bankManager = FindAnyObjectByType<BankManager>();
    }

    public void UpgradePlumbing()
    {
        bankManager.plumbing = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(plumbingStats[0]);
        bankManager.DecreaseRisk(plumbingStats[1]);
    }

    public void UpgradeFood()
    {
        bankManager.food = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(foodStats[0]);
        bankManager.DecreaseRisk(foodStats[1]);
    }

    public void UpgradeElectrical()
    {
        bankManager.electrical = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(electricalStats[0]);
        bankManager.DecreaseRisk(electricalStats[1]);
    }

    public void UpgradeJanitorial()
    {
        bankManager.janitorial = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(janitorialStats[0]);
        bankManager.DecreaseRisk(janitorialStats[1]);
    }
}
