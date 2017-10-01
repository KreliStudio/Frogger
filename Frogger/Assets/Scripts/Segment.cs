using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Segment {

    public float probability; // multiplier chance to create
    public float intensity; // multiplier length segment
    public GameObject prefab;  // prefab, in future array od prefabs
    

    public GameObject Create(long pos, float dificulty)
    {
        // set random segment from prefabs array
        // xxxxxx
        

        GameObject createdSegment = GameObject.Instantiate(prefab, new Vector3(0.0f, -1.0f, pos), Quaternion.identity) as GameObject;
        // add segment destructor component to clearing map behind player
        createdSegment.AddComponent<SegmentDestructor>();
        
        // random inverse segment
        if (Random.value > 0.5f)
        {
            // inverse segment
            createdSegment.transform.rotation = Quaternion.Euler(0.0f,180.0f,0.0f);
        }
        createdSegment.SendMessage("Init", dificulty, SendMessageOptions.DontRequireReceiver);

        return createdSegment;
    }
}
