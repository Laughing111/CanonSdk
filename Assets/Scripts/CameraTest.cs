using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour {
    CanonManager cm;
    // Use this for initialization
    void Start () {
         cm= new CanonManager();
        
	}

    public void Shot()
    {
        cm?.Shot();
    }
    // Update is called once per frame
    void OnDisable () {
        cm?.DeInit();
	}
}
