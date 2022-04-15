using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{    

    //variables for player movement
    public float speedP1 = 1;
    public float jumpForceP1 = 7.5f;

    public float speedP2 = 1;
    public float jumpForceP2 = 7.5f;

    private float speed;
    private float jumpForce;

    public float sizeP1 = 1;
    public float sizeP2 = 1.5f;

    //defines value for jumping
    public float isJumping;

    public float isInteractPlayer;

    //defines Vector2 for movement
    private Vector2 movementInput;

    //defines Vector2 for player postion
    private Vector3 pos;  

    //box colliders as variables
    public BoxCollider2D mainCollider;

    public BoxCollider2D jumpTrigger;

    public BoxCollider2D bodyTrigger;

    public BoxCollider2D headColliderCheck;


    private bool canInteract = false;
    Collider2D Collider2;
    Transform player2;

    private bool isPiggyBack = false;

    private int PlayerID;

    //Grounded Vars
    bool isGrounded = true;


    void Start(){
        // get player ID
        PlayerID = GetComponent<PlayerDetails>().playerID;
        Debug.Log("player ID = " + (PlayerID));

        //gets the local scale of an object
        Vector3 local = transform.localScale;

        
        //Change player stats based on ID
        if(PlayerID == 1){
            speed = speedP1;
            jumpForce = jumpForceP1;
            transform.localScale = new Vector3(1,sizeP1,1);
        }
        else{
            speed = speedP2;
            jumpForce = jumpForceP2;  
            transform.localScale = new Vector3(1,sizeP2,1);   
        }

        //ignore collisions so players cant collide
        Physics.IgnoreLayerCollision(0,5);
    }

    // Update is called once per frame
    void Update()
    {
        //gets player position
        pos = transform.position;

        //move function
        if(!isPiggyBack){
            Move(pos);
        }

        if(PlayerID == 1 && !isPiggyBack){
            PlayerPiggyBack(pos);
        }

    }

    //defines that the script is getting the Move controls from the new input system
    public void onMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
    //defines that the script is getting the Jump controls from the new input system
    public void onJump(InputAction.CallbackContext context) => isJumping = context.ReadValue<float>(); 
    //defines that the script is getting the InteractPlayer from the new input system
    public void onInteractPlayer(InputAction.CallbackContext context) => isInteractPlayer = context.ReadValue<float>(); 


    void Move(Vector3 P){
        //changes the players position horizontaly
        transform.position = (new Vector3(movementInput.x, 0, 0) * speed * Time.deltaTime) + P;

        //checks if player is grounded, wanting to jump and if they are already jumping
        if((isGrounded) && (isJumping == 1 || movementInput.y >= .9) && (GetComponent<Rigidbody2D>().velocity.magnitude == 0)){
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            canInteract = true;
            Collider2 = collision.gameObject.GetComponent<Collider2D>();
            player2 = collision.transform;
        }
    }
    
    private void PlayerPiggyBack(Vector3 p){
        if(canInteract){
            Debug.Log(isInteractPlayer);
            if ((GetComponent<Collider2D>().bounds.Intersects(Collider2.bounds) == true) && (isInteractPlayer == 1))
            {
                Debug.Log("Bounds intersecting");
                transform.SetParent(player2);
                isPiggyBack = true;
                transform.position = (new Vector3(0,1.5f,0)) + p;
            }
        }
    }
}
