/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour {

    public Transform startPivot;
    public Transform endPivot;
    public Transform[] trains;
    public Transform railLight;

    private float timeToArrive;
    private Transform train;


    void Awake()
    {
    }


    public void Init(float dificulty)
    {
        // set time when train is arrive
        timeToArrive = Random.Range(0, 1 / dificulty) + 3;
        // create train 
        int id = Random.Range(0, trains.Length);
        //train = Instantiate(trains[id], startPivot.position, Quaternion.identity) as GameObject;
        train = EZ_Pooling.EZ_PoolManager.Spawn(trains[id], startPivot.position, Quaternion.identity);
        train.SetParent(transform);
        //train.gameObject.AddComponent<Train>().Init(timeToArrive, startPivot.position, endPivot.position);
        // create rail light
        //railLight = Instantiate(railLight, transform.position + new Vector3(1.0f, 0.5f, -0.5f), Quaternion.identity) as GameObject;
        railLight = EZ_Pooling.EZ_PoolManager.Spawn(railLight, transform.position + new Vector3(1.0f, 0.5f, -0.5f), Quaternion.identity);
        railLight.SetParent(transform);
    }





}*/
