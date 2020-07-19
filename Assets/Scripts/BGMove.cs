using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour {
    private int frameCnt;
    public int transpeed;
    void Update () {
        if (global.isPaused == false)
        {
            frameCnt += 1;
            if (frameCnt % transpeed == 0)
                transform.Translate((new Vector3(0f, -1, 0f)));
            if (transform.position.y <= -32)
            { transform.position = new Vector3(transform.position.x, 76f, 50f); }
        }
	}
}
