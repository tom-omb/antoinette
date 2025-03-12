using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAnt : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
Vector2 moveInput;

    [Header("Movement")]
    public float moveSpeed = 5f;
    float horizontalMovement;
    bool isFacingRight = false;

    [Header("Jumping")]
    public float jumpPower = 3f;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f,0.03f);
    public LayerMask groundLayer;

    // bool isGrounded = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("magnitude", rb.velocity.magnitude);
        animator.SetFloat("yVelocity", rb.velocity.y);
       // rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        
        FlipSprite();
        if (isGrounded())
        {
        animator.SetBool("isJumping", false);
        }
        /*
        if(Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded){
            rb.velocity = new Vector2(rb.velocity.x ,jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
        }*/
    }
    private void FixedUpdate()
    {
    rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
    animator.SetFloat("magnitude", Mathf.Abs(rb.velocity.x));
    animator.SetFloat("yVelocity", rb.velocity.y);
    }


    void FlipSprite(){
        if ((isFacingRight && horizontalMovement > 0f) || (!isFacingRight && horizontalMovement < 0f)){            isFacingRight = !isFacingRight;
            Vector3 localScaleTemp = transform.localScale;
            localScaleTemp.x *= -1f;
            transform.localScale = localScaleTemp;
        }
    }
    private bool isGrounded(){
        
    if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0f, groundLayer))
    {
        return true;
    }
    return false;
    }


    public void Move(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
        
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded())
        {
        if(context.performed){
            rb.velocity = new Vector2(rb.velocity.x ,jumpPower);
            animator.SetBool("isJumping", true);
        }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))  
        {
            isGrounded();
            animator.SetBool("isJumping", false);  
        }
        
    }
    

    void OnDrawGizmosSelected()
{
    Gizmos.color = Color.white;
    Gizmos.DrawCube(groundCheckPos.position,groundCheckSize);
    
}



}
