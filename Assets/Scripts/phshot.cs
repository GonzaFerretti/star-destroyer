using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phshot : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (global.isPaused == false)
        {
            rb.velocity = transform.up * -speed;
            if (rb.position.y < -2)
            { DestroyObject(gameObject); }
        }
        else global.Pause(gameObject);
    }
}
