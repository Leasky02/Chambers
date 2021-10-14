using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    //player movement variables
    public float speed = 12f;
    [SerializeField] private float sprintSpeed = 1f;
    public bool sprinting = false;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;

    //camera
    [SerializeField] public Camera fpsCam;

    //holds position of ground check object
    public Transform groundCheck;
    //contains information of the size of sphere to check around the ground check
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //contains the velocity of the player
    Vector3 velocity;
    //contains if the player is on the ground or not
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprintSpeed = 2f;
            //set FOV up for sprinting
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintSpeed = 1f;
            sprinting = false;
        }

        //setting bool to see if player is on the ground repeatedly
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //if player is on the ground and the velocity of the player is moving down
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //position to move to in the frame
        Vector3 move = transform.right * x + transform.forward * z;
        //gets character controller and moves player to new position according to speed
        controller.Move(move * speed * sprintSpeed * Time.deltaTime);
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
}
