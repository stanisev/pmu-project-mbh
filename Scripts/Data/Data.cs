using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "New Data", menuName = "Collections/Data")]
public class Data : ScriptableObject
{
    public float _totalCoins;
    public int _totalEnergies;
    public int _currentRank;
    public int _totalSpins;

    public int _totalRightAnswers;
    public int _totalWrongAnswers;

    public string username;
}
