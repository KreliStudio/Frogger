using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController2 : SegmentController
{
    

    public override void Init()
    {
        // random speed
        speed = Random.Range(0.075f, 0.15f);
        // create logs with random width and calculate proportional pos X (anty overlaping)
        float x = 0;
        // random width first log
        int width = Random.Range(1, prefabs.Length + 1);
        // width map
        float mapWidth = Vector3.Distance(startPivot.position, endPivot.position);
        // if all logs width and spaces between is smaller than map width then create next log
        while ((x + width * 2) < mapWidth)
        {
            // position log 
            x += width * 2 + Random.Range(1, 4);
            // create log
            instancePrefabsList.Add(Instantiate(prefabs[width-1], startPivot.position, Quaternion.identity) as Transform);
            instancePrefabsList[instancePrefabsList.Count - 1].SetParent(transform);
            instancePrefabsList[instancePrefabsList.Count - 1].gameObject.AddComponent<Log>().Init(speed, x, startPivot.position, endPivot.position);
            // random next log width
            width = Random.Range(1, prefabs.Length + 1);
        }
    }

    public override void Spawn()
    {
        for (int i = 0; i < instancePrefabsList.Count; i++)
        {
            instancePrefabsList[i].gameObject.GetComponent<Log>().Spawn((int)transform.position.z);
        }
    }

    public override void Despawn()
    {
        EZ_Pooling.EZ_PoolManager.Despawn(transform);
    }





    
    
        

    

}
