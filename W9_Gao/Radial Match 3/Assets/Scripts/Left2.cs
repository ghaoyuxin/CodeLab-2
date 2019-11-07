using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Left2 : MonoBehaviour
{
    public GameObject middlering;
    private float positiverotate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        middlering.transform.Rotate(0,0,60, Space.Self);
    }
}
