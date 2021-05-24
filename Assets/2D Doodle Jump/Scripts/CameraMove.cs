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
        Vector3 targetPos = Camera.main.WorldToViewportPoint(target.transform.position);
        Vector3 deltaPos = target.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(.5f, .5f, targetPos.z));
        Vector3 targetDir = transform.position + deltaPos;

        targetDir.x = 0;
        if(targetDir.y > transform.position.y)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetDir, ref velocity, .5f);
        }
    }
}
