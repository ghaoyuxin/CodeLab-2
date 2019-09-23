using UnityEngine;

public class MatchManagerScript : MonoBehaviour {
	
	private GameManagerScript _gameManager;

	public virtual void Start () {
		_gameManager = GetComponent<GameManagerScript>();
	}

	//check if grid has match
	public virtual bool GridHasMatch(){
		// check the horizontal grid
		for(int x = 0; x < _gameManager.gridWidth - 2; x++)
		{
			for(int y = 0; y < _gameManager.gridHeight; y++)
			{
				if (GridHasHorizontalMatch(x, y)) return true;
				break;
			}
		}
		//check the vertical grid
		for(int x = 0; x < _gameManager.gridWidth; x++)
		{
			for(int y = 0; y < _gameManager.gridHeight - 2; y++)
			{
				if (GridHasVerticalMatch(x, y)) return true;
				break;
			}
		}
		return false;
	}

	public bool GridHasHorizontalMatch(int x, int y)
	{
		GameObject token1 = _gameManager.gridArray[x + 0, y];
		GameObject token2 = _gameManager.gridArray[x + 1, y];
		GameObject token3 = _gameManager.gridArray[x + 2, y];
		
		if(token1 != null && token2 != null && token3 != null)
		{
			SpriteRenderer sr1 = token1.GetComponent<SpriteRenderer>();
			SpriteRenderer sr2 = token2.GetComponent<SpriteRenderer>();
			SpriteRenderer sr3 = token3.GetComponent<SpriteRenderer>();
			
			return (sr1.sprite == sr2.sprite && sr2.sprite == sr3.sprite);
		} else {
			return false;
		}
	}

	//grid has vertical match
	public bool GridHasVerticalMatch(int x, int y){
		GameObject token4 = _gameManager.gridArray[x, y + 0];
		GameObject token5 = _gameManager.gridArray[x, y + 1];
		GameObject token6 = _gameManager.gridArray[x, y + 2];
		
		if(token4 != null && token5 != null && token6 != null){
			SpriteRenderer sr4 = token4.GetComponent<SpriteRenderer>();
			SpriteRenderer sr5 = token5.GetComponent<SpriteRenderer>();
			SpriteRenderer sr6 = token6.GetComponent<SpriteRenderer>();
			
			return (sr4.sprite == sr5.sprite && sr5.sprite == sr6.sprite);
		} else {
			return false;
		}
	}

	//try make this section work for both horizontal and vertical
	private int _GetHorizontalMatchLength(int x, int y){
		
		var first = _gameManager.gridArray[x, y];
		if (first == null) return 0 ;
		int matchLength = 1;
	
		if(first != null){
			var sr1 = first.GetComponent<SpriteRenderer>();
			
			for(int currentX = x + 1; currentX < _gameManager.gridWidth; currentX++){
				var other = _gameManager.gridArray[currentX, y];

				if (other == null) return 0;
				else
					{
						var sr2 = other.GetComponent<SpriteRenderer>();

						if(sr1.sprite == sr2.sprite){
							matchLength++;
					} 
				}
			}
		}
		
		return matchLength;
	}
	//
	private int _GetVerticalMatchLength(int x, int y){
		int matchLength = 1;
		
		GameObject first = _gameManager.gridArray[x, y];

		if(first != null){
			SpriteRenderer sr1 = first.GetComponent<SpriteRenderer>();
			
			for(int i = y + 1; i < _gameManager.gridHeight; i++){
				GameObject other = _gameManager.gridArray[i, y];

				if(other != null){
					SpriteRenderer sr2 = other.GetComponent<SpriteRenderer>();

					if(sr1.sprite == sr2.sprite){
						matchLength++;
					} else {
						break;
					}
				} else {
					break;
				}
			}
		}
		
		return matchLength;
	}

	public virtual int RemoveMatches(){
		int numRemoved = 0;

		for(int x = 0; x < _gameManager.gridWidth - 2; x++){
			for(int y = 0; y < _gameManager.gridHeight ; y++){
				{

					int horizonMatchLength = _GetHorizontalMatchLength(x, y);
					if(horizonMatchLength > 2){

						for(int i = x; i < x + horizonMatchLength; i++){
							GameObject token = _gameManager.gridArray[i, y]; 
							Destroy(token);

							_gameManager.gridArray[i, y] = null;
							numRemoved++;
						}
					}
				}
			}
		}
		
		return numRemoved;
	}
}
