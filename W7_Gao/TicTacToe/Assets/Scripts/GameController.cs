using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Model _model;
    private View _view;
    private Controller _controller;


    void Start()
    {
        _model = new Model(); // set Model to an instance of itself
        _view = new View();
        _controller = new Controller();

        _model.Initialize();







    }

    void Update()
    {

    }
}
