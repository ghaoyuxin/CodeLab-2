using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Model
{
    public enum Piece
    {
        X = 1,
        O = -1,
        Empty = 0
    };
    private bool _hasMoved = false, _gridIsEmpty = true;
    private int _boardWidth = 3, _boardHeight = 3;
    private string _currentPlayer;
    private Piece _currentPiece;
    private View _view;

    Piece[,] board;

    public void Initialize()
    {
        _view = new View();
        board = new Piece[_boardWidth, _boardHeight];
        MonoBehaviour.print("board has initialized");

    }
    public void MakeMove(int x, int y) // 
    {
        MonoBehaviour.print("x:" + x);
        MonoBehaviour.print("y:" + y);

        //switch which player is moving
        if (!_hasMoved)
        {
            _currentPlayer = "x";
            _currentPiece = Piece.X;

        }
        if (_hasMoved)
        {
            _currentPlayer = "o";
            _currentPiece = Piece.O;
        }


        //check if the grid is empty
        //spawn a game object at (x, y)
        if (board[x, y] == Piece.Empty)
        {
            _view.Update(x, y, _currentPlayer);
            _hasMoved = !_hasMoved;
            board[x, y] = _currentPiece;
        }

        //check win condition
        CheckWinCondition();
    }



    public void CheckWinCondition()
    {
        //check the center piece's 6 adjacent grid, if 3 is in a row, declare won

    }

}
