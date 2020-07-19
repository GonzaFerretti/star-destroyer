using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldPowerUp : MonoBehaviour {
    public unitVars vars;
    public bool isCaptured = false, isFirst;
    public Transform playertrans;
    public GameObject shield2;
    public MonoBehaviour script;
    public PlayerController pcon;
    public float angulo, rad, slidespeed, anglearc;
    private void Start()
    {
        vars.InitVars(vars, gameObject);
        playertrans = GameObject.Find("Player").GetComponent<Transform>();
        pcon = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    public void FixedUpdate()
    {
        if (global.isPaused == false)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            if (isCaptured == false)
            { vars.rig.position = new Vector3(Mathf.Clamp(vars.rig.position.x, vars.MinX, vars.MaxX), Mathf.Clamp(vars.rig.position.y, vars.MinY, vars.MaxY), 0f); }
            if (GameObject.Find("Player") == null) Destroy(gameObject);
            if (isCaptured == true)
            {

                if (isFirst == true)
                {
                    shield2 = Instantiate(vars.drop, vars.trans.position, Quaternion.identity);
                    shield2.GetComponent<shieldPowerUp>().isCaptured = true;
                    shield2.GetComponent<shieldPowerUp>().isFirst = false;
                    shield2.GetComponent<shieldPowerUp>().angulo = -angulo;
                    isFirst = false;
                }
                vars.trans.position = playertrans.position + new Vector3(Mathf.Cos((angulo + 90) * Mathf.Deg2Rad), Mathf.Sin((angulo + 90) * Mathf.Deg2Rad), 0f) * rad;
                if (angulo >= -anglearc / 2 && angulo <= anglearc / 2)
                { angulo += vars.speed; }
                angulo = Mathf.Clamp(angulo, -anglearc / 2, anglearc / 2);
                if (angulo <= -anglearc / 2 || angulo >= anglearc / 2)
                { vars.speed *= -1; }
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "eshot" && isCaptured == true)
        {
            DisableOtherShields();
            Destroy(other.gameObject);
            pcon.vars.asource.PlayOneShot(vars.hit1, vars.vol1);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "enemy" && isCaptured == true && other.gameObject.GetComponent<meteoriteController>() == null)
        {
            pcon.vars.asource.PlayOneShot(vars.hit2, vars.vol2);
        }
        if (other.gameObject.GetComponent<meteoriteController>() != null && other.gameObject.GetComponent<meteoriteController>().hasBounced == false && isCaptured == true)
        {
            
        }
    }
    public void DisableOtherShields()
    {
        shieldPowerUp[] list;
        list = FindObjectsOfType<shieldPowerUp>();
        foreach (shieldPowerUp element in list)
        { element.gameObject.GetComponent<SphereCollider>().enabled = false; }
        gameObject.GetComponent<SphereCollider>().enabled = true;
    }
}

