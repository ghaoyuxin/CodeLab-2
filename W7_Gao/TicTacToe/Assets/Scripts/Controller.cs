using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller
{
    private Model _model;
    public void Initialize()
    {
        _model = new Model();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _model.MakeMove(0, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _model.MakeMove(1, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _model.MakeMove(2, 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _model.MakeMove(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _model.MakeMove(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _model.MakeMove(2, 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            _model.MakeMove(0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            _model.MakeMove(1, 0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            _model.MakeMove(2, 0);
        }

    }

}
