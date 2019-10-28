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
    public void MoveMade(int x, int y) // alternating making moves
    {


    }

    public void CheckWinCondition()
    {

    }

}
