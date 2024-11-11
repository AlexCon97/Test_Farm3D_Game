using System;
using System.Collections;
using System.Collections.Generic;
using PrototypeLogic.Game_Manager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed=1;
    //[SerializeField] private float jumpHeight=1;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float minUpCameraRotationAngle;
    [SerializeField] private float maxUpCameraRotationAngle;
    [SerializeField] private Transform hand;

    private IGrabber grabController;
    private IReleaser releaseController;
    private bool isRunning;
    private bool isGrabbed;
    private Camera playerCamera;
    private CharacterController controller;
    private Vector3 newVelocity;
    private Vector3 newAngle;
    private Transform grabbedObject;
    private Vector3 MoveDelta => newVelocity * Time.deltaTime;

    [HideInInspector] public bool IsMovingForward;
    [HideInInspector] public bool IsMovingBackward;
    [HideInInspector] public bool IsMovingRight;
    [HideInInspector] public bool IsMovingLeft;
    
    public bool CanGrabObjects {get;set;} = true;
    public bool CanReleaseObjects {get;set;} = true;
    public bool CanMove {get;set;} = true;
    public bool CanRotate {get;set;} = true;
    public bool CanJump {get;set;} = true;

    public static PlayerController Instance;

    private void Start()
    {
        if (Instance != null) return;
        Instance = this;
        grabController=new GrabController();
        releaseController=new ReleaseController();
        
        controller = GetComponent<CharacterController>();
        playerCamera=Camera.main;
        Cursor.lockState=CursorLockMode.Locked;
    }

    private void Update()
    {
        PlayerMove();
        GrabReleaseObject();
        PlayerRotate();
        Jump();
    }
    void RayTest(){
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Debug.Log(Physics.Raycast(ray,out hit, 2));
        //Debug.Log(hit.collider.name);
    }

    private void Jump()
    {
        if (!CanJump) return;
        //if (Input.GetKeyDown(KeyCode.Space))
    }
    public void GrabReleaseObject()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(!CanGrabObjects) return;
            HUD.GetPlayerAim.SetActive(true);
            if(isGrabbed)
            {
                Debug.Log("Ready to Release");
                return;
            }
            Debug.Log("Ready to grab");
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            if(!CanReleaseObjects) return;
            HUD.GetPlayerAim.SetActive(false);
            if(isGrabbed)
            {
                Debug.Log("Released");
                releaseController.Release(grabbedObject, null);
                isGrabbed=false;
                StartCoroutine(grabbedObject.GetComponent<GravityController>().FallToGround());
                return;
            }
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);;
            RaycastHit hit;
            bool isDefinedObject = Physics.Raycast(ray,out hit, 2);
            if(!(isDefinedObject && hit.transform.GetComponent<GrabbableActor>())) return;
            
            isGrabbed=true;
            grabbedObject=hit.transform;
            grabController.Grab(grabbedObject, hand);
            Debug.Log("Grabbed");
            //GrabSystem.GrabObject(grabObject, hand);
        }
    }

    private void PlayerMove()
    {
        if(!CanMove) return;
        if(Input.GetMouseButtonDown(1)) isRunning=true;
        else if(Input.GetMouseButtonUp(1)) isRunning=false;
         
        var speed=isRunning?walkSpeed*2f:walkSpeed;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float forwardInput = Input.GetAxisRaw("Vertical");
        Vector3 moveHorizontal = horizontalInput*transform.right;
        Vector3 moveForward = forwardInput*transform.forward;
        Vector3 direction = (moveHorizontal+moveForward).normalized;
        Vector3 motion = direction * speed * Time.deltaTime;
        controller.Move(motion);
    }

    private void PlayerRotate(){
        if(!CanRotate) return;
        float lookAround = Input.GetAxis("Mouse X");
        float lookUp = Input.GetAxis("Mouse Y");
        newAngle.x+=-lookUp*rotationSpeed*Time.deltaTime;
        newAngle.y+=lookAround*rotationSpeed*Time.deltaTime;
        newAngle.x=Mathf.Clamp(newAngle.x, minUpCameraRotationAngle, maxUpCameraRotationAngle);
        transform.Rotate(Vector3.up*lookAround*Time.deltaTime*rotationSpeed);
        playerCamera.transform.localRotation = Quaternion.Euler(Vector3.right*newAngle.x);
    }
}
