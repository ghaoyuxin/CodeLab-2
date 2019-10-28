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
    private bool _hasMoved = false, _hasAWinner = false;
    private int _boardWidth = 3, _boardHeight = 3;
    public string _currentPlayer;
    private Piece _currentPiece;
    public View _view;
    private int _moveCount = 0;

    Piece[,] board;

    public void Initialize()
    {
        _view = new View();
        board = new Piece[_boardWidth, _boardHeight];
        //fill the board with empty pieces
        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                board[i, j] = Piece.Empty;
            }
        }
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
            _view.UpdateView(x, y, _currentPlayer);
            _hasMoved = !_hasMoved;
            board[x, y] = _currentPiece;
            _moveCount++;
        }

        //check win condition
        if (_moveCount >= 5) CheckWinCondition();
        if (_moveCount >= 9 && !_hasAWinner) _view.Draw();
    }



    public void CheckWinCondition() // this is badly coded. how to improve >.<
    {
        //check if the won condition matched with the following 8 conditions, if so declare won
        if (board[0, 0] == _currentPiece && board[1, 0] == _currentPiece && board[2, 0] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }

        else if (board[0, 1] == _currentPiece && board[1, 1] == _currentPiece && board[2, 1] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }

        else if (board[0, 2] == _currentPiece && board[1, 2] == _currentPiece && board[2, 2] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }

        else if (board[0, 0] == _currentPiece && board[0, 1] == _currentPiece && board[0, 2] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;

        }
        else if (board[1, 0] == _currentPiece && board[1, 1] == _currentPiece && board[1, 2] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }
        else if (board[2, 0] == _currentPiece && board[2, 1] == _currentPiece && board[2, 2] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }
        else if (board[0, 0] == _currentPiece && board[1, 1] == _currentPiece && board[2, 2] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }
        else if (board[0, 2] == _currentPiece && board[1, 1] == _currentPiece && board[2, 0] == _currentPiece)
        {
            _view.YouWon(_currentPlayer);
            _hasAWinner = true;
        }
    }

}
