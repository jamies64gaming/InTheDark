using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    

    //variables for player movement
    public float speed = 1;
    public float jumpForce = 7.5f;

    //defines value for jumping
    public float isJumping;

    //defines Vector2 for movement
    private Vector2 movementInput;

    //defines Vector2 for player postion
    private Vector3 pos;  


    //Grounded Vars
    bool isGrounded = true;


    // Update is called once per frame
    void Update()
    {
        //gets player position
        pos = transform.position;

        //move function
        Move(pos);
    }

    //defines that the script is getting the Move controls from the new input system
    public void onMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
    //defines that the script is getting the Jump controls from the new input system
    public void onJump(InputAction.CallbackContext context) => isJumping = context.ReadValue<float>();      

    void Move(Vector3 P){
        //changes the players position horizontaly
        transform.position = (new Vector3(movementInput.x, 0, 0) * speed * Time.deltaTime) + P;

        //checks if player is grounded, wanting to jump and if they are already jumping
        if((isGrounded) && (isJumping == 1) && (GetComponent<Rigidbody2D>().velocity.magnitude == 0)){
            Jump();
        }
    }

    void Jump(){
        //push the rigid body with a force
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //make sure the player is not grounded, prevents double jump
        isGrounded = false;
    }

    //checking if player is touching the ground
    void OnTriggerStay2D()
    {
        isGrounded = true;
    }

    void OnTriggerExit2D()
    {
        isGrounded = false;
    }
}
