using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public int meters;
    public int conins;
    public bool isDead;

    public GameObject drownParticle;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void EndGame()
    {
        Debug.Log("[GameManager] Game Over.");

        isDead = true;
    }

}
