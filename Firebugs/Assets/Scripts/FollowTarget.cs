using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject lookTarget;
    public float speed = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //look at player
        transform.LookAt(lookTarget.transform);

        //follow player
        Vector3 direction = lookTarget.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = direction * speed;
    }
}
