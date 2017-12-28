using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private GameObject player;
    public float speed = 10f;
    public float fightDistance = 1f;
    public float maxRange = 10f;
    public float swingTime = 1f;


    private Animator anim;



    private float timeLastSwing = 1f;
    private bool facingRight = true;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        anim = GetComponentInChildren<Animator>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player != null)
        {
            
            Vector2 here = this.transform.position;
            Vector2 there = player.transform.position;

            Vector3 direction = there - here;
            float distance = direction.magnitude;
            //Debug.Log("dist = " + distance);



            if (timeLastSwing >= swingTime && distance <= fightDistance)
            {
                anim.SetTrigger("Attack");
                timeLastSwing = 0f;

            }
            else
            {
                timeLastSwing += Time.deltaTime;
            }

            if (distance > fightDistance && distance <= maxRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {

                direction = direction / distance;

                direction *= Time.deltaTime * speed;
                this.transform.position += direction;
                anim.SetFloat("speed", direction.magnitude);
                //Debug.Log("speed = " + direction.magnitude);
                //anim.SetTrigger("Walk");
                
                //Debug.Log("position = " + this.transform.position + ", direction = " + direction);
            }
            else
            {
                anim.SetFloat("speed", 0);
            }

            if (here.x - there.x > 0 && !facingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = true;
            }
            if (here.x - there.x < 0 && facingRight)
            {
                transform.Rotate(new Vector3(0, 180, 0));
                facingRight = false;
            }


            
            //Debug.Log("time = " + timeLastSwing);
        }
        else
        {
            Debug.Log("player is null");
        }
        
		
	}
}
