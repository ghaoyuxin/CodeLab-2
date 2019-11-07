using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class matches : MonoBehaviour
{
    private string dotTag;
    public GameObject match;

    // Start is called before the first frame update
    void Start()
    {
        dotTag = gameObject.tag;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(dotTag))
        {
            Debug.Log(("match"));
            match.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
