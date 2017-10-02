using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController2 : SegmentController
{
    public Transform railLight;
    private float timeToArrive;


    

    public override void Init()
    {
        instancePrefabsList = new Transform[2];
        // create train 
        int id = Random.Range(0, prefabs.Length);
        instancePrefabsList[0] = Instantiate(prefabs[id], startPivot.position, Quaternion.identity) as Transform;
        instancePrefabsList[0].SetParent(transform);

        // create rail light
        instancePrefabsList[1] =Instantiate(railLight, transform.position + new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity) as Transform;
        instancePrefabsList[1].SetParent(transform);

        // init Train
        instancePrefabsList[0].gameObject.AddComponent<Train>().Init(instancePrefabsList[1].GetComponent<RailLight>());

    }


    public override void Spawn()
    {
        // set time when train is arrive
        timeToArrive = Random.Range(0, 10) + 3;
        // update train properties on Spawn
        instancePrefabsList[0].GetComponent<Train>().Spawn(timeToArrive,startPivot.position,endPivot.position);
        // rotation light
        instancePrefabsList[1].rotation = transform.parent.rotation;
    }

    public override void Despawn()
    {
        instancePrefabsList[0].GetComponent<Train>().Despawn();
        instancePrefabsList[1].GetComponent<RailLight>().Despawn();
        EZ_Pooling.EZ_PoolManager.Despawn(transform);
    }


}
