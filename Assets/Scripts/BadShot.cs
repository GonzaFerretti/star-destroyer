using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadShot : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
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
            rb.position = new Vector3(rb.position.x, rb.position.y + -speed * Time.fixedDeltaTime);
            if (rb.position.y < -2)
            { DestroyObject(gameObject); };
        }
    }
}
