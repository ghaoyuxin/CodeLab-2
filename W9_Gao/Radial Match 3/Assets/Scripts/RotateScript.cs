using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public int rotateAngle = 0;
    public int rotateDirection = 0;
    public Transform rotateRing;
    private float _currentEulerAngle;
    private bool _coroutineIsRunning = false;



    private void OnMouseDown()
    {
        //rotateRing.transform.Rotate(0, 0, rotateAngle * rotateDirection, Space.Self);
        _currentEulerAngle = rotateRing.eulerAngles.z;
        if (!_coroutineIsRunning)
        {
            _coroutineIsRunning = true;
            StartCoroutine(lerpToRotation());
        }
        else return;
    }

    IEnumerator lerpToRotation()
    {
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 2;
            float angle = Mathf.LerpAngle(_currentEulerAngle, _currentEulerAngle + rotateAngle * rotateDirection, t);
            rotateRing.transform.eulerAngles = new Vector3(0, 0, angle);
            yield return null;
        }
        _coroutineIsRunning = false;
    }
}
