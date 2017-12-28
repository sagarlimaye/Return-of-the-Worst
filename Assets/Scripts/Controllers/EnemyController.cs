using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum Attacks { single, dual };
    public Attacks attack = Attacks.single;
    public GameObject player;
    public float speed = 10f;
    public float fightDistance = 1f;
    public float maxRange = 10f;
    public JointController joint;
    public float swingTime = 1f;

    public Animator sword, anim;
    private PlayerSuper playerSuper;

    private float timeLastSwing = 1f;
    private bool facingRight = true;
    private BossHealth health;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        playerSuper = player.GetComponentInChildren<PlayerSuper>();
        anim = GetComponentInChildren<Animator>();
        health = GetComponentInChildren<BossHealth>();
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

            if (distance > fightDistance && distance <= maxRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && health.CurrentHealth > 1)
            {

                direction = direction / distance;

                direction *= Time.deltaTime * speed;
                this.transform.position += direction;
                
                    anim.SetTrigger("Walk");
                //Debug.Log("position = " + this.transform.position + ", direction = " + direction);
            }
            else
            {
                //Debug.Log((distance > fightDistance) + ", " + (distance <= maxRange) + ", " + (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) + ", " + (health.CurrentHealth > 1));
                
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


            if (timeLastSwing >= swingTime && distance <= fightDistance)
            {

                if (attack == Attacks.single)
                {
                    anim.SetTrigger("Attack");
                }
                else
                {
                    if (Random.value > 0.5)
                    {
                        anim.SetTrigger("Attack");
                    }
                    else
                    {
                        anim.SetTrigger("Attack2");
                    }
                }
                //joint.doSwing();
                timeLastSwing = 0f;
                
            }
            else
            {
                timeLastSwing += Time.deltaTime;
            }
            //Debug.Log("time = " + timeLastSwing);
        }
        else
        {
            Debug.Log("player is null");
        }
        
		
	}
}
