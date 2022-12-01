using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    protected float health;
    protected float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float precentComplete = lerpTimer / chipSpeed;
            precentComplete *= precentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, precentComplete);
        }
        if (fillF < hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float precentComplete = lerpTimer / chipSpeed;
            precentComplete *= precentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, precentComplete);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
