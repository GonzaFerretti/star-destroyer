using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoriteController : MonoBehaviour {
    public float xmov, ymov, angulo, angprev, correccion = 0f;
    public unitVars vars;
    public bool isDestroyed = false, hasBounced = false;
    public Sprite dest;
	// Use this for initialization
	void Start () {
        vars.InitVars(vars, gameObject);
        xmov = Random.Range(4, 6);
        ymov = Random.Range(Mathf.Clamp(xmov-2,0,xmov), 10);
        vars.speed = Random.Range(2, 4);
        if (vars.trans.position.x > 0)
        {
            xmov *= -1;
            angulo = angulo - 90;
        }
        vars.speed = Random.Range(2, 6);
        UpdateRotation();
    }
	void Update () {
        if (global.isPaused == false)
        {
            if (isDestroyed == false)
            { 
                vars.rig.velocity = new Vector3(xmov * vars.speed, -ymov * vars.speed, 0f);
                UpdateRotation();
            }
            else
            { vars.rig.velocity = transform.up * -30; }
            if (vars.trans.position.y < -5 || vars.trans.position.y > 60 || vars.trans.position.x < -30f || vars.trans.position.x > 30f)
            { Destroy(gameObject); }
        }
        else global.Pause(gameObject);
        
    }
    private void UpdateRotation()
    {
        float ang = global.VectorAng(vars.rig.velocity) + 90;
        transform.eulerAngles = new Vector3(0f, 0f, ang); }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "shot(Clone)" && isDestroyed == false)
        {
            vars.anim.Play("meteorite destroyed");
            Destroy(other.gameObject);
            isDestroyed = true;
            transform.eulerAngles = new Vector3(0, 0, 0);
            vars.rig.velocity = new Vector3(0,0,0);
            vars.asource.PlayOneShot(vars.hit1, vars.vol1);
            if (global.Rnd(70) && FindObjectsOfType<shieldPowerUp>().Length == 0)
            { Instantiate(vars.drop, vars.rig.position, Quaternion.identity); }
            else if (global.Rnd(40) && FindObjectsOfType<shieldPowerUp>().Length <= 2 && FindObjectsOfType<shieldPowerUp>().Length > 0)
                { Instantiate(vars.drop, vars.rig.position, Quaternion.identity); }
            else if (global.Rnd(10) && FindObjectsOfType<shieldPowerUp>().Length > 2)
            { Instantiate(vars.drop, vars.rig.position, Quaternion.identity); }
            vars.asource.PlayOneShot(vars.hit1, vars.vol1);
        }
        if (other.gameObject.GetComponent<shieldPowerUp>() != null && isDestroyed == false && other.gameObject.GetComponent<shieldPowerUp>().isCaptured == true)
        {
            if (hasBounced == false)
            {
                Vector3 pspeed = Player.script.realspeed;
                vars.asource.PlayOneShot(vars.hit2,0.5f);
                ymov *= -1;
                if (xmov < 0)
                { xmov /= Mathf.Clamp(pspeed.x /5 , 1, 20);
                }
                else
                {
                    xmov /= Mathf.Clamp(-pspeed.x /5, 1, 20);
                }
                ymov *= Mathf.Clamp(Player.script.moveVertical * 3,1,20);
                //angulo = global.VectorAng(new Vector3(xmov, ymov));
                //transform.eulerAngles = new Vector3(0f, 0f, angulo);
                hasBounced = true;
                vars.rend.flipX = true;
            }
            else
            { vars.speed += 0.125f;
              }
        }
        if (other.gameObject.tag == "enemy" && other.gameObject.GetComponent<meteoriteController>() == null && isDestroyed == false && hasBounced == true)
        {
            vars.anim.Play("meteorite destroyed");
            isDestroyed = true;
            vars.asource.PlayOneShot(vars.hit1, vars.vol1);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    } 
}
