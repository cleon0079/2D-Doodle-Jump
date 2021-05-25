using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    float jumpHeight;
    int platformIndex;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigidbody2d;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2D.isTrigger = true;
        platformIndex = int.Parse(gameObject.name);

        SetPlatform();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (collision.gameObject.tag == "Player" && collision.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            Jump();
        }
    }

    void SetPlatform()
    {
        switch (platformIndex)
        {
            case 0:
                jumpHeight = 6f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 1:
                jumpHeight = 0f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 2:
                jumpHeight = 6f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 3:
                jumpHeight = 9f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 4:
                jumpHeight = 6f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 5:
                jumpHeight = 6f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizePlatform();
                break;
            case 6:
                jumpHeight = 6f;
                spriteRenderer.sprite = sprites[platformIndex];
                ResizeGround();
                break;
        }
    }

    void Jump()
    {
        switch (platformIndex)
        {
            case 0:
                player.Jump(jumpHeight);
                break;
            case 1:
                player.Jump(jumpHeight);
                rigidbody2d.gravityScale = 1.5f;
                break;
            case 2:
                player.Jump(jumpHeight);
                rigidbody2d.gravityScale = 1.5f;
                break;
            case 3:
                player.Jump(jumpHeight);
                break;
            case 4:
                player.Jump(jumpHeight);
                break;
            case 5:
                player.Jump(jumpHeight);
                break;
            case 6:
                player.Jump(jumpHeight);
                break;
        }
    }

    void ResizePlatform()
    {
        transform.localScale = Vector3.one;
        boxCollider2D.size = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);
    }

    void ResizeGround()
    {
        transform.localScale = Vector3.one;
        float width = spriteRenderer.bounds.size.x;
        boxCollider2D.size = new Vector2(width, spriteRenderer.bounds.size.y);
        float targetwidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        Vector3 Scale = transform.localScale;
        Scale.x = targetwidth / width;
        transform.localScale = Scale;
    }
}
