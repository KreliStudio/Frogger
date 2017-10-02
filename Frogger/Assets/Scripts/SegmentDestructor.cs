using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentDestructor : MonoBehaviour
{
    public Transform segmentParent;


    void Update()
    {
        if (GameManager.instance.meters > transform.position.z + 10)
        {
            if (segmentParent.GetComponent<SegmentController>() != null)
                segmentParent.GetComponent<SegmentController>().Despawn();
            else
                EZ_Pooling.EZ_PoolManager.Despawn(segmentParent);

        }
    }



}
