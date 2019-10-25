using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model
{
    public enum Players
    {
        X,
        O,
    };

    private struct Piece
    {
        public bool hasMoved;
        public int moveX;
        public int moveY;

    }

    Piece[,] board = new Piece[3, 3];

}
