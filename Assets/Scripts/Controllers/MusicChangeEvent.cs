using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeEvent : MonoBehaviour
{
    public AudioSource oldMusic;
    public AudioSource newMusic;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            oldMusic.Stop();
            newMusic.Play();
            Destroy(this.gameObject);
            //cam.GetComponent<CameraController>().toFollow = col.gameObject;
        }
    }
}
