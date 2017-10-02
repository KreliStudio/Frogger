/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour {

    public Transform startPivot;
    public Transform endPivot;
    public Transform[] logs;

    private float maxSpeed;
    private float speed;
    private float lerpTime;


    public void Init(float dificulty)
    {
        // random car speed + dificulty
        maxSpeed = 0.4f * dificulty;
        speed =  Random.Range(0.15f, maxSpeed);
        // create logs with random width and calculate proportional pos X (anty overlaping)
        float x = 0;
        // random width first log
        int width = Random.Range(1, logs.Length+1);
        // width map
        float mapWidth = Vector3.Distance(startPivot.position, endPivot.position);
        // if all logs width and spaces between is smaller than map width then create next log
        while ((x + width*2) < mapWidth)
        {
            // position log 
            x += width*2 + Random.Range(1, 4);
            // create log
            CreateWood(x, width-1);
            // random next log width
            width = Random.Range(1, logs.Length + 1);
        }
    }

    private void CreateWood(float x, int width)
    {
        // create object 
        //GameObject obj = Instantiate(logs[width], startPivot.position, Quaternion.identity) as GameObject;
        Transform trans = EZ_Pooling.EZ_PoolManager.Spawn(logs[width], startPivot.position, Quaternion.identity);
        // set parent
        //obj.transform.SetParent(transform);
        // initialization script
        trans.gameObject.AddComponent<Log>().Init(speed, x, startPivot.position, endPivot.position);

    }
    
    
        

    

}
*/