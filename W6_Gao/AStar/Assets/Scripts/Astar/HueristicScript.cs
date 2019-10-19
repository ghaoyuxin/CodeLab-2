using UnityEngine;
using System.Collections;

public class HueristicScript : MonoBehaviour
{

    public virtual float Hueristic(int x, int y, Vector3 start, Vector3 goal, GridScript gridScript)
    {
        //return 0; //heuristic 1 is the original A Star
        //return Mathf.Abs(start.x - goal.x) + Mathf.Abs(start.y - goal.y); // heuristic 2 emphasize on the distance to the goal, why this doesn't change anything?

        return Random.Range(0, 100);//Heuristic 3

        //the faster heuristic for both 1stGridScene and 2ndGridScene is Heurstic 1 or Heuristic 2(they are the same result??)
    }
}
