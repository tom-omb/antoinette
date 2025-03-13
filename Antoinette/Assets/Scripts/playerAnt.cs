using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 moveInput;

    // Headers are to keep track of all of our public parameters
    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    bool isFacingRight = true; //our default

    [Header("Jumping")]
    public float jumpPower = 3f;
int jumps;
int jumpNumber= 1;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.25f,0.03f);
    public LayerMask groundLayer;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator =  GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetFloat("magnitude", rb.velocity.magnitude);
       // animator.SetFloat("yVelocity", rb.velocity.y);
       // rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        
        FlipSprite();

        isGrounded();
       
        
    }
    private void FixedUpdate() //faster than update? not sure
    {    
    // change the player movement/velocity after the inputs (horizontalMovement (updated in MOVE() ) * moveSpeed (static) )
    rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y); 
    //to enable the animations based on the new changed values of both : magnitude=X , yVelocity=Y
    animator.SetFloat("magnitude", Mathf.Abs(rb.velocity.x)); //makes magnitude always positive
    animator.SetFloat("yVelocity", rb.velocity.y);
    }


    void FlipSprite(){ //the name says it all...
        if ((isFacingRight && horizontalMovement < 0f) || (!isFacingRight && horizontalMovement > 0f)){            isFacingRight = !isFacingRight;
            Vector3 localScaleTemp = transform.localScale;
            localScaleTemp.x *= -1f; 
            transform.localScale = localScaleTemp; //where the flip is happening
        }
    }
    private void isGrounded(){ // used to allow jumping only if the player on the ground
    if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0f, groundLayer))
    // our conditions are related to the assigned groundCheck object, to CHECK if it's a ground or not
    //groundCheckPosition 
    //groundCheckSize is WHERE her feet are
    //groundLayer is important to check if the object's Layer is "Ground"
    //0f is an angle for something im not sure and prob not important
    {
        
        //the object is ground, now u r able to jump again
        jumps = jumpNumber;
    }
   
    }


    public void Move(InputAction.CallbackContext context)
    { 
        horizontalMovement = context.ReadValue<Vector2>().x; 
        // reassign any horizantal Movement of the X velocity back in the FixedUpdate() method 
        //im honestly not sure why not the Update method but i think the fixed one is kinda faster 
        animator.ResetTrigger("jump");
    }
    public void Jump(InputAction.CallbackContext context) //function to assign to the input system of the player
    {
        if(jumps > 0) //if we are on the ground, ONLY, jump //doesn't allow multiple jumps
        {
        if(context.performed){ //button is FULLY pressed
            rb.velocity = new Vector2(rb.velocity.x ,jumpPower); // change the player's Y velocity to the jumpPower
            jumps--;            
            animator.SetTrigger("jump"); // enable animation by checking the jumping condition
        }
        if(context.canceled){ //button is NOT Fully pressed "Light tap"
            rb.velocity = new Vector2(rb.velocity.x ,rb.velocity.y *0.5f); //divide the Y velocity by half,Not a full jump
            jumps--;  
            animator.SetTrigger("jump");
        }
        }
    }
    public void Crouch(InputAction.CallbackContext context)
    { 
        if(context.performed){          
            animator.SetTrigger("Crouch"); 
            
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
