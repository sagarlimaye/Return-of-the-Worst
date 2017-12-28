using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private PlayerHealth health;
    public int pickupValue;
    public static event ScoreTracker.ScoreUpdate onPickupObtained;
    void Start()
    {
        health = GameObject.Find("Player").GetComponentInChildren<PlayerHealth>();
        Debug.Log(health);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            
            health.changeHealth(pickupValue);
            if(onPickupObtained!=null)
                onPickupObtained();
            Destroy(this.gameObject);
        }
    }

}
