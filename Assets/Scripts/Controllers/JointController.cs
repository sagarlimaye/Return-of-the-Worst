using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointController : MonoBehaviour
{

    public float swingTime = .25f;
    public float returnTime = .1f;
    public float swingAmount = 90;
    public bool reverse = false;

    private float remaining = 0;
    private bool isSwing = false;
    private bool swung = false;

    void FixedUpdate()
    {
        if (isSwing)
        {
            float toTurn = Mathf.Min((swingAmount * Time.deltaTime) / swingTime, remaining);
            //Debug.Log(toTurn);
            remaining -= toTurn;
            if (reverse) transform.Rotate(0, 0, toTurn);
            else transform.Rotate(0, 0, -toTurn);
            if (remaining <= 0)
            {
                swung = true;
                isSwing = false;
                remaining = swingAmount;
                this.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        if (swung)
        {
            float toTurn = Mathf.Min((swingAmount * Time.deltaTime) / returnTime, remaining);
            //Debug.Log(toTurn);
            remaining -= toTurn;
            if (reverse) transform.Rotate(0, 0, -toTurn);
            else transform.Rotate(0, 0, toTurn);
            if (remaining <= 0)
            {
                swung = false;
                isSwing = false;
                remaining = 0;
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("CTRL PRESSED");
            
        }
    }
    public void doSwing()
    {
        if (!isSwing && !swung)
        {
            this.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            //Debug.Log("swinging");
            remaining = swingAmount;
            isSwing = true;
        }
    }
}
