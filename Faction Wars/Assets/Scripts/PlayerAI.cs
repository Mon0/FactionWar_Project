using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAI : MonoBehaviour
{

    Transform target;
    EnemyHealth attackTarget;
    EnemyBaseHealth baseTarget;
    NavMeshAgent agent;
    public int attackDamage = 10;
    public float attackCooldown = 0.5f;
    float cooldown;

    Collider[] unitsInRange;
    bool aggroed;

     void Awake()
    {
        target = GameObject.FindWithTag("EnemyBase").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.position);
    }


    void Aggro()
    {
        unitsInRange = Physics.OverlapSphere(transform.position, 5f);

        for (int i = 0; i < unitsInRange.Length; i++)
        {
            if (unitsInRange[i].gameObject.tag == "EnemyUnit")
            {
                target = unitsInRange[i].gameObject.transform;
                attackTarget = target.gameObject.GetComponent<EnemyHealth>();
                aggroed = true;
            }
            else if (unitsInRange[i].gameObject.tag == "EnemyBase")
            {
                target = unitsInRange[i].gameObject.transform;
                baseTarget = target.gameObject.GetComponent<EnemyBaseHealth>();
                aggroed = true;
            }
            else
            {
                target = GameObject.FindWithTag("EnemyBase").transform;
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
        if (col.gameObject.tag == "EnemyUnit")
        {
            Attack();
        }
        else if (col.gameObject.tag == "EnemyBase")
        {
            BaseAttack();
        }
        else if (col.gameObject.tag == "EnemyBase")
        {
            BaseAttack();
        }
    }

    void Attack()
    {
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
