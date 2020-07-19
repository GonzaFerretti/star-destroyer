using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class unitVars
{
    [HideInInspector]public Rigidbody rig;
    [HideInInspector]public Animator anim;
    [HideInInspector]public AudioSource asource;
    [HideInInspector]public Transform trans;
    [HideInInspector]public SpriteRenderer rend;
    public float speed;
    public float rng1, rng2, rng3, rng4;
    public float MaxX, MinX, MaxY, MinY;
    public GameObject shot;
    public GameObject drop;
    public Transform sspawn1;
    public Transform sspawn2;
    public Transform sspawn3;
    public AudioClip shotsound;
    public AudioClip hit1;
    public AudioClip hit2;
    public AudioClip hit3;
    public GameObject deathanim;
    [Range(0, 1)]
    public float vol1;
    [Range(0, 1)]
    public float vol2;
    [Range(0, 1)]
    public float vol3;
    [Range(0, 1)]
    public float vol4;
    public float hp;
    public float timer1;
    public float timer2;
    public float timer3;
    public int timer4;
    public void InitVars(unitVars vars, GameObject go)
    {
        if (go.GetComponent<Rigidbody>() != null)
        {
            vars.rig = go.GetComponent<Rigidbody>();
        }
        if (go.GetComponent<Animator>() != null)
        { vars.anim = go.GetComponent<Animator>(); }
        if (go.GetComponent<AudioSource>() != null)
        { vars.asource = go.GetComponent<AudioSource>(); }
        if (go.GetComponent<Transform>() != null)
        { vars.trans = go.GetComponent<Transform>(); }
        if (go.GetComponent<SpriteRenderer>() != null)
        { vars.rend = go.GetComponent<SpriteRenderer>(); }
    }
}
public class PlayerController : MonoBehaviour {
    public unitVars vars;
    public bool firstboot, isdying;
    public ContactPoint[] lista;
    public BoxCollider colid;
    public Vector3 colpos;
    public Vector3 realspeed;
    private bool isinvul = false, canDash;
    public string lastd;
    public float angmov;
    [Header("Movement")]
    public int tiltangle;
    [Header("Shot")]
    public float delay;
    public int fireRate;
    [Header("Sound")]
    public AudioClip getPwrUp,getShield, backon;
    [Header("Inputs")]
    public float moveHorizontal;
    public float moveVertical;
    public float lasthp;
    public float maxHeight;
    public int cd = 0, mult = 1;
    void OnTriggerEnter(Collider col)
    {
        lasthp = vars.hp;
        if (col.gameObject.name == "powerUp")
        {
            
                Destroy(col.gameObject);
                vars.asource.PlayOneShot(getPwrUp, 0.2F);
                vars.hp += 1;

        }
        if (col.gameObject.GetComponent<shieldPowerUp>() != null && col.GetComponent<shieldPowerUp>().isCaptured == false)
        {
            vars.asource.PlayOneShot(getShield, 0.3F);
            col.GetComponent<shieldPowerUp>().isCaptured = true;


        }
        if (col.gameObject.name == "shot2(Clone)" && isinvul == false)
        {

            GameObject.Find("Camera").GetComponent<Animator>().Play("camera rumble");
            vars.hp -= 1;
            Destroy(col.gameObject);
            vars.asource.PlayOneShot(vars.hit1, vars.vol2);
            if (vars.hp >= 1)
            { isinvul = true;
                }
           
        }
        if (col.gameObject.name == "tshot1(Clone)" && isinvul == false)
        {
            GameObject.Find("Camera").GetComponent<Animator>().Play("camera rumble");
            vars.hp -= 2;
            Destroy(col.gameObject);
            vars.asource.PlayOneShot(vars.hit1, vars.vol2);
            if (vars.hp >= 1)
            {
                isinvul = true;
                
            }
        }
        if (col.gameObject.name == "meteorite(Clone)" && isinvul == false && col.GetComponent<meteoriteController>().hasBounced == false)
        {
            GameObject.Find("Camera").GetComponent<Animator>().Play("camera rumble");
            vars.hp -= 1;
            Destroy(col.gameObject);
            vars.asource.PlayOneShot(vars.hit2, vars.vol2);
            if (vars.hp >= 1)
            {
                isinvul = true;
                
            }
        }
        if (col.gameObject.tag == "enemy" && col.gameObject.GetComponent<meteoriteController>() == null)
        {
            if (isinvul == false)
            {
                GameObject.Find("Camera").GetComponent<Animator>().Play("camera rumble");
                vars.hp -= 1;
                vars.asource.PlayOneShot(vars.hit2, vars.vol2);
            }
            
            
            if (vars.hp >= 1)
            {
                isinvul = true;
            }
        }
        if (col.gameObject.name == "shot 3(Clone)" && isinvul == false)
        {
            GameObject.Find("Camera").GetComponent<Animator>().Play("camera rumble");
            vars.hp -= 1;
            Destroy(col.gameObject);
            vars.asource.PlayOneShot(vars.hit1, vars.vol2);
            if (vars.hp >= 1)
            {
                isinvul = true;

            }
        }
        if (vars.hp <= 0)
        {
          vars.rend.enabled = false;
          gameObject.GetComponent<BoxCollider>().enabled = false;
          isdying = true;
        }
        if (lasthp != vars.hp)
        {vars.anim.Play(Mathf.Clamp(Mathf.Floor(4 - vars.hp),0,4) + "moving");
            switch (Mathf.RoundToInt(vars.hp))
            { case 1:
                    { 
                    }
                    break;
                case 2:
                    {
                        colid.size = new Vector3(0.17f, 0.071f, 1.69f);
                        colid.center = new Vector3(0f, -0.0247f, 0.0713f);
                    }
                    break;
                case 3:
                    {
                        colid.size = new Vector3(0.21f, 0.071f, 1.69f);
                        colid.center = new Vector3(-.0191f, -0.0255f, 0.126f);
                    }
                    break;
                case 4:
                    {
                        colid.size = new Vector3(0.25f, 0.07f, 1.69f);
                        colid.center = new Vector3(0f, -0.0255f, 0.126f);
                    }
                    break;
            }
                }
        if (vars.hp == 1 && lasthp > vars.hp)
        { vars.asource.PlayOneShot(vars.hit3, vars.vol3); }
        if (lasthp == 1 && vars.hp > 1)
        { vars.asource.PlayOneShot(backon, vars.vol4);
        }
    }
    void Start()
    {
        vars.InitVars(vars, gameObject);
        delay = 0;
        fireRate = 32;
        firstboot = false;
        vars.timer1 = 0;
        cd = 0;
    }
    void Alvesre(bool bl)
    {
        if (cd <= 0)
        { cd++;
            FindObjectOfType<Camera>().gameObject.GetComponent<Transform>().eulerAngles = (bl) ? new Vector3(0, 0, -180) : new Vector3(0, 0, 0);
            foreach (shieldPowerUp spup in FindObjectsOfType<shieldPowerUp>())
            {   if (spup.isCaptured == true)
                { spup.anglearc += 180; }
                 }
           
        }
        
        }
    void Update()
    {
        if (global.isPaused == false)
        {
            if (cd >= 1)
            { cd++;}
            if (cd >= 1/ Time.deltaTime)
            {
                cd = -1;
            }
            if (cd == 30) mult = -mult;
            realspeed = new Vector3(moveHorizontal, moveVertical) * vars.speed;
            angmov = global.VectorAng(vars.rig.velocity);
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("enemy"))
            { if (go.GetComponent<Transform>().position.y > maxHeight && go.GetComponent<meteoriteController>() == null)
                { maxHeight = go.GetComponent<Transform>().position.y; }
            }
            //if (vars.trans.position.y > maxHeight)
            //{
            //    vars.trans.eulerAngles = new Vector3(0, vars.trans.eulerAngles.y, -180);
            //    vars.rend.flipX = true;
            //    Alvesre(true);
            //}
            //else
            //{ vars.trans.eulerAngles = new Vector3(0, vars.trans.eulerAngles.y, 0);
            //    vars.rend.flipX = false;
            //    Alvesre(false);
            //}
            if (isdying == true)
            {
                if (vars.timer3 == 0)
                {
                    Instantiate(vars.deathanim, vars.rig.position - new Vector3(Random.Range(-2, 2), Random.Range(0, 2), 0f), Quaternion.identity);
                    vars.rend.enabled = false;
                }
                if (vars.timer3 == 2)
                { Instantiate(vars.deathanim, vars.rig.position - new Vector3(Random.Range(-2, 0), Random.Range(-2, 0), 0f), Quaternion.identity); }
                if (vars.timer3 == 4)
                { Instantiate(vars.deathanim, vars.rig.position - new Vector3(Random.Range(-2, 0), Random.Range(-2, 0), 0f), Quaternion.identity); }
                if (vars.timer3 == 6)
                {
                    Instantiate(vars.deathanim, vars.rig.position - new Vector3(Random.Range(0, 2), Random.Range(-2, 2), 0f), Quaternion.identity);
                    Destroy(gameObject);
                }
                vars.timer3 += 1;
            }
            if ((Input.GetKeyDown("w") || (Input.GetKeyDown("s")) || (Input.GetKeyDown("a")) || (Input.GetKeyDown("d"))))
            {
                if ((firstboot == false))
                {
                    firstboot = true;
                    vars.anim.SetBool("Start", true);
                }
                else vars.anim.SetBool("Moving", true);
            }
            if ((Input.GetKeyUp("w") || (Input.GetKeyUp("s")) || (Input.GetKeyUp("a")) || (Input.GetKeyUp("d"))))
            {
                vars.anim.SetBool("Moving", false);
            }
            if (isinvul == true)
            {
                vars.timer1 += Time.deltaTime;
                if (vars.timer1 >= 0 && vars.timer1 < 0.125) vars.rend.enabled = false;
                if (vars.timer1 >= 0.125 && vars.timer1 < 0.25) vars.rend.enabled = true;
                if (vars.timer1 >= 0.25 && vars.timer1 < 0.375) vars.rend.enabled = false;
                if (vars.timer1 >= 0.375 && vars.timer1 < 0.5) vars.rend.enabled = false;
                if (vars.timer1 >= 0.5 && vars.timer1 < 0.625) vars.rend.enabled = true;
                if (vars.timer1 >= 0.625 && vars.timer1 < 0.75) vars.rend.enabled = false;
                if (vars.timer1 >= 0.75 && vars.timer1 < 0.875) vars.rend.enabled = true;
                if (vars.timer1 >= 0.875 && vars.timer1 < 1) vars.rend.enabled = false;
                if (vars.timer1 >= 1 && vars.timer1 < 1.125) vars.rend.enabled = false;
                if (vars.timer1 >= 1.125 && vars.timer1 < 1.25) vars.rend.enabled = true;
                if (vars.timer1 >= 1.25 && vars.timer1 < 1.375) vars.rend.enabled = false;
                if (vars.timer1 >= 1.375 && vars.timer1 < 1.5)
                {
                    vars.rend.enabled = true;
                    vars.timer1 = 0;
                    isinvul = false;
                }
            }
            if (delay < fireRate) delay += 1;
            if (Input.GetButton("fire") && delay == fireRate && global.isPaused == false)
            {
                GameObject shot;
                shot = Instantiate(vars.shot, vars.sspawn1.position, vars.sspawn1.rotation);
                shot.GetComponent<ShotController>().speed = (vars.trans.eulerAngles.z != 0) ? -40 : 40;
                vars.asource = GetComponent<AudioSource>();
                vars.asource.PlayOneShot(vars.shotsound, 0.1f);
                delay = 0;
            }

            if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
            { transform.Rotate(new Vector3(0f, tiltangle, 0f)); }
            if (Input.GetKeyUp("a") || Input.GetKeyUp("left"))
            { transform.Rotate(new Vector3(0f, -tiltangle, 0f)); }
            if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
            { transform.Rotate(new Vector3(0f, -tiltangle, 0f)); }
            if (Input.GetKeyUp("d") || Input.GetKeyUp("right"))
            { transform.Rotate(new Vector3(0f, tiltangle, 0f)); }
            if ((Input.GetKey("a") && Input.GetKey("d")) || (Input.GetKey("right") && Input.GetKey("left")))
            { transform.Rotate(new Vector3(0f, 0f, 0f)); }
            if (Input.GetAxis("Horizontal") == 0)
            { transform.eulerAngles = new Vector3(0f, 0f, vars.trans.eulerAngles.z); }
        }
        else global.Pause(gameObject);
    }
    void FixedUpdate()
    {
        if (global.isPaused == false)
        {
            //Dash();
            moveHorizontal = mult* Input.GetAxis("Horizontal");
            moveVertical = mult * Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * vars.speed * Time.fixedDeltaTime, moveVertical * vars.speed * Time.fixedDeltaTime);
            
            vars.rig.position += movement;
            vars.rig.position = new Vector3(Mathf.Clamp(vars.rig.position.x, vars.MinX, vars.MaxX), Mathf.Clamp(vars.rig.position.y, vars.MinY, vars.MaxY), 0f);
        }
    }
    public void Dash()
    {
        if (lastd == "a" || lastd == "d")
        {
            vars.timer4 += 1;
            if (vars.timer4 >= 0.25f / Time.fixedDeltaTime)
            {
                vars.timer4 = 0;
                lastd = "";
            }
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lastd == "")
            { lastd = "a"; }
            else
            {
                if (lastd == "d")
                {
                    lastd = "a";
                    vars.timer4 = 0;
                }
                else if (lastd == "a")
                {
                    if (vars.timer4 < 0.25f / Time.fixedDeltaTime)
                    {
                        Debug.Log(vars.timer4 * Time.fixedDeltaTime);
                        vars.rig.position -= new Vector3(5 / (vars.timer4 * Time.fixedDeltaTime) / 5, 0, 0);
                        vars.timer4 = 0;
                    }
                    lastd = "";
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lastd == "")
            { lastd = "d"; }
            else
            {
                if (lastd == "a")
                {
                    lastd = "d";
                    vars.timer4 = 0;
                }
                else if (lastd == "d")
                {
                    if (vars.timer4 < 0.25f / Time.fixedDeltaTime)
                    {
                        Debug.Log(vars.timer4 * Time.fixedDeltaTime);

                        vars.rig.position += new Vector3(5 / (vars.timer4 * Time.fixedDeltaTime) / 5, 0, 0);
                        vars.timer4 = 0;
                    }

                    lastd = "";
                }
            }
        }
    }
    } 
