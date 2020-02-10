﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; 
    private Rigidbody myRigidbody; 
    private Vector3 moveInput; 
    private Vector3 moveVelocity; 

    private Camera mainCamera; 

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>(); 
        mainCamera = FindObjectOfType<Camera>(); 
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed; 

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

         if(groundPlane.Raycast(cameraRay, out rayLength)){
          Vector3 pointToLook = cameraRay.GetPoint(rayLength);
          Debug.Log("you are in the if"); 
          Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue); 

    //        transform.LookAt(pointToLook); 
         transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
          }

    }

    void FixedUpdate(){
        myRigidbody.velocity = moveVelocity; 
    }

}