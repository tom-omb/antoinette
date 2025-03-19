using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public CapsuleCollider2D bodySize;
    private float currentSpeed;


    // Headers are to keep track of all of our public parameters
    [Header("Movement")]
    public float moveSpeed = 1f;
    public static float horizontalMovement;
    bool isFacingRight = true; //our default
    

    [Header("Jumping")]
    public float jumpPower = 3f;
    int jumps;
    int jumpNumber= 1;


    [Header("Crouching")] 
    public float crouchSpeed = 0.5f;
    private bool isCrouching = false; //to calc current speed and enable animation

    public Vector2 standingColliderSize = new Vector2(3.71f, 2.27f);  // default capsule SIZE
    public Vector2 standingColliderOffset = new Vector2(-0.58f, -0.93f);  // default capsule OFFSET

    public Vector2 crouchingColliderSize = new Vector2(3.5f, 1.5f); // new size/smaller capsule when crouching
    public Vector2 crouchingColliderOffset = new Vector2(-0.58f, -1.5f); //new position capsule when crouching


    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.25f,0.03f);
    public LayerMask groundLayer;


    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
        bodySize = GetComponent<CapsuleCollider2D>();
    }

    /* ===========================================
    Update() : FlipSprite(); isGrounded(); , 
    FixedUpdate()
    ===========================================*/
    void Update() //logical operaions
    {
        currentSpeed = isCrouching ? crouchSpeed : moveSpeed; 
        FlipSprite();

        isGrounded();
    }
    private void FixedUpdate() //for animation and movement 
    {   
        
        // change the player movement/velocity after the inputs (horizontalMovement (updated in MOVE() ) * current speend (updated in update() depending on weither crouch or not) )
        rb.velocity = new Vector2(horizontalMovement * currentSpeed, rb.velocity.y); 
        //to enable the animations based on the new changed values of both : magnitude=X , yVelocity=Y
        animator.SetFloat("magnitude", Mathf.Abs(rb.velocity.x)); //makes magnitude always positive
        animator.SetFloat("yVelocity", rb.velocity.y);
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FlipSprite(){ //the name says it all...
        if ((isFacingRight && horizontalMovement < 0f) || (!isFacingRight && horizontalMovement > 0f)){            
            isFacingRight = !isFacingRight;
            Vector3 localScaleTemp = transform.localScale;
            localScaleTemp.x *= -1f; 
            transform.localScale = localScaleTemp; //where the flip is happening
        }
    }
    private void isGrounded(){ // used to allow jumping only if the player on the ground
    if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0f, groundLayer))
    // our conditions are related to the assigned groundCheck object, to CHECK if it's a ground or not
    // groundCheckPosition 
    // groundCheckSize is WHERE her feet are
    // groundLayer is important to check if the object's Layer is "Ground"
    // 0f is an angle for something im not sure and prob not important
    { 
        //the object is ground, now u r able to jump again
        jumps = jumpNumber;
    }
   
    }

    /* ==================================================
    Functions to assign to the input system of the player
    =====================================================*/
    public void Move(InputAction.CallbackContext context)
    {
        if (AutoScrollEvents.EnableInput) //if the auto scroller is on, x movement is disabled
        {
            horizontalMovement = context.ReadValue<Vector2>().x;
            // reassign any horizantal Movement of the X velocity back in the FixedUpdate() method 
            //im honestly not sure why not the Update method but i think the fixed one is kinda faster   
        }
    }
    public void Jump(InputAction.CallbackContext context) 
    {
        if(jumps > 0) //if we are on the ground, ONLY, jump //doesn't allow multiple jumps
        {
        if(context.performed){ //button is FULLY pressed; interaction is complete
            rb.velocity = new Vector2(rb.velocity.x ,jumpPower); // change the player's Y velocity to the jumpPower
            jumps--;            
            animator.SetTrigger("jump"); // enable animation by checking the jumping condition
        }
        if(context.canceled){ //button is NOT Fully pressed "Light tap"; interaction is not complete/interrupted
            rb.velocity = new Vector2(rb.velocity.x ,rb.velocity.y *0.5f); //divide the Y velocity by half,Not a full jump
            jumps--;  
        }
        }
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if (AutoScrollEvents.EnableInput) //crouching messes w the autoscroller so I'm disabling it WHOOPS
        {
            if (context.performed)  //button Pressed; Press is performed as an 'interaction'
            {
                isCrouching = true; // movement/current speed will be changed
                animator.SetBool("IsCrouching", true);
                bodySize.size = crouchingColliderSize;         // shrink the body's collider Size
                bodySize.offset = crouchingColliderOffset;
            }
            else if (context.canceled)  //button Released
            {
                isCrouching = false;
                animator.SetBool("IsCrouching", false);
                bodySize.size = standingColliderSize;     //reset the body's collider Size
                bodySize.offset = standingColliderOffset;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded();
    }
    

    void OnDrawGizmosSelected()
    // help us to know where is the ground will be checked at (feet,obv) by drawing a lil cube 
{
     Gizmos.color = Color.white;
     Gizmos.DrawCube(groundCheckPos.position,groundCheckSize); //groundCheck is an empty obj with a position 
}

}
