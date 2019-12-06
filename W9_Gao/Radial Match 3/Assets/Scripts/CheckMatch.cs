using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch : MonoBehaviour
{
    private List<Collider2D> TriggerList = new List<Collider2D>();
    private List<GameObject> MatchList = new List<GameObject>();
    private List<GameObject> RepopulateList = new List<GameObject>();
    private GameObject[] dotPrefabs2;
    private GenerateScript _genScript;

    private void Start()
    {
        dotPrefabs2 = Resources.LoadAll<GameObject>("");
        _genScript = GameObject.FindGameObjectWithTag("GameMan").GetComponent<GenerateScript>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int matchCount = 0;
        MatchList.Clear(); //clear Matchlist to start over the counting
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
            ServiceLocators.foundAMatch = true;

            //get transform of dots that's going to be removed

            // for (int i = 0; i < MatchList.Count; i++)
            // {
            //     Repopulate(MatchList[i].transform);
            // }

            StartCoroutine(RemoveMatches());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TriggerList.Contains(other)) TriggerList.Remove(other);
    }

    private IEnumerator RemoveMatches()
    {
        yield return new WaitForSeconds(1);
        ServiceLocators.removedMatches++;
        if (ServiceLocators.removedMatches == 3) _genScript.Reset();
        for (int i = 0; i < MatchList.Count; i++)
        {
            Destroy(MatchList[i]);
        }

    }

    private void Repopulate(Transform dot)
    {
        GameObject dotToRepopulate = dotPrefabs2[Random.Range(0, dotPrefabs2.Length)];

        GameObject dotRepopulated = Instantiate(dotToRepopulate, dot.position, Quaternion.identity);
        dotRepopulated.transform.SetParent(dot.parent);
    }
}
