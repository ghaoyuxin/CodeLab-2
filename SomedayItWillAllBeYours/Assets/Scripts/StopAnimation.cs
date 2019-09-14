using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnimation : MonoBehaviour
{
    private Animator anim;
    private AudioSource aud;

    private float animSpeed = 1f;
    void Start()
    {
        anim = GetComponent<Animator>();
        animSpeed = anim.speed;
        aud = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PublicVars.isDrag)
        {
            anim.speed = 0f;
            aud.Play();
            //print("played");
        }
        else
        {
            anim.speed = animSpeed;
        }
    }
}
