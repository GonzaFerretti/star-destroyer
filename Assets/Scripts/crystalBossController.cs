using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalBossController : MonoBehaviour {
    public unitVars vars;
    public Animation hit;
    public int boolwait;
    private void Start()
    {
        vars.InitVars(vars, gameObject);   
    }
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
        vars.anim.SetBool("hit", true);
    }
    private void Update()
    {
        if (vars.anim.GetBool("hit"))
        { boolwait += 1;
          if (boolwait > 1)
            { boolwait = 0;
              vars.anim.SetBool("hit", false);
            }
        }
        

    }
}
