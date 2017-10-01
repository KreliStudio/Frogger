using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour {
    
    private float speed;
    private float lerpTime;
    private Vector3 startPos;
    private Vector3 endPos;


    public void Init(float sp, float x, Vector3 sPos, Vector3 ePos)
    {
        speed = sp;
        startPos = sPos;
        endPos = ePos;
        lerpTime = x/Vector3.Distance(startPos,endPos);
        transform.rotation = transform.parent.rotation;
    }

    public void Update()
    {
        Move();
    }




    public void Move()
    {
        // smooth move animation
        lerpTime += Time.deltaTime * speed;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos, lerpTime);

        if (lerpTime >= 1)
        {
            // reset animation
            transform.position = startPos;
            lerpTime = 0;
        }
    }


}
