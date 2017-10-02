using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Segment {

    [Range(0.0f, 10.0f)]
    public float probability; // multiplier chance to create
    [Range(0.0f, 10.0f)]
    public float intensity; // multiplier length segment
    public Transform prefab;  // prefab, in future array od prefabs
    

    public GameObject Create(long pos, float dificulty)
    {
        // set random segment from prefabs array
        // xxxxxx
        //GameObject createdSegment = GameObject.Instantiate(prefab, new Vector3(0.0f, -1.0f, pos), Quaternion.identity) as GameObject;
        Transform createdSegment = EZ_Pooling.EZ_PoolManager.Spawn(prefab, new Vector3(0.0f, -1.0f, pos), Quaternion.identity);
        // add segment destructor component to clearing map behind player
        //createdSegment.gameObject.AddComponent<SegmentDestructor>();
        
        // random inverse segment
        if (Random.value > 0.5f)
        {
            // inverse segment
            createdSegment.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
        }
        // after spawn segment update custom properties
        if (createdSegment.GetComponent<SegmentController>() != null)
            createdSegment.GetComponent<SegmentController>().Spawn(); 

        return createdSegment.gameObject;
    }
}
