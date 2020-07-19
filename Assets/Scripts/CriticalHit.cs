using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriticalHit : MonoBehaviour {
    public int hit = 0;
    private void Start()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "shot(Clone)")
        {
            Destroy(other.gameObject);
            transform.parent.GetComponent<TurtleController>().vars.hp -= 10;
            hit = 1;
        }
        if (other.gameObject.GetComponent<shieldPowerUp>() != null && other.gameObject.GetComponent<shieldPowerUp>().isCaptured == true)
        {
            transform.parent.GetComponent<TurtleController>().vars.hp -= 2;
            Destroy(other.gameObject);
            hit = 1;
        }
    }
    
}
