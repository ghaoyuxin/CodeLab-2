using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatch : MonoBehaviour
{
    private int _blueTag = 0, _greenTag = 0, _orangeTag = 0, _purpleTag = 0;
    private List<Collider2D> TriggerList = new List<Collider2D>();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!TriggerList.Contains(other))
        {
            //add the object to the list
            TriggerList.Add(other);
        }

        if (TriggerList.Count == 3)
        {
            if (TriggerList[0] == TriggerList[1] && TriggerList[0] == TriggerList[2]) Destroy(other.gameObject);
            else return;
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if the object is in the list
        if (TriggerList.Contains(other))
        {
            //remove it from the list
            TriggerList.Remove(other);
            print(TriggerList);
        }
    }

    // if (other.gameObject.CompareTag("blue"))
    //     print("on trigger enter");
    // if (other.gameObject.CompareTag("blue"))
    // {
    //     print("found");
    //     Destroy(other.gameObject);
    // }


}
