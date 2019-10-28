using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View
{
    public void Update(int x, int y, string _currentPlayer)
    {
        GameObject.Instantiate(Resources.Load(_currentPlayer), new Vector2(x, y), Quaternion.identity);
    }
}
