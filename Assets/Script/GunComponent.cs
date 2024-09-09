using UnityEngine;

public class GunComponent : MonoBehaviour
{
    // Reference to the bullet prefab that will be instantiated
    public GameObject bulletPrefab;

    // The location from where the bullet will be fired
    public Transform bulletSpawnPoint;

    // How much the bullet speed increases per second of charging
    public float chargeSpeed = 10f;

    // Maximum amount of time the player can charge their shot (limits how fast the bullet can get)
    public float maxChargeTime = 3f;

    // Time between shots when holding the fire button(dont want crazy spam)
    public float fireRate = 0.2f;

    // Tracks how long the fire button is held down to calculate bullet speed
    private float chargeTime = 0f;

    // The earliest time at which another shot can be fired (used to limit fire rate)
    private float nextFireTime = 0f;

    void Update()
    {
        // Detect when Fire1 is first pressed down (button down)
        if (Input.GetButtonDown("Fire1"))
        {
            // Reset charge time
            chargeTime = 0f;
        }

        // Detect if Fire1 is being held down (button is held, use get)
        if (Input.GetButton("Fire1"))
        {
            // Increase charge time over time
            chargeTime += Time.deltaTime;

            // Clamp the charge time to maxChargeTime
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime); 

            if (Time.time >= nextFireTime)
            {
                Shoot();

                // Set next allowed fire time
                nextFireTime = Time.time + fireRate; 
            }
        }

        // Detect when Fire1 is released (button up)
        if (Input.GetButtonUp("Fire1"))
        {
            // Shoot once on release with the final charge time
            Shoot();

            // Reset charge time after firing
            chargeTime = 0f; 
        }
    }

    //shoot method 
    void Shoot()
    {
        // Spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Access bullet component to modify its speed based on charge time
        BulletComponent bulletComp = bullet.GetComponent<BulletComponent>();
        if (bulletComp != null)
        {
            // Set the bullet's speed based on how long the fire button was held down(bulletspeed class being used)
            bulletComp.bulletSpeed = chargeTime * chargeSpeed; 
        }
    }
}
