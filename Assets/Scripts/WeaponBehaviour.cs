using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponBehaviour : MonoBehaviour
{
    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    /// <summary>
    /// Projectile prefab for shooting
    /// </summary>
    public Transform shotPrefab;

    /// <summary>
    /// Cooldown in seconds between two shots
    /// </summary>
    public float shootingRate = 0.25f;

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float _shootCooldown;

    private void Start()
    {
        _shootCooldown = 0f;
    }

    private void Update()
    {
        if (_shootCooldown > 0)
        {
            _shootCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    /// <summary>
    /// Create a new projectile if possible
    /// </summary>
    public void Attack(bool isEnemy)
    {
        if (!CanAttack)
        {
            return;
        }

        _shootCooldown = shootingRate;

        // Create a new shot
        var shotTransform = Instantiate(shotPrefab);

        // Assign position
        shotTransform.position = transform.position;

        // The is enemy property
        var shot = shotTransform.gameObject.GetComponent<ShotBehaviour>();
        if (shot != null)
        {
            shot.isEnemyShot = isEnemy;
        }

        // Make the weapon shot always towards it
        var move = shotTransform.gameObject.GetComponent<MoveBehaviour>();
        if (move != null)
        {
            move.direction = transform.right; // towards in 2D space is the right of the sprite
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack => _shootCooldown <= 0f;
}