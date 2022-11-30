using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public float invincibleLength;
    private float invincibleCounter;
    private SpriteRenderer theSR;

    private void Awake()
    {
        instance = this;
    }

    public int currentHealth, maxHealth;
    void Start()
    {
        currentHealth = maxHealth;
        theSR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(invincibleCounter > 0)
        {
            invincibleCounter -= Time.deltaTime;
            if(invincibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);

            }
        }
    }

    public void DealDamage()
    {
        if(invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                LevelMenager.instance.RespawnPlayer();
            }
            else
            {
                invincibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                PlayerController.instance.KnockBack();
            }

            UIController.instance.UpdateHealtDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.instance.UpdateHealtDisplay();
    }
}
