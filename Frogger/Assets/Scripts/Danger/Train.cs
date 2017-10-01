using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    
    private float timeToArrive;
    private float lerpTime;
    private Vector3 startPos;
    private Vector3 endPos;

    // when player jump on vehicle
    public GameObject stickedPlayer;
    public void Init(float tToArr, Vector3 sPos, Vector3 ePos)
    {
        timeToArrive = tToArr;
        startPos = sPos;
        endPos = ePos;
        lerpTime = 0;
        transform.rotation = transform.parent.rotation;
        Invoke("ArriveTrain", timeToArrive);
        // 2sec before arrive tru on alert (pulse light)
        Invoke("Alert", timeToArrive-2.0f);
    }

    private void ArriveTrain()
    {
        StartCoroutine(Move());
    }
    private void Alert()
    {
        // send alert to rail light
        transform.parent.GetComponent<TrackController>().railLight.SendMessage("Alert", SendMessageOptions.DontRequireReceiver);
    }

    public void StickPlayer(GameObject player)
    {
        stickedPlayer = player;
    }

    private IEnumerator Move()
    {
        yield return new WaitForFixedUpdate();
        // smooth move animation
        lerpTime += Time.deltaTime*1.5f;
        gameObject.transform.position = Vector3.Lerp(startPos, endPos, lerpTime);

        if (lerpTime >= 1)
        {
            if (stickedPlayer != null)
                Destroy(stickedPlayer);
            // reset animation
            transform.position = startPos;
            lerpTime = 0;
            Invoke("ArriveTrain", timeToArrive);
            // 2sec before arrive tru on alert (pulse light)
            Invoke("Alert", timeToArrive - 2.0f);
        }
        else
        {
            StartCoroutine(Move());
        }
    }
}
