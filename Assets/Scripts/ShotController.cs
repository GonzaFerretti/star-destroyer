using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    private float cociente;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
	void Start () {
        
}
    private void FixedUpdate()
    {
        if (global.isPaused == false)
        {
            rb.position = new Vector3(rb.position.x, rb.position.y + speed * Time.fixedDeltaTime);
            if (rb.position.y >= 47.5)
            { DestroyObject(gameObject);
            };
        }
        else global.Pause(gameObject);
    }
}
