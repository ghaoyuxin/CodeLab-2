using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INTENT: generate dots
//USAGE: attach to game manager

public class GenerateScript : MonoBehaviour
{
    [Header("Parents")]
    public Transform innerRing;
    public Transform middleRing;
    public Transform outerRing;
    public SpriteRenderer foundMatchUI;
    private GameObject[] dotPrefabs;

    private void Start()
    {
        dotPrefabs = Resources.LoadAll<GameObject>("");
        GenerateDots(0.55f, 3, 60, innerRing);//inner ring, the radius is tested out from the scene
        GenerateDots(1.5f, 6, 0, middleRing);//middle ring
        GenerateDots(2.4f, 12, 0, outerRing);//outer ring

    }
    private void GenerateDots(float radius, int numberOfDots, float angleOffset, Transform parent)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            GameObject dotToGenerate = dotPrefabs[Random.Range(0, dotPrefabs.Length)];
            float angle = angleOffset * Mathf.Deg2Rad + i * Mathf.PI * 2f / numberOfDots;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            GameObject dot = Instantiate(dotToGenerate, newPos, Quaternion.identity);
            dot.transform.SetParent(parent);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Reset();

        UpdateView();

    }

    public void Reset()
    {
        ServiceLocators.removedMatches = 0;

        foreach (Transform child in innerRing)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in middleRing)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in outerRing)
        {
            GameObject.Destroy(child.gameObject);
        }
        GenerateDots(0.55f, 3, 60, innerRing);
        GenerateDots(1.5f, 6, 0, middleRing);
        GenerateDots(2.4f, 12, 0, outerRing);
    }

    private void UpdateView()
    {
        if (ServiceLocators.foundAMatch) foundMatchUI.enabled = true;
        if (!ServiceLocators.foundAMatch) foundMatchUI.enabled = false;
    }
}
