using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text metersText;


    public void Update()
    {
        metersText.text = GameManager.instance.meters.ToString();
    }
}
