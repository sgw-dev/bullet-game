using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth;
    private float currentHealth;
    private bool shielding = false;
    public Slider shieldBar;
    public float maxShield;
    private float currentShield;
    public GameObject shield;

    private Renderer shieldRend;
    private Color startColor;
    public Color brokenColor;
    public float shieldRegenDelay = .5f;
    private float timer;
    public float shieldRegenSpeed = .05f;
    private bool regenerating = false;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        currentHealth = maxHealth;

        shieldBar.maxValue = maxShield;
        shieldBar.value = maxShield;
        currentShield = maxShield;

        shieldRend = shield.GetComponent<Renderer>();
        startColor = shieldRend.material.color;
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
        //If the timer is not 0, start counting down
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            //If the timer is over, set the regen flag
            if(timer <= 0)
            {
                regenerating = true;
            }
        }
        if (regenerating) //If the regen flag is set, regen
        {
            currentShield += shieldRegenSpeed * Time.deltaTime;
            //Reflect the change in the shield bar and the shield color
            shieldRend.material.color = Color.Lerp(brokenColor, startColor, currentShield / maxShield);
            shieldBar.value = currentShield;
            //Is the shield full? if so, stop regenerating
            if (currentShield > maxShield)
            {
                regenerating = false;
                currentShield = maxShield;
            }
        }
    }
    void DamageShield(int damage)
    {
        //You took shield damage, reset the timer and make sure the regen flag is off
        timer = shieldRegenDelay;
        regenerating = false;

        currentShield -= damage;
        if(currentShield < 0)
        {
            float damageLeft = Mathf.Abs(currentShield);
            currentShield = 0;
            shieldBar.value = 0;
            DamageHealth(damageLeft);
        }
        else
        {
            shieldRend.material.color = Color.Lerp(brokenColor, startColor, currentShield / maxShield);
            shieldBar.value = currentShield;
        }
    }
    void DamageHealth(float damage)
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
