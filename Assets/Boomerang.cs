using System;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    private Player.Arms._3.Arm3 throwingScript;
    private Transform player;
    private Rigidbody2D rb;
    private bool isReturning = false;
    private float returnSpeed;
    private float returnTime = 3f;

    public void Initialize(Player.Arms._3.Arm3 throwingScript, Transform player, float throwSpeed, float returnSpeed)
    {
        this.throwingScript = throwingScript;
        this.player = player;
        this.returnSpeed = returnSpeed;
        rb = GetComponent<Rigidbody2D>();

        Invoke(nameof(StartReturning), returnTime);
    }

    private void Update()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }

    void StartReturning()
    {
        isReturning = true;
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            rb.linearVelocity = directionToPlayer * returnSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            other.SendMessage("OnBoomerangHit", SendMessageOptions.DontRequireReceiver);
        }
        else if (other.CompareTag("Player") && isReturning)
        {
            throwingScript.OnBoomerangDestroyed();
            Destroy(gameObject);
        }
    }
}
