using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth;
    public int currentHealth;
    public bool shielding = false;
    public Slider shieldBar;
    public int maxShield;
    public int currentShield;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        shieldBar.maxValue = maxShield;
        shieldBar.value = maxShield;
        currentShield = maxShield;
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            if (currentShield > 0)
            {
                if (!shielding)
                {
                    shielding = true;
                    shield.SetActive(true);
                }
            }
            else
            {
                shield.SetActive(false);
            }
            
        }
        if (Input.GetButtonUp("Fire2"))
        {
            shielding = false;
            shield.SetActive(false);
        }
    }
    void DamageShield(int damage)
    {
        currentShield -= damage;
        if(currentShield < 0)
        {
            int damageLeft = Mathf.Abs(currentShield);
            currentShield = 0;
            shieldBar.value = 0;
            DamageHealth(damageLeft);
        }
        else
        {
            shieldBar.value = currentShield;
        }
    }
    void DamageHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
            if (shielding)
            {
                DamageShield(1);
            }
            else
            {
                DamageHealth(1);
            }
            
        }
    }
}
