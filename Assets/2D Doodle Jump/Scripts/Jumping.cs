using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class JumpAndMove : MonoBehaviour
{
    float jumpHeight = 10f;
    float moveSpeed = 5f;

    bool isGrounded = false;

    BoxCollider2D boxCollider2D;
    Rigidbody2D rigidbody2d;

    private void Start()
    {
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();

        rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    private void FixedUpdate()
    {
        if(isGrounded == true)
        {
            rigidbody2d.velocity = new Vector2(0, jumpHeight);
            isGrounded = false;
        }
        
        if(!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), Mathf.Epsilon))
        {
            rigidbody2d.velocity = new Vector2(
                Input.GetAxisRaw("Horizontal") * moveSpeed, 
                rigidbody2d.velocity.y);
        }
        else if(!Mathf.Approximately(rigidbody2d.velocity.x, Mathf.Epsilon))
        {
            rigidbody2d.velocity = new Vector2(
                rigidbody2d.velocity.x - ((moveSpeed * Time.deltaTime) 
                * Mathf.Sign(rigidbody2d.velocity.x)), 
                rigidbody2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
