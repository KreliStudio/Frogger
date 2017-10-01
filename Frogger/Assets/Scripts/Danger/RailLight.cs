using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailLight : MonoBehaviour {

    public GameObject lightObject;


    public void Start()
    {
        // trun off light on start
        lightObject.SetActive(false);
    }

    public void Alert()
    {
        // start coroutine with pulse light
        StartCoroutine(PulseLight());
    }

    private IEnumerator PulseLight()
    {
        // pule light
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.25f);
            lightObject.SetActive(!lightObject.activeSelf);
        }
        // ant turn on when train is arraiving, after that turn off
        yield return new WaitForSeconds(0.75f);
        lightObject.SetActive(false);

    }

}
