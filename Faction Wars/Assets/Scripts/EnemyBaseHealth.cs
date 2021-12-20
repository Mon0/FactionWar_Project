using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBaseHealth : MonoBehaviour
{

    public int startingHealth = 200;
    public int currentHealth;
    public Slider healthBar;

    GameObject playerBase;

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
