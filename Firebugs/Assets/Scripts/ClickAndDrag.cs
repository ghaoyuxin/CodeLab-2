using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//INTENT: the script enable mouse to click and drag gameobjects with tag "Pickup"
//USAGE: attach it to Main Camera, check camera Z position, create tag "Pickup"
//NOTE: //this script is good to use. not very heavy if I don't have may game object. 
//it can be optimized by putting objects on different layers. raycast will ignore objects that are not on raycast layer.
public class ClickAndDrag : MonoBehaviour
{
    public GameObject ball;
    private Camera cam;
    Vector2 mousePos;
    Vector3 myPoint;
    
    public float zPoint = 10;
    //private bool isDrag = false;
    

    void Start()
    {
        cam = Camera.main;
        myPoint = new Vector3();
        mousePos = new Vector2();

    }
    
    void Update()
    {
        //when dragging
        if (PublicVars.isDrag)
        {
            //do math to convert mouse position into world space
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            myPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane + zPoint)); // +10f is to make myPoint Z = 0, otherwise it will be at the Z of nearClipPlane
            //make ball follow the mouse
            ball.transform.position = myPoint;

            if (Input.GetMouseButtonUp(0))
            {
                PublicVars.isDrag = false;
            }

        }
        //when not dragging
        else
        {
            //check if player is clicking
            if (Input.GetMouseButtonDown(0))
            {
                //check if raycast hit a game object w/ tag "Pickup"
                Ray myRay = cam.ScreenPointToRay( Input.mousePosition);
                float maxRayDist = 100f; 
                Debug.DrawRay( myRay.origin, myRay.direction * maxRayDist, Color.yellow);
                RaycastHit myRayHit = new RaycastHit();
                //check if object is on the raycast layer
                //int layerMask = 1 << LayerMask.NameToLayer("CameraRight");
                //if hit
                if(Physics.Raycast(myRay, out myRayHit, maxRayDist))
                {
                    //compare tag
                    if(myRayHit.transform.CompareTag("Pickup"))
                    {
                        ball.transform.position = myRayHit.transform.position;
                        PublicVars.isDrag = true;
                    }
                }
            }
        }
    }
}

