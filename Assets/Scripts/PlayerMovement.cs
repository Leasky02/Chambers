using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    //variable containing the character controller
    public CharacterController controller;
    //player movement variables
    public float speed = 12f;

    //airSpeedMultiplier
    [SerializeField] private float airSpeed = 1f;
    //sprint multiplier
    [SerializeField] private float sprintSpeed = 1f;
    //artifact weight multiplier
    [HideInInspector] public bool additionalWeight = false;
    //contains if player is sprinting
    public bool sprinting = false;
    //if player is idle
    private bool idle;
    //variables containing gravity and jump height inputs
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    //camera
    [SerializeField] public Camera fpsCam;

    //holds position of ground check object
    public Transform groundCheck;
    //contains information of the size of sphere to check around the ground check
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    //contains the velocity of the player
    public Vector3 velocity;
    //contains if the player is on the ground or not
    bool isGrounded;
    //audio source
    private AudioSource myAudioSource;
    //audio clip
    [SerializeField] private AudioClip[] walkingSound;

    //animator
    private Animator myAnimator;
    //if animation is already playing variable
    private bool animationPlaying;
    private string movementAnimation = "walking";

    //start is called once at the start
    private void Start()
    {
        //set component variables
        myAudioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //setting bool to see if player is on the ground repeatedly
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log(additionalWeight);
        //test if player presses button to sprint and is on the ground
        if (Input.GetButton("Sprint") && isGrounded && additionalWeight == false)
        {
            //set sprint speed
            sprintSpeed = 1.5f;
            //set FOV up for sprinting
            sprinting = true;
            //allow animation to change to sprinting animation
            animationPlaying = false;
            movementAnimation = "sprinting";
        }
        //if player has stopped moving, or picked up an item stop sprinting
        if ((idle == true && sprinting) || additionalWeight == true)
        {
            //set sprint speed to normal
            sprintSpeed = 1f;
            sprinting = false;

            //allow animation to change to walking animation
            animationPlaying = false;
            movementAnimation = "walking";
        }

        //if player drops item
        if(additionalWeight == false)
        {
            sprintSpeed = 1.5f;
        }

        //if player is on the ground and the velocity of the player is moving down
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        //if not on ground and sprinting, slow player movement by air speed
        if(!isGrounded && !sprinting)
        {
            airSpeed = 0.6f;
        }
        //if on ground, set airSpeed to normal
        if (isGrounded)
        {
            airSpeed = 1f;
        }
        //if player is on the ground and is moving
        if(isGrounded && (Input.GetAxis("Horizontal") != 0) || Input.GetAxis("Vertical") != 0)
        {
            //set audio clip speed
            if (!sprinting)
                myAudioSource.pitch = 1.5f;
            else
                myAudioSource.pitch = 2.25f;
            //if a sound is NOT already playing, play a random footstep sound
            if (!myAudioSource.isPlaying)
            {
                //set audio clip randomly
                myAudioSource.clip = walkingSound[Random.Range(0, 2)];
                //play walking sound
                myAudioSource.Play();
            }
            //if the walking animation is NOT being played, start walking
            if(!animationPlaying)
            {
                //play animation
                myAnimator.Play(movementAnimation);
                animationPlaying = true;
            }
        }
        else
        {
            //stop footsteps
            myAudioSource.Stop();
        }
        //set x and z equal to Axis input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //if player isnt moving
        if(Input.GetAxis("Horizontal") < 0.4 && Input.GetAxis("Vertical") < 0.4)
        {
            idle = true;
        }
        else
        {
            idle = false;
        }

        //position to move to in the frame
        Vector3 move = transform.right * x + transform.forward * z;
        //gets character controller and moves player to new position according to speed
        controller.Move(move * speed * sprintSpeed * airSpeed *Time.deltaTime);
        //if player jumps with space bar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //equation of a fall after player jumps is square root of jump height * -2 * gravity. velocity is set to this
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //add gravity to players y velocity (gravity is negative so pulls player down)
        velocity.y += gravity * Time.deltaTime;
        //player moves according to velocity
        controller.Move(velocity * Time.deltaTime);
    }
    //check performed from animation whether it should stop
    public void CheckToContinueAnimation()
    {
        //if player is not on ground or not moving
        if(!isGrounded || Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") == 0)
        {
            //sest player to static
            myAnimator.Play("static");
            animationPlaying = false;
        }
    }
}
