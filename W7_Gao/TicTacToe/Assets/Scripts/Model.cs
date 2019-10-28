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
    private bool _hasMoved = false;
    private int _boardWidth = 3, _boardHeight = 3;
    private string _currentPlayer;
    private Piece _currentPiece;
    private View _view;
    private int _moveCount = 0;

    Piece[,] board;

    public void Initialize()
    {
        _view = new View();
        board = new Piece[_boardWidth, _boardHeight];
        //MonoBehaviour.print("board has initialized");

    }
    public void MakeMove(int x, int y) // 
    {
        // MonoBehaviour.print("x:" + x);
        // MonoBehaviour.print("y:" + y);

        //switch which player is moving
        if (!_hasMoved)
        {
            _currentPlayer = "X";
            _currentPiece = Piece.X;

        }
        if (_hasMoved)
        {
            _currentPlayer = "O";
            _currentPiece = Piece.O;
        }


        //check if the grid is empty
        //spawn a game object at (x, y)
        if (board[x, y] == Piece.Empty)
        {
            _view.Update(x, y, _currentPlayer);
            _hasMoved = !_hasMoved;
            board[x, y] = _currentPiece;
            _moveCount++;
        }

        //check win condition
        if (_moveCount >= 5) CheckWinCondition(x, y);
    }



    public void CheckWinCondition(int x, int y)
    {
        //check if the won condition matched with the following 8 conditions, if so declare won
        if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) MonoBehaviour.print(_currentPlayer + " Won");
        else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) MonoBehaviour.print(_currentPlayer + " Won");

    }

}
