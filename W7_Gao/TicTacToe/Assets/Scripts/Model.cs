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
    public bool hasMoved;
    public int boardWidth = 3, boardHeight = 3;

    Piece[,] board = new Piece[,] { };

    public void Initialize()
    {
        board = new Piece[boardWidth, boardHeight];

    }
    public void MakeMove(int x, int y) // 
    {
        //switch which game object get spawned



        //spawn a game object at (x, y)
        GameObject.Instantiate(Resources.Load("X"), new Vector2(x, y), Quaternion.identity);

        //check win condition
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        //check the center piece's 6 adjacent grid, if 3 is in a row, declare won

    }

}
