using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Player
{
    public static GameObject obj;
    public static Transform trans;
    public static Rigidbody rig;
    public static AudioSource asource;
    public static PlayerController script;
    public static BoxCollider boxcol;
    public static CapsuleCollider capcol;
}
public class rnJesus : MonoBehaviour
{
    public float yes, no, realOdds, maxdelta;
    public int chance;
    public bool CountOnOdds;
    public void Start()
    {
       global.rng = Random.Range(0, 9);
       Player.obj = GameObject.Find("Player");
       Player.trans = Player.obj.GetComponent<Transform>();
       Player.rig = Player.obj.GetComponent<Rigidbody>();
        Player.asource = Player.obj.GetComponent<AudioSource>();
        Player.script = Player.obj.GetComponent<PlayerController>();
        Player.boxcol = Player.obj.GetComponent<BoxCollider>();
        Player.capcol = Player.obj.GetComponent<CapsuleCollider>();
    }
    public void FixedUpdate()
    {
        if (CountOnOdds)
        { global.rng += 1; }
        else if (Time.frameCount % 3 == 0)
        {
            global.rng += 1;
        }
        if (global.rng > 9)
        { global.rng = 0;
          CountOnOdds = !CountOnOdds;

        }
        if (global.Rnd(chance))
        {
            yes += 1;
            //Debug.Log("yes");
        }
        else { no += 1;
            //Debug.Log("no");
        }
        
        realOdds = yes * 100 / (yes + no);
        maxdelta = Mathf.Abs(realOdds - chance);
    }
}
public static class global
{
    public static float VectorAng(Vector3 vec3)
    { float ang;
      ang = Mathf.Atan2(vec3.y, vec3.x) * Mathf.Rad2Deg;
        return ang;
    }
    public static int score;
    public static void Death(int id)
    { 
        switch (id)
        {
            case 1:
                { score += 100; }
                break;
            case 2:
                { score += 200; }
                break;
            case 3:
                { score += 50; }
                break;
            case 4:
                { score += 150; }
                break;
        }
        foreach (ScoreController sc in GameObject.FindObjectsOfType<ScoreController>())
        {
            sc.ResetScores();
            sc.UpdateScores();
        }
    }
    public static float rng;
    public static float rescalingFactor = 1;
    public static bool isPaused;
    public static void Pause(GameObject go)
    { go.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); }
    public static bool Rnd(int prob)
    {switch (prob)
        {
            case 10:
                {if (rng == 5)
                    { return true; }
                }
                break;
            case 20:
                {
                    if (rng == 0 || rng == 9)
                    { return true; }
                }
                break;
            case 30:
                {
                    if (rng == 2 || rng == 5 || rng == 8)
                    { return true; }
                }
                break;
            case 40:
                {
                    if (rng == 2 || rng == 4 || rng == 6 || rng == 8)
                    { return true; }
                }
                break;
            case 50:
                {
                    if (rng == 0 || rng == 2 || rng == 4 || rng == 6 || rng == 8)
                    { return true; }
                }
                break;
            case 60:
                {
                    if (rng == 1 || rng == 2 || rng == 4 || rng == 6 || rng ==7 || rng == 9)
                    { return true; }
                }
                break;
            case 70:
                {
                    if (rng == 0 || rng == 2 || rng == 3 || rng == 5 || rng == 6 || rng == 7 || rng == 8)
                    { return true; }
                }
                break;
            case 80:
                {
                    if (rng == 1 || rng == 2 || rng == 3 || rng == 4 || rng == 5 || rng == 6 || rng == 7 || rng == 8)
                    { return true; }
                }
                break;
            case 90:
                {
                    if (rng == 1 || rng == 2 || rng == 3 || rng == 4 || rng == 5 || rng == 6 || rng == 7 || rng == 8 || rng == 9)
                    { return true; }
                }
                break;
        }
        return false;
    }
}

