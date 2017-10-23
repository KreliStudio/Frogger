using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offsetPosition;
    public float AxisLimitX;

    private float leftLimit;
    private float rightLimit;

    void Start()
    {
        leftLimit = offsetPosition.x - AxisLimitX;
        rightLimit = offsetPosition.x + AxisLimitX;
    }


    void Update () {
        // to modification

        Vector3 cameraPos = transform.position;
        Vector3 targetPos = target.position;
        if (target != null) {
            if (cameraPos.x < leftLimit)
            {
                cameraPos = new Vector3(leftLimit, cameraPos.y, cameraPos.z);
            }
            else
            if (cameraPos.x > rightLimit)
            {
                cameraPos = new Vector3(rightLimit, cameraPos.y, cameraPos.z);
            } else
            {
                cameraPos = Vector3.Lerp(cameraPos, target.position + offsetPosition, Time.deltaTime);
            }
        }
        transform.position = cameraPos;

    }
    

}
