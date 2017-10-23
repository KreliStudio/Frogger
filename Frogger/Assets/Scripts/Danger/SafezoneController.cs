using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafezoneController : SegmentController
{

    public int offset;
    public float rate;

    public override void Init()
    {
        int id = 0;
        int startPos = (int)startPivot.position.x - offset;
        int endPos = (int)endPivot.position.x + offset;

        for (int i = startPos; i <= endPos; i++)
        {
            float randValue = Random.Range(0.0f, 1-(Mathf.Abs(i) + 0.1f) * rate);
            if (randValue < 0.1f)
            {
                id = Random.Range(0, prefabs.Length); // random prefab to spawn
                instancePrefabsList.Add(Instantiate(prefabs[id], new Vector3(i, transform.position.y + 1, transform.position.z), Quaternion.identity) as Transform);
                instancePrefabsList[instancePrefabsList.Count - 1].SetParent(transform);
            }

        }



    }

    public override void Spawn()
    {    
    }

    public override void Despawn()
    {
        EZ_Pooling.EZ_PoolManager.Despawn(transform);
    }
}
    

    
    
    
        

    


