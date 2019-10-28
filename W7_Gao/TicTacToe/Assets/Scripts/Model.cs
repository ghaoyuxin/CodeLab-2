using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Model
{
    public enum Piece
    {
        X,
        O,
        Empty
    };
    private bool _hasMoved = false, _gridIsEmpty = true;
    public int boardWidth = 3, boardHeight = 3;
    private string _currentPlayer;

    Piece[,] board = new Piece[,] { };

    public void Initialize()
    {
        board = new Piece[boardWidth, boardHeight];

    }
    public void MakeMove(int x, int y) // 
    {
        //switch which player is moving
        if (!_hasMoved)
        {
            _currentPlayer = "x";
        }
        if (_hasMoved)
        {
            _currentPlayer = "o";
        }

        //check if the grid is empty


        //spawn a game object at (x, y)

        GameObject.Instantiate(Resources.Load(_currentPlayer), new Vector2(x, y), Quaternion.identity);
        _hasMoved = !_hasMoved;

        //check win condition
        CheckWinCondition();
    }



    public void CheckWinCondition()
    {
        //check the center piece's 6 adjacent grid, if 3 is in a row, declare won

    }

}
