using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    
    public float originSpeed;
    private float speed;
    private float lerpTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 streamInPos;
    private Vector3 streamOutPos;

    // when player go out river stream
    public GameObject stickedPlayer;



    public void Init(float sp, float x, Vector3 strInPos, Vector3 strOutPos)
    {
        //startPos = sPos;
        //endPos = ePos;
        streamInPos = strInPos;
        streamOutPos = strOutPos;
        originSpeed = sp;
        speed = originSpeed;
        lerpTime = x / Vector3.Distance(streamInPos, streamOutPos);
        transform.rotation = transform.parent.rotation;
    }

    public void Spawn(Vector3 sPos, Vector3 ePos)
    {
        startPos = sPos;
        endPos = ePos;
    }



    void Update()
    {
        Move();
    }


    public void StickPlayer(GameObject player)
    {
        stickedPlayer = player;
    }

    public void Move()
    {
        if (transform.position.x > streamInPos.x && transform.position.x < streamOutPos.x)
        {
            speed = originSpeed;
        }
        else
        {
            speed = originSpeed * 5;
        }

        // smooth move animation
        lerpTime += Time.deltaTime * speed;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos, lerpTime);

        if (lerpTime >= 1)
        {
            if (stickedPlayer != null)
            {
                stickedPlayer.transform.GetChild(0).gameObject.SetActive(false); // turn off player model
            }
            // reset animation
            transform.position = startPos;
            lerpTime = 0;
        }
    }


}
