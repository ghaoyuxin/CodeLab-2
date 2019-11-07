using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left1 : MonoBehaviour
{
    public GameObject outmostring;
    private float positiverotate;
    
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
        outmostring.transform.Rotate(0,0,30, Space.Self);
    }
}
