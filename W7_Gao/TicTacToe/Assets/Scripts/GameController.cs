using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private Model _model;
    private View _view;
    public Controller _controller;

    public TextMeshProUGUI gameOver;


    void Start()
    {
        // set Model to an instance of itself

        //put itself in the view script
        _controller = new Controller();
        _controller.Initialize();
        _controller._model._view.gameController = this;

    }

    void Update()
    {
        _controller.Update();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
