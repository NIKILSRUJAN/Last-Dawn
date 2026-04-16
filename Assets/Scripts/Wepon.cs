using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float timeBetweenShots = 0.2f; // Lower = faster shooting

    private Camera mainCam;
    private float nextShotTime;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        // 1. Aim the gun at the mouse cursor
        AimAtMouse();

        // 2. Shoot if holding Left Click
        if (Mouse.current.leftButton.isPressed && Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + timeBetweenShots; // Reset the cooldown timer
        }
    }

    void AimAtMouse()
    {
        if (Mouse.current == null) return;

        // Find the mouse in the world
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(mouseScreenPos);

        // Calculate the direction from the gun to the mouse
        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        // Point the gun's X-axis (right) towards that direction
        transform.right = direction; 
    }

    void Shoot()
    {
        // Clone the bullet prefab at the exact position and rotation of the FirePoint
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}