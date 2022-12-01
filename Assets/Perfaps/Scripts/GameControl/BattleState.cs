using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    public List<Player> Players;
    // Start is called before the first frame update
    void Start()
    {
        Players = new List<Player>(this.GetComponentsInChildren<Player>());
        HealthBarManager[] healthBars = GameObject.Find("Canvas").GetComponentsInChildren<HealthBarManager>();
        for (int i = 0; i < healthBars.Length; i++)
        {
            if (i >= Players.Count) continue;
            Players[i].HealthBar = healthBars[i];
        }
    }
}
