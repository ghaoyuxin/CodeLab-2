using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScript : MonoBehaviour
{
    public float radius = 0f;
    public int numberOfDots = 0;
    public float angleOffset = 0;

    public GameObject blue, green, orange, purple;

    private void Start()
    {
        for (int i = 0; i < numberOfDots; i++)
        {
            float angle = angleOffset * Mathf.Deg2Rad + i * Mathf.PI * 2f / numberOfDots;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            GameObject ring = Instantiate(blue, newPos, Quaternion.identity);
        }

    }
}
