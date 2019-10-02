using System;
using UnityEngine;
using System.Collections;

public class InputManagerScript : MonoBehaviour {
	private GameManagerScript _gameManager;
	private MoveTokensScript _moveManager;
	private GameObject _selected = null;
	public Camera camera;

	public virtual void Start () {
		_moveManager = GetComponent<MoveTokensScript>();
		_gameManager = GetComponent<GameManagerScript>();
	}

	public virtual void SelectToken(){
		if (!Input.GetMouseButtonDown(0)) return;
		
		var mousePos = camera.ScreenToWorldPoint(Input.mousePosition); // get world position of the mouse click
			
		var overlapPoint = Physics2D.OverlapPoint(mousePos); //get Collider2D of the place mouse clicked

		if (ReferenceEquals(overlapPoint, null)) return; // if no overlappoint detected (MEANS mouse clicked space did not have collider2D), return
		//By this point, we have got overlappoint/Collider2D! 

		if(ReferenceEquals(_selected, null)) // if no game object is selected yet
		{
			_selected = overlapPoint.gameObject; // pass the game object at the overlap point to _selected, _selected is the first token's temporary storage
		} 
		else 
		{
			var pos1 = _gameManager.GetPositionOfTokenInGrid(_selected);
			var pos2 = _gameManager.GetPositionOfTokenInGrid(overlapPoint.gameObject);

			if(Math.Abs(Mathf.Abs((pos1.x - pos2.x) + (pos1.y - pos2.y)) - 1) < 0.01f){ //strange math???
				_moveManager.SetupTokenExchange(_selected, pos1, overlapPoint.gameObject, pos2, true);
			}

			_selected = null;
		}
	}
}
