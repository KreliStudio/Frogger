using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Structure {

    public float probability; // multiplier chance to create
    public int length; // length of structure
    public GameObject prefab;  // prefab, in future array od prefabs
    
    public GameObject Create(long pos)
    {
        // set random segment from prefabs array
        // xxxxxx


        GameObject createdStructure = GameObject.Instantiate(prefab, new Vector3(0.0f, -1.0f, pos), Quaternion.identity) as GameObject;
        // add segment destructor component to clearing map behind player
        createdStructure.AddComponent<SegmentDestructor>();
        

        return createdStructure;
    }
}
