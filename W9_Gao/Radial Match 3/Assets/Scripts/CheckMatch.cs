using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch : MonoBehaviour
{
    private List<Collider2D> TriggerList = new List<Collider2D>();
    int matchCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        matchCount = 0;
        if (!TriggerList.Contains(other)) TriggerList.Add(other);
        for (int i = 0; i < TriggerList.Count - 1; i++)
        {
            if (TriggerList[i].tag == TriggerList[i + 1].tag) matchCount++;
        }
        if (matchCount == 2) print("found a match");


    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TriggerList.Contains(other)) TriggerList.Remove(other);
    }


}
