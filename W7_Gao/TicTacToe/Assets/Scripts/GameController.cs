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

        for (int x = 0; x < _model.boardWidth; x++)
        {
            for (int y = 0; y < _model.boardHeight; y++)
            {
                Instantiate(Resources.Load("X"), new Vector2(x, y), Quaternion.identity);
            }
        }



    }

    void Update()
    {

    }
}
