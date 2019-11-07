using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right1 : MonoBehaviour
{
    public GameObject outmostring;
    
    // Start is called before the first frame update
    void Start()
    {
//        positiverotate = Math(e)
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        outmostring.transform.Rotate(0,0,-30, Space.Self);
    }
}
