using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnController : MonoBehaviour {
    public GameObject gyrod, turtle, meteorite, phasem;
    private Rigidbody spawnerig;
    public bool roundrest = true;
    public string[] rounds;
    public float timer = 0;
    public int currentround = 0, resttime, alivebaddies;
    /// <summary>
    /// Spawns an enemy.
    /// </summary>
    /// <param name="enemycode">Desired type of enemy to spawn.</param>
    /// /// <param name="ammount">Desired ammount of enemies to spawn.</param>
    public void SpawnEnemy(int enemycode,int ammount)
    {
        float posxmin = 0, posxmax= 0, miny= 0;
        GameObject enemy = null;
        int num = 0, minydelta = 0;
            switch (enemycode)
        {
            case (1):
                { posxmin = 0;
                    posxmax = 27;
                    miny = 30;
                    enemy = gyrod;
                    minydelta = -2;
                }
                break;
            case (2):
                {
                    posxmin = 0;
                    posxmax = 27;
                    miny = 18;
                    enemy = turtle;
                    minydelta = 4;
                }
                break;
            case (4):
                {
                    posxmin = 0;
                    posxmax = 27;
                    miny = 20;
                    enemy = phasem;
                    minydelta = -5;
                }
                break;
            case (3):
                {
                    switch (global.Rnd(60))
                    {
                        case false:
                            {
                                posxmin = -10;
                                posxmax = -5;
                            }
                            break;
                        case true:
                            {
                                posxmin = 35;
                                posxmax = 40;
                            }
                            break;
                    }
                    miny = 35;
                    enemy = meteorite;
                    minydelta = 3;
                }
                break;
        }
        for (num = 0; num < ammount; num++)
        { Instantiate(enemy, new Vector3(Random.Range(posxmin, posxmax) - 13.5f, Random.Range(miny, 42), 0f), Quaternion.identity);
            miny = Mathf.Clamp(miny + minydelta, 20, 46);
        }
    }
    public void RoundSpawn(string code)
    {
        int cnt = 1;
        int enemy = 0;
        int ammount = 0;
        bool gettingTime = false;
        foreach (char ch in code)
        {if (cnt == 1)
            {
                if (ch == "x"[0])
                { gettingTime = true; }
                else
                {
                    enemy = ch - 48;
                }
                cnt++;
            }
            else
            {   if (gettingTime)
                { resttime = ch - 48;
                    gettingTime = !gettingTime;
                    break;
                }
                else
                {
                    ammount = ch - 48;
                    SpawnEnemy(enemy, ammount);
                }
                cnt = 1;
            }
        }
    }
	void FixedUpdate () {
        if (global.isPaused == false)
        {
            if (GameObject.Find("Player") == null)
            {
                Destroy(gameObject);
            }
            alivebaddies = GameObject.FindGameObjectsWithTag("enemy").Length -FindObjectsOfType<meteoriteController>().Length;
            if ((Mathf.Round(Time.timeSinceLevelLoad * 100) / 100) % 2 == 0)
            {
                if (global.Rnd(50))
                { SpawnEnemy(3, 1); }
                
            }
            if (alivebaddies == 0)
            {
                if (roundrest) timer += Time.fixedDeltaTime;
                    if (Mathf.Round(timer*100)/100  == resttime)
                {
                    roundrest = false;
                    if (currentround == rounds.Length) currentround = 0;
                    RoundSpawn(rounds[currentround]);
                    currentround++;
                    timer = 0;
                }
                else roundrest = true;
            }
        }
    }
}
