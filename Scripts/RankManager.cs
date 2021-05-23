using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankManager : MonoBehaviour
{
    public TMP_Text trophies;
    public TMP_Text rank;
    private int totalTrophies;
    public void Update()
    {
        totalTrophies = DataUpdater.instance.GetCorrectAnswer() / 10;
        trophies.text = totalTrophies.ToString();

        rank.text = GetRank(CalculateRank()).ToString();

    }

    private int CalculateRank()
    {
        return (DataUpdater.instance.GetCorrectAnswer() / 100);
    }

    private Ranks GetRank(int currentRank)
    {
        switch(currentRank)
        {
            case 0: return Ranks.NOVICE;
            case 1: return Ranks.BEGINNER;
            case 2: return Ranks.INTERMEDIATE;
            case 3: return Ranks.EXPERT;
            case 4: return Ranks.MASTER;
            default: return Ranks.MASTER;
        }
    }
}

public enum Ranks
{
    NOVICE,
    BEGINNER,
    INTERMEDIATE,
    EXPERT,
    MASTER
}
