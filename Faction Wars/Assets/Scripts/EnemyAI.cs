using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    Transform target;
    PlayerHealth attackTarget;
    PlayerBaseHealth baseTarget;
    NavMeshAgent agent;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;
    float cooldown;

    Collider[] unitsInRange;
    bool aggroed;

     void Awake()
    {
        target = GameObject.FindWithTag("PlayerBase").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


    void Aggro()
    {
        unitsInRange = Physics.OverlapSphere(transform.position, 5f);

        for (int i = 0; i < unitsInRange.Length; i++)
        {
            if (unitsInRange[i].gameObject.tag == "PlayerUnit")
            {
                target = unitsInRange[i].gameObject.transform;
                attackTarget = target.gameObject.GetComponent<PlayerHealth>();
                aggroed = true;
            }
            else if (unitsInRange[i].gameObject.tag == "PlayerBase")
            {
                target = unitsInRange[i].gameObject.transform;
                baseTarget = target.gameObject.GetComponent<PlayerBaseHealth>();
                aggroed = true;
            }
            else
            {
                target = GameObject.FindWithTag("PlayerBase").transform;
                agent.SetDestination(target.position);
            }
        }
    }

    void Chase()
    {
        agent.SetDestination(target.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerUnit")
        {
            Attack();
        }
        else if (col.gameObject.tag == "PlayerBase")
        {
            BaseAttack();
        }
    }

    void Attack()
    {
        cooldown = 0f;
        if (attackTarget.currentHealth > 0)
        {
            attackTarget.Damage(attackDamage);
        }
    }

    void BaseAttack()
    {
        cooldown = 0f;
        if (baseTarget.currentHealth > 0)
        {
            baseTarget.Damage(attackDamage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown += Time.deltaTime;

        if (!aggroed)
        {
            Aggro();
        }
        else
        {
            Chase();
        }

        if(attackTarget.currentHealth > 0 && cooldown >= attackCooldown)
        {
            Attack();
        }
        else if(baseTarget.currentHealth > 0 && cooldown >= attackCooldown)
        {
            BaseAttack();
        }
        else
        {
            aggroed = false;
        }
    }
}
