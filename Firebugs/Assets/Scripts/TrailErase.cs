using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailErase : MonoBehaviour
{
    private TrailRenderer ink;
    private int eraseTimeSlow = 100;
    private float eraseTimeFast = 0.5f;
    void Start()
    {
        ink = GetComponent<TrailRenderer>();
        ink.time = eraseTimeSlow;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PublicVars.isDrag)
        {
            ink.time = eraseTimeFast;
        }
        else if (PublicVars.isDrag)
        {
            ink.time = eraseTimeSlow;
        }
    }
}
