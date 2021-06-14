using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    // Sprites for the platform
    [SerializeField] Sprite[] sprites;

    // StartPos and a bool to check if its out of range
    Vector3 startPos;
    bool hitMax = false;

    PlatformSetting platformSettingRef;

    // The type and jumpheight and gravity that the platform is
    PlatformSetting.PlatformType type = PlatformSetting.PlatformType.Ground;
    float jumpHeight;
    float gravity;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigidbody2d;

    Player player;
    GameManager gameManager;

    // Get the type and jumpheight and gravity when they spawn
    public void SetType(PlatformSetting.PlatformType _type) => type = _type;
    public void SetJumpHeight(float _jumpHeight) => jumpHeight = _jumpHeight;
    public void SetGravity(float _gravity) => gravity = _gravity;

    private void OnEnable()
    {
        platformSettingRef = FindObjectOfType<GameManager>().platformSetting;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        // Set the default jumpHeight
        if(type == PlatformSetting.PlatformType.Ground)
        {
            jumpHeight = platformSettingRef.groundPlatform.jumpHeight;
        }

        // Set up the platforms rigidbody2d var code
        rigidbody2d.gravityScale = 0;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        // Set the collider2d to a trigger
        boxCollider2D.isTrigger = true;

        // Set up the platform
        SetPlatform(type);
    }

    private void Update()
    {
        // Set up the speed and distance and move it
        switch (type)
        {
            case PlatformSetting.PlatformType.Horizontal:
                PlatformMove(platformSettingRef.horizontalPlatform.speed, platformSettingRef.horizontalPlatform.distance);
                break;
            case PlatformSetting.PlatformType.Vertical:
                PlatformMove(platformSettingRef.verticalPlatform.speed, platformSettingRef.verticalPlatform.distance);
                break;
            default:
                break;
        }

        // If there is platform that is below the disableGO then move it back to the object pool and reset its gravity
        if(gameManager.disableGO.transform.position.y >= transform.position.y && type != PlatformSetting.PlatformType.Ground)
        {
            rigidbody2d.gravityScale = 0;
            gameManager.BackToPool(this.gameObject);
        }
    }

    void PlatformMove(float _speed, float _distance)
    {
        // Move the platform left and right or up and down with in a range
        if (hitMax == false)
        {
            transform.Translate(new Vector3(0, -_speed * Time.deltaTime));
            if (startPos.x - transform.position.x > _distance)
            {
                hitMax = !hitMax;
            }
        }
        else
        {
            transform.Translate(new Vector3(0, _speed * Time.deltaTime));
            if (startPos.x - transform.position.x < -_distance)
            {
                hitMax = !hitMax;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player hits the platform and its falling down then give it a force to jump up
        player = collision.GetComponent<Player>();
        if (collision.gameObject.tag == "Player" && collision.GetComponent<Rigidbody2D>().velocity.y <= 0)
        {
            // Get what type it is and then give the player a force to jump up
            player.Jump(jumpHeight);
            // After the player jumps up then the platform falls down
            rigidbody2d.gravityScale = gravity;

            gameManager.PlaySFX();
        }
    }

    void SetPlatform(PlatformSetting.PlatformType _type)
    {
        // Get what type it is then change the sprites to its image and then resize it
        spriteRenderer.sprite = sprites[(int)_type];
        ResizePlatform();
        startPos = transform.position;
    }

    void ResizePlatform()
    {
        // Set the size of the platforms to the size it should be
        transform.localScale = Vector3.one;
        boxCollider2D.size = new Vector2(spriteRenderer.bounds.size.x, spriteRenderer.bounds.size.y);

        // Resize the ground to scale it to fill the full area of the screen
        if (type == PlatformSetting.PlatformType.Ground)
        {
            float targetwidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
            Vector3 Scale = transform.localScale;
            Scale.x = targetwidth / spriteRenderer.bounds.size.x;
            transform.localScale = Scale;
        }
    }
}
