using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTokenColor : MonoBehaviour
{
    private InputManagerScript _inputManager;
    private MoveTokensScript _moveTokens;
    private bool selected;

    void Start()
    {
        _inputManager = GetComponent<InputManagerScript>();
        _moveTokens = GetComponent<MoveTokensScript>();
    }

    //change token one color

    // Update is called once per frame
    void Update()
    {
        
    }
}
