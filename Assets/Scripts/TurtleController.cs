using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleController : MonoBehaviour {
    public unitVars vars;
    private float roundglobal = 0, roundlocal, cociente, stopFrequency;
    public float firingTime,lastspeed;
    public AudioClip breakdown, breakdown2;
    public GameObject hitpoint;
    private GameObject spawnedshot;
    public BoxCollider bcollider;
    public float fireTimer;
    public bool canfire = true, IsPermaStunned, IsStunned, hasPlayed;
    [Range(0,1)]
    public float timescale;
    private bool direccionCalculada = false;
    private bool closed = true;
    private int lastanim, cont;
    public string lado;
    void Start () {
        vars.InitVars(vars, gameObject);
        vars.anim.SetBool("closed", true);
        stopFrequency = (Random.Range(1, 8));
        vars.sspawn1 = transform.Find("shotspawn");
        vars.hp = 10;
        hitpoint = transform.Find("critic").gameObject;
        vars.asource = GetComponent<AudioSource>();
        bcollider = hitpoint.GetComponent<BoxCollider>();
        if ((vars.rig.position.x < -13 && direccionCalculada == false))
        {
            vars.speed = Mathf.Abs(vars.speed);
            direccionCalculada = true;
            lado = "izq";
            lastanim = 1;
            vars.anim.SetInteger("animation", lastanim);
        }
        if ((vars.rig.position.x > 13 && direccionCalculada == false))
        {
            vars.speed *= -1;
            direccionCalculada = true;
            lado = "der";
            lastanim = 2;
            vars.anim.SetInteger("animation", lastanim);
        }
    }
    void Update()
    {
        if (global.isPaused == false)
        {
            //if (Player.trans.position.y - 5 > vars.trans.position.y)
            //{ vars.trans.eulerAngles = new Vector3(0, 0, -180); }
            //else vars.trans.eulerAngles = new Vector3(0, 0, 0);
            vars.trans.eulerAngles = (Player.trans.position.y - 5 > vars.trans.position.y) ? new Vector3(0, 0, -180) : new Vector3(0, 0, 0);
            vars.rend.flipX = (Player.trans.position.y - 5 > vars.trans.position.y) ? true : false;
            if (IsPermaStunned == true)
            {
                vars.speed = 0;
            }
            if (IsStunned == true)
            {
                vars.speed = 0;
                vars.timer3 += Time.deltaTime;
                if (vars.timer3 >= 2 && hasPlayed == false)
                {
                    vars.asource.PlayOneShot(breakdown2, vars.vol3);
                    hasPlayed = true;
                }
                if (vars.timer3 >= 3)
                {
                    IsStunned = false;
                    vars.speed = lastspeed;
                    cont = 0;
                    vars.timer3 = 0;
                    hasPlayed = false;
                }
            }
            if (GameObject.Find("Player") == null)
            {
                Destroy(gameObject);
            }
            vars.asource.enabled = true;
            cociente = 1 / Time.deltaTime;
            Time.timeScale = timescale;
            vars.timer1 += Time.deltaTime;
            roundglobal = Mathf.Round(vars.timer1 * 10) / 10;
            if (roundglobal > stopFrequency - 15 / cociente && closed == true)
            {
                bcollider.enabled = false;
            }
            if (roundglobal > stopFrequency + 15/ cociente && closed == false)
            {
                bcollider.enabled = true;
            }

            if (closed == false && roundglobal < stopFrequency && canfire == true)
            {
                spawnedshot = Instantiate(vars.shot, vars.sspawn1.position, vars.trans.rotation);
                spawnedshot.GetComponent<turtleShot>().Turtle = gameObject;
                canfire = false;

                fireTimer = stopFrequency / 2 * cociente;
            }
            if ((vars.rig.position.x < -13 && direccionCalculada == false))
            {
                vars.speed = Mathf.Abs(vars.speed);
                direccionCalculada = true;
                lado = "izq";
                lastanim = 1;
                vars.anim.SetInteger("animation", lastanim);
            }
            if ((vars.rig.position.x > 13 && direccionCalculada == false))
            {
                vars.speed *= -1;
                direccionCalculada = true;
                lado = "der";
                lastanim = 2;
                vars.anim.SetInteger("animation", lastanim);
            }
            if (roundglobal < stopFrequency)
            {
                vars.rig.velocity = new Vector3(vars.speed, 0f, 0f);
                vars.anim.SetInteger("animation", lastanim);
                vars.timer2 = 0;
            }
            //(roundglobal > 4.99f && roundglobal<5.01f)
            if (vars.timer1 > stopFrequency - 1f / cociente && vars.timer1 < stopFrequency + 1f / cociente)
            {
                vars.rig.velocity = new Vector3(0f, 0f, 0f);
                vars.anim.SetInteger("animation", 0);

                if (closed == true && vars.timer2 == 0)
                {
                    vars.anim.SetBool("closed", false);
                    closed = false;
                    vars.timer2 += 1;
                    gameObject.GetComponent<BoxCollider>().size = new Vector3(0.26f, gameObject.GetComponent<BoxCollider>().size.y, 0.2f);
                }
                else if (closed == false && vars.timer2 == 0)
                {
                    vars.anim.SetBool("closed", true);
                    closed = true;
                    vars.timer2 += 1;
                    gameObject.GetComponent<BoxCollider>().size = new Vector3(0.2f, gameObject.GetComponent<BoxCollider>().size.y, 0.2f);
                }

            }
            if ((vars.rig.position.x > 13 && lado == "izq") || (vars.rig.position.x < -13 && lado == "der"))
            {
                direccionCalculada = false;
            }
            if (roundglobal >= stopFrequency + 43f / cociente)
            {
                switch (Random.Range(0, 1))
                {
                    case 0:
                        {
                            stopFrequency = (Random.Range(4, 5));
                        }
                        break;
                    case 1:
                        { stopFrequency = (Random.Range(6, 7)); }
                        break;
                }
                vars.timer1 = 0;
                vars.anim.SetInteger("animation", 0);
                canfire = true;
                if (lado == "der") vars.speed = -Random.Range(12, 20);
                else vars.speed = Random.Range(15, 20);
            }
            if (vars.hp <= 0)
            {
                vars.rig.velocity = new Vector3(0f, 0f, 0f);
                Instantiate(vars.deathanim, transform.position, Quaternion.identity);
                Destroy(gameObject);
                global.Death(2);
            }
            if (hitpoint.GetComponent<CriticalHit>().hit == 1)
            {
                vars.asource.Stop();
                vars.asource.PlayOneShot(vars.hit1, vars.vol1);
                hitpoint.GetComponent<CriticalHit>().hit = 0;
            }
        }
        else global.Pause(gameObject);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "shot(Clone)")
        {
            Destroy(col.gameObject);
            
            if (IsStunned == false && IsPermaStunned == false)
            {
                cont++;
                switch (cont)
                    { case 1:
                        { vars.asource.PlayOneShot(vars.hit2, vars.vol2 / 3f); }
                        break;
                    case 2:
                        { vars.asource.PlayOneShot(vars.shotsound, vars.vol2 / 3f); }
                        break;
                    case 3:
                        { vars.asource.PlayOneShot(vars.hit3, vars.vol2 / 3f); }
                        break;
                    default:
                        { vars.asource.PlayOneShot(vars.hit2, vars.vol2 / 3f); }
                        break;

                }
                if (cont == 3)
                {
                    vars.asource.PlayOneShot(breakdown, vars.vol3);
                    lastspeed = vars.speed;
                    IsStunned = true;
                }
            }

        }
        if (col.gameObject.GetComponent<shieldPowerUp>() != null && col.gameObject.GetComponent<shieldPowerUp>().isCaptured == true)
        {   vars.asource.PlayOneShot(vars.hit2, vars.vol2 / 3f);
            col.gameObject.GetComponent<shieldPowerUp>().vars.speed *= -1;
        }
        if (col.gameObject.GetComponent<meteoriteController>() != null && col.gameObject.GetComponent<meteoriteController>().hasBounced == true)
        { IsPermaStunned = true; }

    }
}
