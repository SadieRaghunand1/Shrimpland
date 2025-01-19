using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class LawsuitData : ScriptableObject
{
    public string title;
    public string desc;
    public float payout;
    public float fightFee;
    public float chanceToWin;
}
