using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhController : MonoBehaviour {
    public unitVars vars;
    public bool tp = false, hasanim = false;
    private float timeround;
    private float steps, speedtime, timetaken;
    private float playerposx;
    private int rng1,rng2;
    [Range(0, 10)]
    public float timescal;
	void Start () {
        vars.InitVars(vars, gameObject);
        speedtime = Random.Range(1, 10) / 10f;
        timetaken = Random.Range(11, 15) / 10f - speedtime;
        rng1 = Random.Range(2, 4);
        rng2 = Random.Range(6, 10);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "shot(Clone)")
        { Destroy(col.gameObject);
          vars.hp -= 1;
            vars.asource.PlayOneShot(vars.hit1, vars.vol1);
        }
        if ((col.gameObject.GetComponent<shieldPowerUp>() != null && col.gameObject.GetComponent<shieldPowerUp>().isCaptured == true) || (col.gameObject.GetComponent<meteoriteController>() != null && col.gameObject.GetComponent<meteoriteController>().hasBounced == true))
        {
            vars.hp -= 2;
            Destroy(col.gameObject);
        }
        if (vars.hp <= 0)
        {
            vars.rig.velocity = new Vector3(0f, 0f, 0f);
            Destroy(gameObject);
            Instantiate(vars.deathanim, transform.position, Quaternion.identity);
            global.Death(4);
        }
    }
    void FixedUpdate () {
        if (global.isPaused == false)
        {
            if (GameObject.Find("Player") == null)
            {
                Destroy(gameObject);
            }
            vars.trans.eulerAngles = (Player.trans.position.y > vars.trans.position.y) ? new Vector3(0, 0, -180) : new Vector3(0, 0, 0);
            vars.rend.flipX = (Player.trans.position.y - 5 > vars.trans.position.y) ? true : false;
            Time.timeScale = timescal;
            vars.timer1 += Time.deltaTime;
            timeround = Mathf.Round(vars.timer1 * 10) / 10;
            steps = 8 / (60 * speedtime);
            if (timeround > timetaken && tp == false)
            {
                vars.anim.speed += steps;
                if (vars.anim.speed >= 8)
                {
                    tp = true;
                    vars.anim.speed = 1f;
                    vars.anim.SetBool("tp", true);
                }
            }
            if (tp == true)
            {
                vars.timer2 += 1;

                if (vars.timer2 == 3 || vars.timer2 == 3 + rng1 || vars.timer2 == 3 + rng2)
                {
                    GameObject shot;
                    shot = Instantiate(vars.shot, vars.sspawn1.position, vars.sspawn1.localRotation);
                    shot.GetComponent<phshot>().speed = (vars.trans.eulerAngles.z != 0) ? -28 : 28;
                    vars.asource.PlayOneShot(vars.shotsound, vars.vol2);
                    shot = Instantiate(vars.shot, vars.sspawn2.position, vars.sspawn2.localRotation);
                    shot.GetComponent<phshot>().speed = (vars.trans.eulerAngles.z != 0) ? -28 : 28;
                    shot = Instantiate(vars.shot, vars.sspawn3.position, vars.sspawn3.localRotation);
                    shot.GetComponent<phshot>().speed = (vars.trans.eulerAngles.z != 0) ? -28 : 28;

                }
                if (vars.timer2 > 19)
                {
                    float ycoor;
                    vars.anim.SetBool("tp", false);
                    tp = false;
                    vars.timer2 = 0;
                    playerposx = GameObject.Find("Player").GetComponent<Transform>().position.x;
                    ycoor = Random.Range(20, 42) - 24.5f;
                    ycoor = (vars.trans.eulerAngles.z != 0) ? -ycoor + 24.5f : ycoor + 24.5f;
                    if (playerposx > -7.5 && playerposx < 7.5)
                    {
                        if (playerposx > -7.5 && playerposx < 0)
                        { vars.trans.position = new Vector3(Mathf.Clamp(-playerposx + Random.Range(6, 10), -15, 15), ycoor, 0f); }
                        if (playerposx < 7.5 && playerposx >= 0)
                        { vars.trans.position = new Vector3(Mathf.Clamp(-playerposx - Random.Range(6, 10), -15, 15), ycoor, 0f); }
                    }

                    else vars.trans.position = new Vector3(-playerposx, ycoor, 0f);
                    vars.timer1 = 0;
                    speedtime = Random.Range(1, 10) / 10f;
                    timetaken = Random.Range(18, 25) / 10f - speedtime;
                    vars.sspawn1.transform.localEulerAngles = new Vector3(0f, 0f, Random.Range(5, 40));
                    vars.sspawn3.transform.localEulerAngles = -vars.sspawn1.transform.localEulerAngles;
                    vars.sspawn2.transform.localRotation = Quaternion.identity;
                    rng1 = Random.Range(2, 4);
                    rng2 = Random.Range(6, 10);
                }
            }
        }
	}
}
