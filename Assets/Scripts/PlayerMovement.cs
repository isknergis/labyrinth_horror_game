using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEditor.Experimental.GraphView;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private List<AudioClip> footSteps = new List<AudioClip>();
    [SerializeField] private List<AudioClip> RunFootSteps = new List<AudioClip>();

    private AudioSource audioSource;

    private float footStepTimer;


    public CharacterController controller;
    public float speed = 12f;


    public float gravity = -9.81f;
    private Vector3 velocity;

    public Transform groundCheck;
    [SerializeField] private bool isGrounded;


    public float groundDistance = 0.4f;
    public LayerMask groundMask;



    private float walkSpeed;
    private float runSpeed=18;
    private Camera cam;
    private bool isRunning;


    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    private void Start()
    {
        walkSpeed = speed;
        cam= Camera.main;   


        controller = GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();

     


        //if (PlayerPositionManager.hasSavedPosition)
        //{
        //    controller.enabled = false;
        //    transform.position = PlayerPositionManager.playerPosition;
        //    transform.rotation = PlayerPositionManager.playerRotation;
        //    controller.enabled = true;
        //}

    }


    //public void SavePosition()
    //{
    //    PlayerPositionManager.playerPosition = transform.position;
    //    PlayerPositionManager.playerRotation = transform.rotation;
    //    PlayerPositionManager.hasSavedPosition = true;
    //}
    private void DoMove()
    {
       
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

  
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Yere yapýþmayý saðlar
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    private void DoGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    private void DoSprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            isRunning = true;
            DOTween.To(() => speed, x => speed = x, runSpeed, 3);

            cam.DOFieldOfView(80, 3);
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            DOTween.To(() => speed, x => speed = x, walkSpeed, 3);
            cam.DOFieldOfView(60, 3);
        }

    }

    private void DoFootSteps()
    {
        footStepTimer-=Time.deltaTime;

        if(footStepTimer<=0)
        {
            if (!isRunning)
            {
                audioSource.PlayOneShot(footSteps[Random.Range(0,footSteps.Count)]);
                footStepTimer = 1;

            
            
            }
            else
            {
       
                audioSource.PlayOneShot(RunFootSteps[Random.Range(0,footSteps.Count)]);
                footStepTimer = .4f; 
            
            }

            
        }
        

    }
   

    void Update()
    {

        DoMove();
        DoGravity();
        DoSprint();
        Debug.Log("Is Grounded: " + isGrounded);
        Debug.Log("Velocity Y: " + velocity.y);



    }
}
