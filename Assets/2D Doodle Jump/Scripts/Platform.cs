using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
    Normal,
    Broken,
    Once,
    Double,
    Horizontal,
    Vertical
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    public PlatformType type;
    [SerializeField] Sprite[] sprites;
    float jumpHeight;

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

        switch (type)
        {
            case PlatformType.Normal:
                spriteRenderer.sprite = sprites[0];
                jumpHeight = 10f;
                break;
            case PlatformType.Broken:
                spriteRenderer.sprite = sprites[1];
                break;
            case PlatformType.Once:
                spriteRenderer.sprite = sprites[2];
                jumpHeight = 10f;
                break;
            case PlatformType.Double:
                spriteRenderer.sprite = sprites[3];
                jumpHeight = 15f;
                break;
            case PlatformType.Horizontal:
                spriteRenderer.sprite = sprites[4];
                jumpHeight = 10f;
                break;
            case PlatformType.Vertical:
                spriteRenderer.sprite = sprites[5];
                jumpHeight = 10f;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if (collision.gameObject.tag == "Player" && collision.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            switch (type)
            {
                case PlatformType.Normal:
                    player.Jump(jumpHeight);
                    break;
                case PlatformType.Broken:
                    rigidbody2d.gravityScale = 1.5f;
                    break;
                case PlatformType.Once:
                    player.Jump(jumpHeight);
                    rigidbody2d.gravityScale = 1.5f;
                    break;
                case PlatformType.Double:
                    player.Jump(jumpHeight);
                    break;
                case PlatformType.Horizontal:
                    player.Jump(jumpHeight);
                    break;
                case PlatformType.Vertical:
                    player.Jump(jumpHeight);
                    break;
                default:
                    break;
            }
        }
    }
}
