using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("on trigger enter");
        if (other.gameObject.CompareTag("blue"))
        {
            print("found");
            Destroy(other.gameObject);
        }
    }
}
