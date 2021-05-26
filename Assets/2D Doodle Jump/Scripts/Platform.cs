using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class Platform : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    PlatformSetting platformSettingRef;
    PlatformSetting.PlatformType type = PlatformSetting.PlatformType.Ground;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;
    Rigidbody2D rigidbody2d;
    Player player;

    public void SetType(PlatformSetting.PlatformType _type) => type = _type;

    private void OnEnable()
    {
        platformSettingRef = FindObjectOfType<GameManager>().platformSetting;
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.gravityScale = 0;
        rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        boxCollider2D.isTrigger = true;

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
        switch (type)
        {
            case PlatformSetting.PlatformType.Normal:
                spriteRenderer.sprite = sprites[platformSettingRef.normalPlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Broken:
                spriteRenderer.sprite = sprites[platformSettingRef.brokenPlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Once:
                spriteRenderer.sprite = sprites[platformSettingRef.oncePlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Doudle:
                spriteRenderer.sprite = sprites[platformSettingRef.doublePlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Horizontal:
                spriteRenderer.sprite = sprites[platformSettingRef.horizontalPlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Vertical:
                spriteRenderer.sprite = sprites[platformSettingRef.verticalPlatform.index];
                ResizePlatform();
                break;
            case PlatformSetting.PlatformType.Ground:
                spriteRenderer.sprite = sprites[platformSettingRef.groundPlatform.index];
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
