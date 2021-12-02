using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject PlayerSpawn;
    public GameObject EnemySpawn;
    public GameObject Player;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Instantiate(Enemy, EnemySpawn.transform.position, Quaternion.identity);
    }

    public void SpawnPlayer()
    {
        Instantiate(Player, PlayerSpawn.transform.position, Quaternion.identity);
    }
}
