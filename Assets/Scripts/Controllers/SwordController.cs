using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float damageAmount = 40;
    public bool isEnemy = true;
    public int superChargeAmount;

    private PlayerSuper playersuper;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        GameObject pa = GameObject.Find("PlayerAttributes");
        if (pa != null)
        {
            PlayerAttributes pas = pa.GetComponent<PlayerAttributes>();
            if (pas != null)
            {
                damageAmount = pas.attackDamage;
            }
        }

        playersuper = player.GetComponentInChildren<PlayerSuper>();
        superChargeAmount = 15;
    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("======================================================");
        //Debug.Log("col with" + col.gameObject.name);
        //Debug.Log("col.tag = " + col.tag + "isEnemy = " + isEnemy);
        if (col.tag == "enemy" && !isEnemy)
        {
            //Destroy(col.gameObject);
            NPCHealth enemyHP = col.GetComponent<NPCHealth>();
            if (enemyHP != null)
            {
                bool enemyDied = enemyHP.changeHealth(-damageAmount);
                if (enemyDied)
                {
                    if (playersuper == null)
                    {
                        Debug.Log(this.name + " has null reference to playersuper");
                    }
                    else
                    {
                        playersuper.changeSuper(superChargeAmount);
                    }

                }
            }
            
            //Debug.Log("enemy has been hit!");
        }
        if (col.tag == "Boss" && !isEnemy)
        {
            BossHealth enemyHP = col.GetComponent<BossHealth>();
            if (enemyHP != null)
            {
                enemyHP.changeHealth(-damageAmount);
            }

            //Debug.Log("enemy has been hit!");
        }


        if (col.tag == "Player" && isEnemy)
        {
            //Debug.Log("hit player");
            //Destroy(col.gameObject);
            //Debug.Log("col.tag = " + col.tag + "isEnemy = " + isEnemy);
            PlayerHealth playerHP = col.GetComponent<PlayerHealth>();
            if (playerHP != null)
            {
                Debug.Log("Hitting player for " + -damageAmount);
                playerHP.changeHealth(-damageAmount);
            }

            //Debug.Log("Player has been hit!");
        }

        //Debug.Log("======================================================");
    }
}
