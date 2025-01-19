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

        bankManager.maintenanceLosses -= plumbingStats[0];
        bankManager.ChangeBankStatement();
        bankManager.maintenanceAnim.SetTrigger("LoseMoney");
    }

    public void UpgradeFood()
    {
        bankManager.food = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(foodStats[0]);
        bankManager.DecreaseRisk(foodStats[1]);

        bankManager.maintenanceLosses -= foodStats[0];
        bankManager.ChangeBankStatement();
        bankManager.maintenanceAnim.SetTrigger("LoseMoney");
    }

    public void UpgradeElectrical()
    {
        bankManager.electrical = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(electricalStats[0]);
        bankManager.DecreaseRisk(electricalStats[1]);

        bankManager.maintenanceLosses -= electricalStats[0];
        bankManager.ChangeBankStatement();
        bankManager.maintenanceAnim.SetTrigger("LoseMoney");
    }

    public void UpgradeJanitorial()
    {
        bankManager.janitorial = BankManager.FacilitiesStatus.UPGRADED;
        bankManager.DecreaseBalance(janitorialStats[0]);
        bankManager.DecreaseRisk(janitorialStats[1]);

        bankManager.maintenanceLosses -= janitorialStats[0];
        bankManager.ChangeBankStatement();
        bankManager.maintenanceAnim.SetTrigger("LoseMoney");
    }
}
