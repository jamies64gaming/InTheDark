using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    

    public float speed = 1;
    public float jumpForce = 7.5f;
    public float isJumping;

    private Vector2 movementInput;
    private Vector3 pos;  


    //Grounded Vars
    bool isGrounded = true;


    void start(){

    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 move = playerControls.Gameplay.Move.ReadValue<Vector2>();
        //gets player position
        pos = transform.position;

        //move function
        //transform.Translate(new Vector3(movementInput.x, movementInput.y, 0) * speed * Time.deltaTime);
        Debug.Log(isJumping);
        Move(pos);
    }

    public void onMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
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
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        isGrounded = false;
    }


    void OnTriggerStay2D()
    {
        isGrounded = true;
    }

    void OnTriggerExit2D()
    {
        isGrounded = false;
    }
}
