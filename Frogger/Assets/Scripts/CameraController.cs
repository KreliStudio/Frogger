using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 offset;
    private Vector3 pos;
    

	void Update () {
        if (!GameManager.instance.isDead)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime);
        }
       

	}

    
}
