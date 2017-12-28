using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWiperController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("col with" + col.gameObject.name);
        //Debug.Log("col.tag = " + col.tag + "isEnemy = " + isEnemy);
        if (col.tag == "enemy")
        {
            //Destroy(col.gameObject);
            NPCHealth enemyHP = col.GetComponent<NPCHealth>();
            if (enemyHP != null)
            {
                enemyHP.changeHealth(-1000000); //ridiculously large number to show it's a super attack and the boss can calculate correctly
            }

            Debug.Log("Screen wiper hit enemy");
        }
        else if (col.tag == "Boss")
        {
            //Destroy(col.gameObject);
            BossHealth enemyHP = col.GetComponent<BossHealth>();
            if (enemyHP != null)
            {
                enemyHP.changeHealth(-50); 
            }

            Debug.Log("Screen wiper hit enemy");
        }
    }
}
