using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Ground : MonoBehaviour
{
    [SerializeField] float onGroundHeight = 10f;

    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    Player player;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        Resize();
    }

    void Resize()
    {
        transform.localScale = Vector3.one;
        float width = spriteRenderer.bounds.size.x;
        float targetwidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        Vector3 Scale = transform.localScale;
        Scale.x = targetwidth / width;
        transform.localScale = Scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        if(collision.gameObject.tag == "Player")
        {
            player.Jump(onGroundHeight);
        }
    }
}
