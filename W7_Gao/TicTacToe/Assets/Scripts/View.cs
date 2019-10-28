using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View
{
    public GameController gameController;
    public void UpdateView(int x, int y, string _currentPlayer)
    {
        GameObject.Instantiate(Resources.Load(_currentPlayer), new Vector2(x, y), Quaternion.identity);


    }
    public void YouWon(string player)
    {
        MonoBehaviour.print("you won ran");
        gameController.gameOver.gameObject.SetActive(true);
        gameController.gameOver.text = player + " Won";

    }
    public void Draw()
    {
        gameController.gameOver.gameObject.SetActive(true);
        gameController.gameOver.text = "It's a Draw. R to try again.";
    }
}
