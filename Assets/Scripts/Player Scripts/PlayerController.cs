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

    //set the caninteract variable to false (this is for PiggyBacks)
    private bool canInteract = false;

    //get collider variable ready for piggyback
    Collider2D otherCollider;

    //get position variable ready for piggyback
    Transform otherPlayer;

    //player is not giving piggy back right now
    private bool isPiggyBack = false;

    //set cooldown to avoid accidental double presses
    private float piggyBackCooldown = .5f; 

    //player id variable
    private int PlayerID;

    //Grounded Vars
    bool isGrounded = true;

    //get variable name for sprite of player
    public GameObject sprite;

    private followPlayers variables;

    void Start(){
        // get player ID
        PlayerID = GetComponent<PlayerDetails>().playerID;
        Debug.Log("player ID = " + (PlayerID));

        //gets the local scale of an object
        Vector3 local = transform.localScale;

        variables = GameObject.Find("Main Camera").GetComponent<followPlayers>();
        //Change player stats based on ID
        if(PlayerID == 1){
            speed = speedP1;
            jumpForce = jumpForceP1;
            transform.localScale = new Vector3(1,sizeP1,1);
            sprite.GetComponent<SpriteRenderer>().color = Color.red;
            variables.player1 = GetComponent<Transform>();
        }
        else{
            speed = speedP2;
            jumpForce = jumpForceP2;  
            transform.localScale = new Vector3(1,sizeP2,1);   
            sprite.GetComponent<SpriteRenderer>().color = Color.blue;
            variables.player2 = GetComponent<Transform>(); 
        }
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

        //if player one (small player) want to piggyback
        if(PlayerID == 1 && !isPiggyBack){
            PlayerPiggyBack();
        }

        //check if player wants to get off piggy back
        else if(PlayerID == 1 && isPiggyBack){
            //activate cooldown
            if (piggyBackCooldown > 0){
                piggyBackCooldown -= Time.deltaTime;
            }
            else{
                CancelPlayerPiggyBack();
            }
        }

    }

    //defines that the script is getting the Move controls from the new input system
    public void onMove(InputAction.CallbackContext context) => movementInput = context.ReadValue<Vector2>();
    //defines that the script is getting the Jump controls from the new input system
    public void onJump(InputAction.CallbackContext context) => isJumping = context.ReadValue<float>(); 
    //defines that the script is getting the InteractPlayer from the new input system
    public void onInteractPlayer(InputAction.CallbackContext context) => isInteractPlayer = context.ReadValue<float>(); 


    private void Move(Vector3 P){
        //changes the players position horizontaly
        transform.position = (new Vector3(movementInput.x, 0, 0) * speed * Time.deltaTime) + P;

        //checks if player is grounded, wanting to jump and if they are already jumping
        if((isGrounded) && (isJumping == 1 || movementInput.y >= .9) && (GetComponent<Rigidbody2D>().velocity.magnitude == 0)){
            Jump();
        }
    }

    private void Jump(){
        //push the rigid body with a force
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //make sure the player is not grounded, prevents double jump
        isGrounded = false;
    }

    //checking if player is touching the ground
    private void OnTriggerStay2D()
    {
        isGrounded = true;
    }

    private void OnTriggerExit2D()
    {
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if players collide
        if (collision.gameObject.tag == "Player"){
            //ignore colisions of players
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());

            //players have interacted
            canInteract = true;

            //set variables for other players
            otherCollider = collision.gameObject.GetComponent<Collider2D>();
            otherPlayer = collision.transform;
        }
    }
    
    private void PlayerPiggyBack(){
        if(canInteract){
            //check if players are within eachothers bounds, and check if player wants to interact
            if ((GetComponent<Collider2D>().bounds.Intersects(otherCollider.bounds) == true) && (isInteractPlayer == 1))
            {
                //Debug.Log("Bounds intersecting");
                //set other player as parent
                transform.SetParent(otherPlayer);
                //set piggyback to true
                isPiggyBack = true;
                //mave player to above the other player
                transform.position = (new Vector3(0,.7f,0)) + otherPlayer.position;
                //get rid of rigid body so player doesnt fall
                Destroy(GetComponent<Rigidbody2D>());
            }
        }
    }

    private void CancelPlayerPiggyBack(){
        //check if player wants to get off player
        if(canInteract && (isInteractPlayer == 1)){
            //get rid of parent of player
            transform.SetParent(null);
            //offset the players postion to be slightly off
            transform.position = (new Vector3(1,.75f,0)) + otherPlayer.position;
            //add rigidbody back
            gameObject.AddComponent<Rigidbody2D>();
            //set piggyback to false
            isPiggyBack = false;
            //reset cooldown timer
            piggyBackCooldown = .5f;
        }

    }
}
