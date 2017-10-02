using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController2 : SegmentController
{


    public override void Init()
    {
        int id = 0;
        if (Random.value > 0.5f)
        {
            instancePrefabsList = new Transform[2];
            // create first car 
            id = Random.Range(0, prefabs.Length);
            instancePrefabsList[0] = Instantiate(prefabs[id], startPivot.position, Quaternion.identity) as Transform;
            instancePrefabsList[0].SetParent(transform);
            instancePrefabsList[0].gameObject.AddComponent<Car>();
            // create second car 
            id = Random.Range(0, prefabs.Length);
            instancePrefabsList[1] = Instantiate(prefabs[id], startPivot.position, Quaternion.identity) as Transform;
            instancePrefabsList[1].SetParent(transform);
            instancePrefabsList[1].gameObject.AddComponent<Car>();
        }
        else
        {
            instancePrefabsList = new Transform[1];
            // create first car 
            id = Random.Range(0, prefabs.Length);
            instancePrefabsList[0] = Instantiate(prefabs[id], startPivot.position, Quaternion.identity) as Transform;
            instancePrefabsList[0].SetParent(transform);
            instancePrefabsList[0].gameObject.AddComponent<Car>();
        }
    }


    public override void Spawn()
    {
        // random car speed
        speed = Random.Range(0.1f, 0.3f);
        for(int i=0; i < instancePrefabsList.Length; i++) { 
            instancePrefabsList[i].gameObject.GetComponent<Car>().Spawn(speed, startPivot.position, endPivot.position, 1.0f/(i+1.0f));
        }
        
    }


    public override void Despawn()
    {
        EZ_Pooling.EZ_PoolManager.Despawn(transform);
    }
}
    

    
    
    
        

    


