using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxController : MonoBehaviour
{
    public float mass = 1f;

    private Rigidbody2D rb;
    private Rigidbody2D rb2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("box hit " + collision.gameObject.tag );
        if(collision.gameObject.tag == "Player")
        {
            
            rb2 = collision.gameObject.GetComponent<Rigidbody2D>();
            Debug.Log("box hit player :) rb2 massx2 = " + (rb2.mass * 2) + " box mass = " + mass);
            if(rb2.mass * 2 <  mass){
                Debug.Log("box collision mass " + rb2.mass); 
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else{
                rb.constraints = RigidbodyConstraints2D.None;
            }
        }
    }
}
