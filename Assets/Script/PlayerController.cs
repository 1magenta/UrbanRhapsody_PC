using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    //-----------audio------------------------
    public AudioClip collectTrashClip;

    //-----------item collection variable---------------------
    int trashCollected = 0; // the number relate to the city comfort score
    int comfortScore = 10;

    int pleasureValue = 10;

    //-------------player movement variable-------------
    private float moveSpeed = 4.0f;
    private float rotationSpeed = 80.0f; 

    [Header("Movement System")]
    public float walkSpeed = 4.0f;
    public float runSpeed = 8.0f;

    //player interaction component
    PlayerInteractionController playerInteractionController;

    public LayerMask groundLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        //access movement componets
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // access interaction component
        playerInteractionController = GetComponentInChildren<PlayerInteractionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded())
        {
            PlayerMove();
            Interact();
            if(controller.transform.position.y < 0.0f || controller.transform.position.y > 0.3f)
            {
                controller.transform.position = new Vector3(controller.transform.position.x, 0.0f, controller.transform.position.z);
            }
        }

        if (!IsGrounded())
        {
            Debug.Log("Not grounded");
            Vector3 nearestGround = FindNearestGroundPosition();
            if (nearestGround != Vector3.zero) // A valid ground position is found
            {
                //controller.transform.position = nearestGround;
                controller.transform.position = new Vector3(controller.transform.position.x, 0, controller.transform.position.z); 
            }
            if (Input.GetKeyDown(KeyCode.F5))
            {
                RelocatePlayerToOrigin();
            }
        }


        // To debug
        //skip the time when the right squre bracket is pressed
        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    //------------------------------------Grounded the character---------------------------------------
    private bool IsGrounded()
    {
        //float checkDistance = 0.1f; // Small distance below the character to check for ground
        Vector3 origin = transform.position; // Center of the character
        float radius = controller.radius; //half the height

        // Cast a sphere slightly below the character to check for ground
        return Physics.CheckSphere(origin, radius, groundLayerMask);
    }
    private Vector3 FindNearestGroundPosition()
    {
        RaycastHit hit;
        Vector3 origin = controller.transform.position + Vector3.up; // Start slightly above the player
        float maxDistance = 100f; // Increased distance to ensure it reaches the ground

        if (Physics.Raycast(origin, Vector3.down, out hit, maxDistance, groundLayerMask))
        {
            return new Vector3(hit.point.x, hit.point.y + controller.height / 2, hit.point.z); // Adjust for player height
        }

        return Vector3.zero; // Return zero if no ground found
    }
    //For inGame bug, if can't grounded, reposition the character
    private void RelocatePlayerToOrigin()
    {
        // Relocate the player to (0,0,0)
        if (controller != null)
        {
            controller.enabled = false;
            transform.position = Vector3.zero;
            controller.enabled = true;
        }
        else
        {
            transform.position = Vector3.zero;
        }
    }



    /// <summary>
    /// to deal with all the player interaction
    /// </summary>
    public void Interact()
    {
        if (Input.GetButtonDown("Water"))
        {
            playerInteractionController.Interact();
        }
    }

    /* public void PlayerMove()
     {
         float hori = Input.GetAxisRaw("Horizontal");
         float vert = Input.GetAxisRaw("Vertical");

         //Movement dir & velocity
         Vector3 dir = new Vector3(hori, 0f, vert).normalized;
         Vector3 moveVelocity = moveSpeed * Time.deltaTime * dir;

         *//*        float translation = vert * moveSpeed * Time.deltaTime;
                 float rotation = hori * rotationSpeed * Time.deltaTime;*//*


         //check running requirements
         if (Input.GetButton("Sprint"))
         {
             moveSpeed = runSpeed;
             animator.SetBool("Running", true);
         }
         else
         {
             moveSpeed = walkSpeed;
             animator.SetBool("Running", false);
         }
         //check movement
         if (dir.magnitude >= 0.1f)
         {
             //look at the dir
             transform.rotation = Quaternion.LookRotation(dir);

             //move
             controller.Move(moveVelocity);
         }

         animator.SetFloat("Speed", moveVelocity.magnitude);
     }*/

    public void PlayerMove()
    {
        float moveInput = Input.GetAxisRaw("Vertical"); // Forward and backward movement
        float rotateInput = Input.GetAxisRaw("Horizontal"); // Left and right rotation

        // Calculate forward movement
        Vector3 moveDirection = transform.forward * moveInput;
        Vector3 moveVelocity = moveSpeed * Time.deltaTime * moveDirection.normalized;

        // Check for sprinting
        if (Input.GetButton("Sprint"))
        {
            moveSpeed = runSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            moveSpeed = walkSpeed;
            animator.SetBool("Running", false);
        }

        // Apply movement
        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveVelocity);
        }

        // Calculate rotation
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        // Update animator speed
        animator.SetFloat("Speed", moveVelocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        //collect trash
        if (other.CompareTag("trash"))
        { 
            AudioManager.instance.AudioPlay(collectTrashClip);
            trashCollected++;
            comfortScore += 1;
            Debug.Log(comfortScore);
            Destroy(other.gameObject);
            UIManager.Instance.UpdateComfortVal(1);
        }

    }
}
