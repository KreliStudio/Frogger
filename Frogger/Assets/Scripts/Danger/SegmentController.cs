using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SegmentController : MonoBehaviour {

    public Transform startPivot;
    public Transform endPivot;
    public Transform[] prefabs;
    public Transform[] instancePrefabsList;
    
    protected float speed;

    public abstract void Init();
    public abstract void Spawn();
    public abstract void Despawn();

    void Awake()
    {
        Init();
    }






}
