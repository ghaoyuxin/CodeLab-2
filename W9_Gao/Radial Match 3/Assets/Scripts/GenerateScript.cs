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
    private GameObject[] dotList;

    private void Start()
    {
        dotList = Resources.LoadAll<GameObject>("");
        GenerateDots(0.55f, 3, 60, innerRing);//inner ring
        GenerateDots(1.5f, 6, 0, middleRing);//middle ring
        GenerateDots(2.4f, 12, 0, outerRing);//outer ring

    }
    private void GenerateDots(float radius, int numberOfDots, float angleOffset, Transform parent)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            GameObject dotToGenerate = dotList[Random.Range(0, dotList.Length)];
            float angle = angleOffset * Mathf.Deg2Rad + i * Mathf.PI * 2f / numberOfDots;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            GameObject dot = Instantiate(dotToGenerate, newPos, Quaternion.identity);
            dot.transform.SetParent(parent);
        }

    }
}
