using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{

    public float StartingHealth = 100;

    public int killValue;
    public float CurrentHealth;

    private Animator anim;


    public static event ScoreTracker.ScoreUpdate onNPCKilled;
    // Use this for initialization
    void Start()
    {
        CurrentHealth = StartingHealth;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //changeHealth(-0.3f);

    }

    public bool changeHealth(float diff) //negative is damage, positive is healing
    {
        if (diff < 0)
        {
            anim.SetTrigger("Pain");
        }
        if (CurrentHealth > 0)
        {
            CurrentHealth += diff;
            Debug.Log("health is now " + CurrentHealth);

        }

        if (CurrentHealth <= 0)
        {
            //Player Died, do something
            Debug.Log("NPC Has Died");
            Destroy(this.gameObject);
            if (onNPCKilled != null)
                onNPCKilled();
            return true; //this character died, tell the caller
        }

        return false; //this character did not die
    }
}
