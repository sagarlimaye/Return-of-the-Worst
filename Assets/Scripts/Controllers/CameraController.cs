using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject toFollow;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - toFollow.transform.position;
    }

    void LateUpdate()
    {
        //transform.position = toFollow.transform.position + offset;
        Vector3 v = toFollow.transform.position;
        Vector3 pos = new Vector3(v.x, 0, -10);
        transform.position = pos;
    }
    public void setToFollow(GameObject gm)
    {
        toFollow = gm;
    }
}