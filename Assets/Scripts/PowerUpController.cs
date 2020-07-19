using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {
    public Rigidbody rb;
    public int speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (global.isPaused == false)
        {
            rb.position -= new Vector3(0f, speed * Time.deltaTime);
        }
	}
}
