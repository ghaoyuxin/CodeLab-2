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
    private GameObject _blue, _green, _orange, _purple;

    private void Start()
    {
        _blue = GameObject.FindWithTag("blue");
        _green = GameObject.FindWithTag("green");
        _orange = GameObject.FindWithTag("orange");
        _purple = GameObject.FindWithTag("purple");

        GenerateDots(0.55f, 3, 60, innerRing);//inner ring
        GenerateDots(1.5f, 6, 0, middleRing);//middle ring
        GenerateDots(2.4f, 12, 0, outerRing);//outer ring

    }
    private void GenerateDots(float radius, int numberOfDots, float angleOffset, Transform parent)
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            float angle = angleOffset * Mathf.Deg2Rad + i * Mathf.PI * 2f / numberOfDots;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            GameObject dot = Instantiate(Resources.Load<GameObject>("blue"), newPos, Quaternion.identity);
            dot.transform.SetParent(parent);
        }

    }
}
