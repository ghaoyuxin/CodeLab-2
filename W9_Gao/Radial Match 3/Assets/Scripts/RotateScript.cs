using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public int rotateAngle = 0;
    public int rotateDirection = 0;
    public Transform rotateRing;
    private int _targetEulerAngle;

    private void OnMouseDown()
    {
        //_targetEulerAngle = rotateAngle * rotateDirection;
        //rotateRing.transform.Rotate(0, 0, rotateAngle * rotateDirection, Space.Self);
        StartCoroutine(lerpToRotation());
        //call check match
    }

    IEnumerator lerpToRotation()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime;
            print(t);
            rotateRing.transform.Rotate(0, 0, Mathf.LerpAngle(0, rotateAngle * rotateDirection, t), Space.Self);
            yield return null;
        }

    }
}
