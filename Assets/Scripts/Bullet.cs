using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f; // How long before the bullet deletes itself

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Shoot forward (transform.right is the "forward" direction for 2D sprites)
        rb.linearVelocity = transform.right * speed; 
        
        // Destroy the bullet after a few seconds so it doesn't lag the game forever
        Destroy(gameObject, lifeTime);
    }

    // We will use this later to damage enemies!
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if the object we hit has the EnemyAI script attached
        EnemyAI enemy = hitInfo.GetComponent<EnemyAI>();
        
        // If it DOES have the script, that means it's an enemy!
        if (enemy != null)
        {
            enemy.TakeDamage(1); // Deal 1 damage
        }

        // Destroy the bullet after it hits anything (wall or enemy)
        Destroy(gameObject); 
    }
}