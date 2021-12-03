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

    public int PlayerMoney;
    public int GoodUpgrade;
    public int GoodGoldAdd;

    public int EnemyMoney;
    public int EvilUpgrade;
    public int EvilGoldAdd;

    public int DelayTimer;
    public float Timer;
    public float ExTimer;
    public float ErrorTimer;

    public TMP_Text PlayerG;
    public TMP_Text EnemyG;
    public TMP_Text PlayerError;
    public TMP_Text EnemyError;
    public TMP_Text GoodUpCost;
    public TMP_Text BadUpCost;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMoney = 50; 
        EnemyMoney = 50; //Player and AI both start with 50 gold
        DelayTimer = 1; //Gold is added every 1 second
        ErrorTimer = 3;

        GoodUpgrade = 0;
        EvilUpgrade = 0; //Player and AI both start off with zero upgrades  

        GoodGoldAdd = 1;
        EvilGoldAdd = 1; //Player and AI both start off gaining 1 gold per second 
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= DelayTimer)
        {
            Timer = 0f;
            SetPlayerMoney();
            SetEnemyMoney();
            SetUpgradeCost();
        }

        ExTimer += Time.deltaTime;
        if (ExTimer >= ErrorTimer)
        {
            ExTimer = 0f;
            EnemyError.enabled = false;
            PlayerError.enabled = false;
        }
    }

    public void SpawnEnemy()
    {
        if(EnemyMoney >= 10)
        {
            Instantiate(Enemy, EnemySpawn.transform.position, Quaternion.identity);
            EnemyMoney -= 10;
        }
        else
        {
            EnemyError.enabled = true;
            EnemyError.text = "Not enough gold!";
        }
    }

    void SetEnemyMoney()
    {
        EnemyG.text = "Gold: " + EnemyMoney;
        EnemyMoney += EvilGoldAdd;
    }

    public void EnemyUpgrade()
    {
        EnemyMoney -= EvilUpgrade;
        EvilUpgrade++;
        EvilGoldAdd += EvilUpgrade;
    }

    public void SpawnPlayer()
    {
        if(PlayerMoney >= 10)
        {
            Instantiate(Player, PlayerSpawn.transform.position, Quaternion.identity);
            PlayerMoney -= 10;
        }
        else
        {
            PlayerError.enabled = true;
            PlayerError.text = "Not enough gold!";
        }
    }

    void SetPlayerMoney()
    {
        PlayerG.text = "Gold: " + PlayerMoney;
        PlayerMoney += GoodGoldAdd;
    }

    public void PlayerUpgrade()
    {
        PlayerMoney -= GoodUpgrade;
        GoodUpgrade++;
        GoodGoldAdd += GoodUpgrade;
    }

    void SetUpgradeCost()
    {
        GoodUpCost.text = "Upgrade Income: " + GoodGoldAdd;
        BadUpCost.text = "Upgrade Income: " + EvilGoldAdd;
    }
}
