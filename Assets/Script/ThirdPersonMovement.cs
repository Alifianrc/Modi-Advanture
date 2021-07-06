using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public static bool playerIsMoving;
    int walkingPitch = 3;

    public CharacterController theController;
    public Transform theCamera;

    public float playerSpeed = 10f;
    public float playerRunSpeed = 20f;
    bool isRunning = false;

    // For turning player
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    Vector3 movementY = new Vector3();
    // For gravity
    float gravityValue = -9.8f * 5;
    // For jumping
    float jumpForce = 2f;
    bool spaceIsUp = false;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameMenuManager.menuIsActive && !GameManager.mulaiTantangan && !PlayerController.animationPicking)
        {
            // Input Movement
            float horizontalIn = Input.GetAxisRaw("Horizontal");
            float verticalIn = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontalIn, 0f, verticalIn).normalized;

            if (Input.GetMouseButtonDown(1))
            {
                isRunning = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                isRunning = false;
                animator.speed = 1;
            }

            // Moving in X and Z axis
            if (direction != Vector3.zero)
            {
                
                if (theController.isGrounded)
                {
                    FindObjectOfType<AudioManager>().Play("Jalan");
                }
                    
                playerIsMoving = true;
                            

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + theCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // For Running
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if (isRunning)
                {
                    theController.Move(moveDir.normalized * playerRunSpeed * Time.deltaTime);
                    FindObjectOfType<AudioManager>().SetPitch("Jalan", walkingPitch * 2);
                }
                else if (!isRunning)
                {
                    theController.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
                    FindObjectOfType<AudioManager>().SetPitch("Jalan", walkingPitch);
                }

                if (theController.isGrounded)
                {
                    animator.SetBool("IsWalking", true);
                    if (isRunning)
                    {
                        animator.speed = 2;
                    }
                }
            }
            else
            {    
                FindObjectOfType<AudioManager>().Stop("Jalan");
                playerIsMoving = false;
                
                animator.SetBool("IsWalking", false);                
            }

            // Movement in Y axis
            if (theController.isGrounded && movementY.y < 0)
            {
                movementY.y = 0f;
            }
            // For Jumping
            if (theController.isGrounded && Input.GetKey("space") && spaceIsUp)
            {
                FindObjectOfType<AudioManager>().Stop("Jalan");
                FindObjectOfType<AudioManager>().Play("Lompat");

                movementY.y += Mathf.Sqrt(jumpForce * -3.0f * gravityValue); Debug.Log("Jump");
                spaceIsUp = false;

                animator.SetBool("IsWalking", false);
            }
            else if (Input.GetKeyUp("space"))
            {
                spaceIsUp = true;
            }  
        }

        // Add Gravity
        if (!theController.isGrounded)
        {
            movementY.y += gravityValue * Time.deltaTime;
        }
        theController.Move(movementY * Time.deltaTime);
    }
}
