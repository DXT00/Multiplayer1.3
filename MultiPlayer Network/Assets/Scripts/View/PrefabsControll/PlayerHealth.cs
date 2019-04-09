using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class PlayerHealth : MonoBehaviour 
{

    public const float startingHealth = 100;


  
    float damageCount = 10;
    bool isDead;

    public Slider healthSlider;
    private Animator anim;

    
    float currentHealth = startingHealth;
	// Use this for initialization
	
        //currentHealth
        //healthSlider.value = startingHealth;
	
	// Update is called once per frame
	void Start () {
        anim = GetComponent<Animator>();

    }

    public void TakeDamage()
    {
       
        if (isDead) return;

        currentHealth -= damageCount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
    }
}
