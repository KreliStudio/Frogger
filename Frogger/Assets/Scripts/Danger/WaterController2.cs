using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController2 : SegmentController
{
    public Transform streamIn;
    public Transform streamOut;



    public override void Init()
    {
        // random speed
        speed = Random.Range(0.025f, 0.175f);
        // create logs with random width and calculate proportional pos X (anty overlaping)
        float x = 0;
        // random width first log
        int width = Random.Range(1, prefabs.Length + 1);
        // width map
        float mapWidth = Vector3.Distance(streamIn.position, streamOut.position)-2;
        // if all logs width and spaces between is smaller than map width then create next log
        while ((x + width * 2) < mapWidth)
        {
            // position log 
            x += width * 2;
            // create log
            instancePrefabsList.Add(Instantiate(prefabs[width-1], startPivot.position, Quaternion.identity) as Transform);
            instancePrefabsList[instancePrefabsList.Count - 1].SetParent(transform);
            instancePrefabsList[instancePrefabsList.Count - 1].gameObject.AddComponent<Log>().Init(speed, x, streamIn.position,streamOut.position);
            // random next log width
            width = Random.Range(1, prefabs.Length + 1);
        }
    }

    public override void Spawn()
    {
        for (int i = 0; i < instancePrefabsList.Count; i++)
        {
            instancePrefabsList[i].gameObject.GetComponent<Log>().Spawn(startPivot.position, endPivot.position);
        }
    }

    public override void Despawn()
    {
        EZ_Pooling.EZ_PoolManager.Despawn(transform);
    }

    



    
    
        

    

}
