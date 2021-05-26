using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    float screenLeft;
    float screenRight;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2d;
    Animator anim;

    GameManager gameManager;

    private void Start()
    {
        screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0)).x;
        screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0)).x;

        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody2d.sleepMode = RigidbodySleepMode2D.NeverSleep;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
    private void FixedUpdate()
    {
        if (gameManager.gameState == GameState.Game)
        {
            Movement();
        }
    }

    private void Update()
    {
        if(transform.position.x < screenLeft)
        {
            transform.position = new Vector3(screenRight, transform.position.y);
        }

        if(transform.position.x > screenRight)
        {
            transform.position = new Vector3(screenLeft, transform.position.y);
        }
        if(rigidbody2d.velocity.y > 0)
        {
            anim.SetBool("JumpUp", true);
        }
        else
        {
            anim.SetBool("JumpUp", false);
        }
    }

    void Movement()
    {
        if(Input.GetAxisRaw("Horizontal") > .1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < -.1f)
        {
            spriteRenderer.flipX = true;
        }

        if (!Mathf.Approximately(Input.GetAxisRaw("Horizontal"), Mathf.Epsilon))
        {
            rigidbody2d.velocity = new Vector3(
                Input.GetAxisRaw("Horizontal") * moveSpeed,
                rigidbody2d.velocity.y);
        }
        else if (!Mathf.Approximately(rigidbody2d.velocity.x, Mathf.Epsilon))
        {
            rigidbody2d.velocity = new Vector3(
                rigidbody2d.velocity.x - ((moveSpeed * Time.fixedDeltaTime)
                * Mathf.Sign(rigidbody2d.velocity.x)),
                rigidbody2d.velocity.y);
        }
    }

    public void Jump(float _jumpHeight)
    {
        rigidbody2d.velocity = Vector3.zero;
        rigidbody2d.AddForce(new Vector3(0, _jumpHeight), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Disable")
        {
            gameManager.EndGame();
        }
    }
}
