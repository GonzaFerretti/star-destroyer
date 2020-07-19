using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BoomDeath : MonoBehaviour {
    public unitVars vars;
    private GameObject newpwrup;
	void Update () {
        if (global.isPaused == false)
        {
            float vidareal;
            vars.timer1 += Time.deltaTime;
            transform.Translate((new Vector3(0f, -0.3f, 0f)));
            vars.rng1 = Random.Range(1, 3);
            vidareal = GameObject.Find("Player").GetComponent<PlayerController>().vars.hp + FindObjectsOfType<PowerUpController>().Length;
            if (vars.timer1 >= 1.11f / 1.75)
            {
                if (global.Rnd(30) && vidareal < 4)
                {
                    newpwrup = Instantiate(vars.drop, vars.rig.position, Quaternion.identity);
                    newpwrup.name = "powerUp";
                }
                else if (global.Rnd(70) && vidareal <= 2)
                {
                    newpwrup = Instantiate(vars.drop, vars.rig.position, Quaternion.identity);
                    newpwrup.name = "powerUp";
                }
                Destroy(gameObject);
            }
        }
	}
    private void Start()
    {
        vars.InitVars(vars, gameObject);
        vars.rig.velocity = new Vector3(0f, 0f, 0f);
        vars.timer4 = Random.Range(0, 2);
        switch (vars.timer4)
        {
            case 0:
                {
                    vars.asource.PlayOneShot(vars.hit1, vars.vol1);
                    break;
                }
            case 1:
                {
                    vars.asource.PlayOneShot(vars.hit2, vars.vol1);
                    break;
                }
            case 2:
                {
                    vars.asource.PlayOneShot(vars.hit3, vars.vol1);
                    break;
                }
        

        }

    }
}
