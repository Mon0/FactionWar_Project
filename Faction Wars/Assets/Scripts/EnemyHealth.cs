using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public int startingHealth = 50;
    public int currentHealth;
    public Slider healthBar;
    public bool dead;

    void Awake()
    {
        currentHealth = startingHealth;
    }

    public void Damage(int hit)
    {
        if (dead)
            return;

        currentHealth -= hit;
        healthBar.value = currentHealth;
        
        if(currentHealth <= 0)
        {
            dead = true;
            Destroy(gameObject, 1f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
