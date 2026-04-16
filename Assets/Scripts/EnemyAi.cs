using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))] // Make sure we have an Animator
public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 3f;
    public int health = 3; 

    private Transform player;
    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    
    // We need this to stop the zombie from chasing you after it dies
    private bool isDead = false; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>(); // Grab the collider
        
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
        // 1. If the zombie is dead (or player is missing), STOP moving!
        if (isDead || player == null) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // 2. Normal chasing logic
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void TakeDamage(int damage)
    {
        // Don't take more damage if already dead
        if (isDead) return; 

        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Set the flag so FixedUpdate stops moving the zombie
        isDead = true; 
        
        // 1. Play the death animation
        anim.SetTrigger("Die");

        // 2. Turn off the collider so the player and bullets pass right over the body
        coll.enabled = false;

        // 3. Optional visual polish: Push the dead body to a lower sorting layer
        // so living zombies and the player walk "on top" of it.
        GetComponent<SpriteRenderer>().sortingOrder = -2;

        // 4. The 5-Second Vanish!
        // The Destroy function lets you pass in a time delay in seconds.
        Destroy(gameObject, 5f);
    }
}