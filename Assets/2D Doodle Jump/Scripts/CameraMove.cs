using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // Get the midpoint of the camera and the targetPos
        Vector3 targetViewportPos = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector3 deltaPos = target.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, targetViewportPos.z));
        Vector3 targetPos = transform.position + deltaPos;

        // If the targetpos > camerapos then move the camera up
        targetPos.x = 0;
        if(targetPos.y > transform.position.y)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, .5f);
        }
    }
}
