using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player controller and behavior
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// 1 - The speed of the ship
    /// </summary>
    [SerializeField] private Vector2 speed = new Vector2(50, 50);

    // 2 - Store the movement and the component
    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;

    private void Start()
    {
        // 5 - Get the component and store the reference
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 3 - Retrieve axis information
        var inputX = Input.GetAxis("Horizontal");
        var inputY = Input.GetAxis("Vertical");

        // 4 - Movement per direction
        _movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY);

        // 5 - Shooting
        bool shoot = Input.GetButton("Fire1");
        shoot |= Input.GetButton("Fire2");
        // Careful: For Mac users, ctrl + arrow is a bad idea

        if (shoot)
        {
            var weapon = GetComponent<WeaponBehaviour>();
            if (weapon != null)
            {
                // false because the player is not an enemy
                weapon.Attack(false);
                SoundEffectsHelper.Instance.MakePlayerShotSound();
            }
        }
        
        // 6 - Make sure we are not outside the camera bounds
        var dist = (transform.position - Camera.main.transform.position).z;

        var leftBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, dist)
        ).x;

        var rightBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(1, 0, dist)
        ).x;

        var topBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 0, dist)
        ).y;

        var bottomBorder = Camera.main.ViewportToWorldPoint(
            new Vector3(0, 1, dist)
        ).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
        );
    }

    private void FixedUpdate()
    {
        // 6 - Move the game object
        _rigidbodyComponent.velocity = _movement;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        bool damagePlayer = false;

        // Collision with enemy
        var enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            // Kill the enemy
            var enemyHealth = enemy.GetComponent<HealthBehaviour>();
            if (enemyHealth != null) enemyHealth.Damage(enemyHealth.hp );

            damagePlayer = true;
        }

        // Damage the player
        if (damagePlayer)
        {
            var playerHealth = GetComponent<HealthBehaviour>();
            if (playerHealth != null) playerHealth.Damage(1);
        }
    }
    
    private void OnDestroy()
    {
        // Game Over.
        var gameOver = FindObjectOfType<GameOverBehaviour>();
        if (gameOver != null)
        {
            gameOver.ShowButtons();
        }
    }
}
