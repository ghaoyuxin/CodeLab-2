using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Right2 : MonoBehaviour
{
    public GameObject middlering;
    
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
        middlering.transform.Rotate(0,0,-60, Space.Self);
    }
}
