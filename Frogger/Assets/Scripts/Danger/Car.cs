using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {

    private float carSpeed;
    private float lerpTime;
    private Vector3 startPos;
    private Vector3 endPos;

    // when player jump on vehicle
    public GameObject stickedPlayer;
    

    public void Spawn(float speed, Vector3 sPos, Vector3 ePos, float lTime)
    {
        carSpeed = speed;
        startPos = sPos;
        endPos = ePos;
        lerpTime = lTime;
        transform.rotation = transform.parent.rotation;
    }
    
    public void Update()
    {
        Move();
    }

    public void StickPlayer(GameObject player)
    {
        stickedPlayer = player;
    }


    public void Move()
    {
        // smooth move animation
        lerpTime += Time.deltaTime * carSpeed;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos, lerpTime);

        if (lerpTime >= 1)
        {
            if (stickedPlayer != null)
                Destroy(stickedPlayer);
            // reset animation
            transform.position = startPos;
            lerpTime = 0;
        }
    }


}
