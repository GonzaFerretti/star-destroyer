using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoom : MonoBehaviour {
    public float timer;
    public AudioSource source;
    public int rnd, rnd1;
    [Header("Sound")]
    [Range(0, 1)]
    public float volume;
    public AudioClip clip,clip2,clip3;
    public Rigidbody rb;
    public GameObject powerup;
    public Transform trans;
	void Update () {
        timer += Time.deltaTime;
        if (timer > 1.11f)
        {
            Destroy(gameObject);
        }
	}
    private void Start()
    {
        trans = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, 0f);
        rnd1 = Random.Range(5, 15);
        trans.localScale = new Vector3(rnd1, rnd1, 0f);
        source = GetComponent<AudioSource>();
        rnd = Random.Range(0, 2);
        switch (rnd)
        {
            case 0:
                {
                    source.PlayOneShot(clip, volume);
                    break;
                }
            case 1:
                {
                    source.PlayOneShot(clip2, volume);
                    break;
                }
            case 2:
                {
                    source.PlayOneShot(clip3, volume);
                    break;
                }

        }
        
    }
}
