using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentDestructor : MonoBehaviour {

    
    void Start()
    {
        StartCoroutine("Destroy");
    }

    IEnumerator Destroy()
    {
        // wait 5 sec
        yield return new WaitForSeconds(5);
        // if segment is behind player then destroy obj
        if (GameManager.instance.meters > transform.position.z + 40)
        {
            DestroyObject(gameObject);
        }
        else
        {
            StartCoroutine("Destroy");
        }
    }
}
