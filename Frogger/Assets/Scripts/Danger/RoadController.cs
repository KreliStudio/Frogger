using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

    public Transform startPivot;
    public Transform endPivot;
    public Transform[] cars;

    private float carMaxSpeed;
    private float carSpeed;
    private Transform car1;
    private Transform car2;
    private float lerpTime;


    public void Init(float dificulty)
    {
        // random car speed + dificulty
        carMaxSpeed = 0.4f + dificulty;
        carSpeed = Random.Range(0.1f, carMaxSpeed);
        // create first car 
        int id = Random.Range(0, cars.Length);
        //car1 = Instantiate(cars[id], startPivot.position, Quaternion.identity) as GameObject;
        car1 = EZ_Pooling.EZ_PoolManager.Spawn(cars[id].transform, startPivot.position, Quaternion.identity);
        //car1.SetParent(transform);
        car1.gameObject.AddComponent<Car>().Init(carSpeed, startPivot.position, endPivot.position);
        // create second car
        lerpTime = 0;
        StartCoroutine(CreateSecondCar());
    }

    private  IEnumerator CreateSecondCar()
    {
        yield return new WaitForFixedUpdate();
        // if car speed is lower than half max speed then create second car when first is half way
        if (carSpeed <= carMaxSpeed * 0.66f)
        {
            lerpTime += Time.deltaTime * carSpeed;
            if (lerpTime >= 0.5f)
            {
                int id = Random.Range(0, cars.Length);
                car2 = EZ_Pooling.EZ_PoolManager.Spawn(cars[id].transform, startPivot.position, Quaternion.identity);
                //car2.SetParent(transform);
                car2.gameObject.AddComponent<Car>().Init(carSpeed, startPivot.position, endPivot.position);
            }else
            {
                StartCoroutine(CreateSecondCar());
            }
        }
    }
    
        

    

}
