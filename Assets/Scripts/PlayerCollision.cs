using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public int mult;
    public bool trigg;
    private void Start()
    
    {
        mult = 1;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player"){
            Destroy(gameObject);
            mult += 1;
            trigg = true; 
        }
    }
}
