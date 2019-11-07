using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public int rotateAngle = 0;
    public int rotateDirection = 0;
    public Transform rotateRing;

    private void OnMouseDown()
    {
        rotateRing.transform.Rotate(0, 0, rotateAngle * rotateDirection, Space.Self);
    }
}
