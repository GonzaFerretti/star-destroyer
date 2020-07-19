using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turtleShot : MonoBehaviour {
    private Rigidbody rb;
    public float speed, timer, fireSec, modifier = 1;
    private Vector3 lastangle;
    public GameObject Turtle;
    public TurtleController user;
    private Animator anim;
    public Transform trans;
    public bool gotspeed = false;
    public AudioSource asource;
    public AudioClip charge, fire;
    [Range(0, 1)]
    public float volume;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        asource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (global.isPaused == false)
        {
            if (GameObject.Find("Player") == null) Destroy(gameObject);
            trans.localEulerAngles = (Turtle != null) ? Turtle.GetComponent<Transform>().eulerAngles:lastangle;
            lastangle = trans.eulerAngles;
            if (gotspeed == false && Turtle.GetComponent<TurtleController>().fireTimer > 30f)
            {
                fireSec = Mathf.Floor(Turtle.GetComponent<TurtleController>().fireTimer);
                gotspeed = true;
                asource.loop = true;
                //asource.clip = charge;
                //asource.Play();
                asource.PlayOneShot(charge, volume / 2);
            }
            if (timer <= fireSec && Turtle == null)
                { Destroy(gameObject); }
            if (timer == fireSec && gotspeed == true)
            {
                asource.loop = false;
                asource.Stop();
                asource.PlayOneShot(fire, volume / 3);
                anim.SetBool("fire", true);
                speed = (Turtle.GetComponent<Transform>().eulerAngles.z != 0) ? -70 : 70;
                Debug.Log(Turtle.GetComponent<Transform>().eulerAngles.z != 0);
            }
            if (timer >= fireSec)
            {
                rb.position -= new Vector3(0f,speed * Time.deltaTime); }
            if (timer < fireSec && gotspeed == true)
            {
                trans.position = new Vector3(Turtle.GetComponent<Rigidbody>().position.x, Turtle.GetComponentInChildren<Transform>().GetChild(0).position.y, 0f);
            }
            if (gotspeed == true)
            { timer += 1; }
            if (rb.position.y < -2)
            { DestroyObject(gameObject); };
            if (rb.position.y < Turtle.GetComponent<Transform>().position.y - 5 && anim.GetBool("fire") == true)
            {
                anim.SetBool("expand", true);
                speed /= 1.00001f;
            }
        }
        else global.Pause(gameObject);
    }
}
