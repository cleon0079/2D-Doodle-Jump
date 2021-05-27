using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    Vector3 startPos;
    bool hitMax = false;

    PlatformSetting platformSettingRef;
    PlatformSetting.PlatformType type = PlatformSetting.PlatformType.Ground;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigidbody2d;

    Player player;
    GameManager gameManager;

    public void SetType(PlatformSetting.PlatformType _type) => type = _type;

    private void OnEnable()
    {
        platformSettingRef = FindObjectOfType<GameManager>().platformSetting;
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2D.isTrigger = true;

        SetPlatform();
    }

    private void Update()
    {
        switch (type)
        {
            case PlatformSetting.PlatformType.Horizontal:
                if (hitMax == false)
                {
                    transform.Translate(new Vector3(-platformSettingRef.horizontalPlatform.speed * Time.deltaTime, 0));
                    if (startPos.x - transform.position.x > platformSettingRef.horizontalPlatform.distance)
                    {
                        hitMax = !hitMax;
                    }
                }
                else
                {
                    transform.Translate(new Vector3(platformSettingRef.horizontalPlatform.speed * Time.deltaTime, 0));
                    if (startPos.x - transform.position.x < -platformSettingRef.horizontalPlatform.distance)
                    {
                        hitMax = !hitMax;
                    }
                }
                break;
            case PlatformSetting.PlatformType.Vertical:
                if (hitMax == false)
                {
                    transform.Translate(new Vector3(0, -platformSettingRef.verticalPlatform.speed * Time.deltaTime));
                    if (startPos.x - transform.position.x > platformSettingRef.verticalPlatform.distance)
                    {
                        hitMax = !hitMax;
                    }
                }
                else
                {
                    transform.Translate(new Vector3(0, platformSettingRef.verticalPlatform.speed * Time.deltaTime));
                    if (startPos.x - transform.position.x < -platformSettingRef.verticalPlatform.distance)
                    {
                        hitMax = !hitMax;
                    }
                }
                break;
            default:
                break;
        }

        if(gameManager.disableGO.transform.position.y >= transform.position.y && type != PlatformSetting.PlatformType.Ground)
        {
            rigidbody2d.gravityScale = 0;
            gameManager.BackToPool(this.gameObject);
        }
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
        switch (type)
        {
            case PlatformSetting.PlatformType.Normal:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Normal];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Broken:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Broken];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Once:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Once];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Doudle:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Doudle];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Horizontal:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Horizontal];
                startPos = transform.position;
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Vertical:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Vertical];
                startPos = transform.position;
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Ground:
                spriteRenderer.sprite = sprites[(int)PlatformSetting.PlatformType.Ground];
                ResizeGround();
                break;
        }
    }

    void Jump()
    {
        switch (type)
        {
            case PlatformSetting.PlatformType.Normal:
                player.Jump(platformSettingRef.normalPlatform.jumpHeight);
                break;
            case PlatformSetting.PlatformType.Broken:
                player.Jump(platformSettingRef.brokenPlatform.jumpHeight);
                rigidbody2d.gravityScale = 1.5f;
                break;
            case PlatformSetting.PlatformType.Once:
                player.Jump(platformSettingRef.oncePlatform.jumpHeight);
                rigidbody2d.gravityScale = 1.5f;
                break;
            case PlatformSetting.PlatformType.Doudle:
                player.Jump(platformSettingRef.doublePlatform.jumpHeight);
                break;
            case PlatformSetting.PlatformType.Horizontal:
                player.Jump(platformSettingRef.horizontalPlatform.jumpHeight);
                break;
            case PlatformSetting.PlatformType.Vertical:
                player.Jump(platformSettingRef.verticalPlatform.jumpHeight);
                break;
            case PlatformSetting.PlatformType.Ground:
                player.Jump(platformSettingRef.groundPlatform.jumpHeight);
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
