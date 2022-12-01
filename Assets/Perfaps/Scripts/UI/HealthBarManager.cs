using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public touchable Owner;
    public Object HealthStockPrefap;
    public int amountOfHealthStocks;

    private List<HealthStock> healthStocks;
    public int currentHealthStock;
    // Start is called before the first frame update
    void Start()
    {
        if (amountOfHealthStocks <= 0) return;
        //HealthStockPrefap = Resources.Load("Prefabs/GameObjects/HealthStock");
        for (int i = 0; i < amountOfHealthStocks; i++)
        {
            Instantiate(HealthStockPrefap, new Vector2(transform.position.x-(400*i*transform.localScale.x), transform.position.y), new Quaternion(), this.transform);
        }
        healthStocks = new List<HealthStock>(this.GetComponentsInChildren<HealthStock>());
        currentHealthStock = amountOfHealthStocks-1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float damage)
    {
        if (currentHealthStock < 0) return;
        healthStocks[currentHealthStock].TakeDamage(damage);
        if (!healthStocks[currentHealthStock].isAlive()) currentHealthStock--;
    }
    public void RestoreHealth(float healAmount)
    {
        if (currentHealthStock < 0) return;
        healthStocks[currentHealthStock].RestoreHealth(healAmount);
    }
}
