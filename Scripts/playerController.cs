using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    //Movement
    public float maxSpeed; // Max Speed set in unity

    //Jumping
    bool grounded = false; //Is player grounded
    float groundCheckRadius = 0.2f; //Circle to check for ground
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight; //Jump force for player character

    //Misc
    Rigidbody2D myRB; //Character Rigid Body
    Animator myAnim; //Character Animator
    bool facingRight; //Checks direction character is facing

	// Use this for initialization
	void Start ()
    {
        myRB = GetComponent<Rigidbody2D>(); //Setting local RB to Character RB
        myAnim = GetComponent<Animator>(); //Setting local Anim to Character Anim
        facingRight = true; //Sets character to face right
	}

    void Update()
    {
        if(grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myAnim.SetBool("isGrounded", grounded); //Sets value in animator to false
            myRB.AddForce(new Vector2(0, jumpHeight)); //Adds jumpheight to Y velocity
        }
    }


    void FixedUpdate ()
    {
        //Grounded check - if not grounded then falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //Sets grounded bool to appropriate value based on whether or not circle intersects with ground layer
        myAnim.SetBool("isGrounded", grounded); //Sets value in animator to false

        myAnim.SetFloat("verticalSpeed", myRB.velocity.y); //Sets animator value to rigidbody velocity

        float move = Input.GetAxis("Horizontal"); //Sets float to horizontal input by player
        myAnim.SetFloat("speed", Mathf.Abs(move)); //Returns absolute value and sends result to anim speed variable 
        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y); //Sets horizontal velocity of character model to local move variable

        //Flipping character model
        if (move  > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }
	}

    void flip() //Function to flip graphic
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale; //Sets a new vector 3 to the local scale value of character
        theScale.x *= -1; //Flips the x value of the character
        transform.localScale = theScale;
    }
}
