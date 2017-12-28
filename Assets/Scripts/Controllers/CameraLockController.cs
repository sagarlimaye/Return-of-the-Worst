using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLockController : MonoBehaviour
{
    public PlayerController player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "CameraEvent")
        {
            Debug.Log("locking Camera!");
            player.cam.GetComponent<CameraController>().setToFollow(col.gameObject);
            player.following = col.gameObject;
            player.isLocked = true;
            //cam.GetComponent<CameraController>().toFollow = col.gameObject;
        }
    }

    public void cameraEventDone()
    {
        player.cam.GetComponent<CameraController>().setToFollow(this.gameObject);
        player.isLocked = false;
        Debug.Log("Did unlock");
    }
}
