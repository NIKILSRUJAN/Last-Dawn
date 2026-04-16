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
        // For now, just destroy the bullet if it hits any collider (like a wall)
        Destroy(gameObject); 
    }
}