using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch : MonoBehaviour
{
    private List<Collider2D> TriggerList = new List<Collider2D>();
    private List<GameObject> MatchList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        int matchCount = 0;
        MatchList.Clear();
        if (!TriggerList.Contains(other)) TriggerList.Add(other);
        for (int i = 0; i < TriggerList.Count - 1; i++)
        {
            if (TriggerList[i].tag == TriggerList[i + 1].tag)
            {
                matchCount++;
                if (!MatchList.Contains(other.gameObject))
                {
                    MatchList.Add(TriggerList[i].gameObject);
                    MatchList.Add(TriggerList[i + 1].gameObject);
                }
            }
        }
        if (matchCount == 2)
        {
            print("found a match");

            StartCoroutine(RemoveMatches());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TriggerList.Contains(other)) TriggerList.Remove(other);
        //if (MatchList.Contains(other.gameObject)) MatchList.Remove(other.gameObject);
    }

    private IEnumerator RemoveMatches()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < MatchList.Count; i++)
        {
            Destroy(MatchList[i]);
        }
    }
}
