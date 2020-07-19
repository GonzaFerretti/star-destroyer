using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class healthbar
{ public float maxhp; }
public class GyroController : MonoBehaviour {
    public unitVars vars;
    public int fireInterval;
    private float fireCounter;
    private float fireDelay;
    [Range(1,60)]
    public int fireRate;
    public double firingTime;
    [Header("Movement Pattern")]
    public int minMovement;
    public int maxMovement;
    public int xmov;
    private float ymov;
    public float posfail;
    public float posfailcomp;
    public bool isfail = false;
    public float limit;
    public bool direccionCalculada= false;
    public string lado;
    // Use this for initialization
    void Start () {
        vars.InitVars(vars, gameObject);
        xmov = Random.Range(minMovement, maxMovement);
        ymov = Random.Range(minMovement-1, maxMovement);
        vars.speed = Mathf.Clamp(Mathf.Min(xmov,ymov)*2,1,5);
        vars.hp = 3;
        fireDelay = 60 / fireRate;
        fireInterval = Mathf.Clamp(xmov - 1,2,4);
        fireCounter = 0;
        fireRate = Random.Range(4, 6);
        firingTime = 0.6f;
    }


    // Update is called once per frame
    void Update() {
        if (global.isPaused == false)
        {
            vars.timer1 += Time.deltaTime;
            vars.timer2 += Time.deltaTime;
            vars.trans.eulerAngles = (Player.trans.position.y> vars.trans.position.y) ? new Vector3(0, 0, -180) : new Vector3(0, 0, 0);
            vars.rend.flipX = (Player.trans.position.y - 5 > vars.trans.position.y) ? true : false;
            if (GameObject.Find("Player") == null)
            {
                xmov = 0;
                ymov = 0;
                fireInterval = 0;
                Destroy(gameObject);
            }
            if (posfail != vars.rig.position.x)
            {
                posfail = vars.rig.position.x;
                vars.timer4 = 0;
            }
            if (posfail == vars.rig.position.x)
            { vars.timer4++; }
            if (vars.timer4 >= 3)
            {
                vars.timer2 = 0;
                direccionCalculada = false;
            }
            if (vars.rig.position.x < 0 && direccionCalculada == false)
            {
                xmov = Mathf.Abs(xmov);
                limit = (27 - (Mathf.RoundToInt(13.5f + vars.rig.position.x))) / (xmov * vars.speed);
                direccionCalculada = true;
                lado = "izq";
            }
            if (vars.rig.position.x > 0 && direccionCalculada == false)
            {
                limit = (27 - (Mathf.RoundToInt(13.5f + vars.rig.position.x))) / (xmov * vars.speed);
                limit = 27 / (xmov * vars.speed) - limit;
                xmov *= -1;
                direccionCalculada = true;
                lado = "der";
            }

            if (vars.timer2 < limit)
            {
                if (vars.timer1 < 1)
                { vars.rig.velocity = new Vector3(xmov * vars.speed, -ymov * vars.speed, 0f); }
                if (vars.timer1 >= 1)
                {
                    vars.rig.velocity = new Vector3(xmov * vars.speed, ymov * vars.speed, 0f);
                }
                if (vars.timer1 >= 2)
                { vars.timer1 = 0; }
            }
            if (vars.timer2 >= limit)
            {
                if (vars.timer1 < 1)
                { vars.rig.velocity = new Vector3(-xmov * vars.speed, -ymov * vars.speed, 0f); }
                if (vars.timer1 >= 1)
                {
                    vars.rig.velocity = new Vector3(-xmov * vars.speed, ymov * vars.speed, 0f);
                }
                if (vars.timer1 >= 2)
                { vars.timer1 = 0; }

            }
            //if (vars.timer2 >= 2 * limit) {
            if ((vars.rig.position.x > 13 && lado == "izq") || (vars.rig.position.x < -13 && lado == "der"))
            {
                vars.timer2 = 0;
                direccionCalculada = false;
            }
            //} 


            vars.rig.position = new Vector3(Mathf.Clamp(vars.rig.position.x, vars.MinX, vars.MaxX), Mathf.Clamp(vars.rig.position.y, vars.MinY, vars.MaxY), 0f);
            vars.timer3 += Time.deltaTime;
            if (Mathf.Round(vars.timer3) % fireInterval == 0 && vars.timer3 - Mathf.Round(vars.timer3) < firingTime && vars.timer3 != 0)
            {
                fireCounter += 1;
                if (fireCounter == fireDelay && global.isPaused == false)
                {
                    GameObject shot;
                    vars.asource.PlayOneShot(vars.shotsound, 1);
                    shot = Instantiate(vars.shot, vars.sspawn1.position, vars.trans.rotation);
                    shot.GetComponent<BadShot>().speed = (vars.trans.eulerAngles.z != 0) ? -40 : 40;
                    fireCounter = 0;
                }
            };
        }
        else global.Pause(gameObject);
    }
    void OnTriggerEnter(Collider col)
    {if (col.gameObject.name == "shot(Clone)")
        { vars.hp -= 1;
            Destroy(col.gameObject);
            vars.asource.PlayOneShot(vars.hit1, 0.1f);
            if (vars.hp <= 0)
            {
                vars.rig.velocity = new Vector3(0f, 0f, 0f);
                Destroy(gameObject);
                Instantiate(vars.deathanim, transform.position, Quaternion.identity);
                global.Death(1);
            }
            else 
            if (vars.anim.GetBool("IsDamaged") == false && vars.hp == 1)
            {
                vars.anim.SetBool("IsDamaged", true);
                if (vars.speed > 1) vars.speed -= 1;
                vars.sspawn1.transform.up = new Vector3(0f,-2f,0f);
                vars.asource.PlayOneShot(vars.hit2, 0.7f);
            }
             }
        if (col.gameObject.GetComponent<shieldPowerUp>() != null && col.gameObject.GetComponent<shieldPowerUp>().isCaptured == true)
        { vars.hp -= 2;
            Destroy(col.gameObject);
            if (vars.hp <= 0)
            {
                vars.rig.velocity = new Vector3(0f, 0f, 0f);
                Destroy(gameObject);
                Instantiate(vars.deathanim, transform.position, Quaternion.identity);
                global.Death(1);
            }
            else
              if (vars.anim.GetBool("IsDamaged") == false && vars.hp == 1)
            {
                vars.anim.SetBool("IsDamaged", true);
                if (vars.speed > 1) vars.speed -= 1;
                vars.sspawn1.transform.up = new Vector3(0f, -2f, 0f);
                vars.asource.PlayOneShot(vars.hit2, 0.7f);
            }
        }
        if (col.gameObject.GetComponent<meteoriteController>() != null && col.gameObject.GetComponent<meteoriteController>().hasBounced == true)
        {
            vars.hp = 0;
            if (vars.hp <= 0)
            {
                vars.rig.velocity = new Vector3(0f, 0f, 0f);
                Destroy(gameObject);
                Instantiate(vars.deathanim, transform.position, Quaternion.identity);
                global.Death(1);
            }
            else
              if (vars.anim.GetBool("IsDamaged") == false && vars.hp == 1)
            {
                vars.anim.SetBool("IsDamaged", true);
                if (vars.speed > 1) vars.speed -= 1;
                vars.sspawn1.transform.up = new Vector3(0f, -2f, 0f);
                vars.asource.PlayOneShot(vars.hit2, 0.7f);
            }
        }
    }
}
