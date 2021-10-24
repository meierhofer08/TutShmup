using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    private bool _hasSpawn;
    private MoveBehaviour _moveBehaviour;
    private WeaponBehaviour[] _weapons;
    private Collider2D _colliderComponent;
    private SpriteRenderer _rendererComponent;

    private void Awake()
    {
        // Retrieve the weapon only once
        _weapons = GetComponentsInChildren<WeaponBehaviour>();

        // Retrieve scripts to disable when not spawn
        _moveBehaviour = GetComponent<MoveBehaviour>();

        _colliderComponent = GetComponent<Collider2D>();

        _rendererComponent = GetComponent<SpriteRenderer>();
    }

    // 1 - Disable everything
    private void Start()
    {
        _hasSpawn = false;

        // Disable everything
        // -- collider
        _colliderComponent.enabled = false;
        // -- Moving
        _moveBehaviour.enabled = false;
        // -- Shooting
        foreach (var weapon in _weapons)
        {
            weapon.enabled = false;
        }
    }

    void Update()
    {
        // 2 - Check if the enemy has spawned.
        if (_hasSpawn == false)
        {
            if (_rendererComponent.IsVisibleFrom(Camera.main))
            {
                Spawn();
            }
        }
        else
        {
            // Auto-fire
            foreach (var weapon in _weapons)
            {
                if (weapon != null && weapon.enabled && weapon.CanAttack)
                {
                    weapon.Attack(true);
                    SoundEffectsHelper.Instance.MakeEnemyShotSound();
                }
            }

            // 4 - Out of the camera ? Destroy the game object.
            if (_rendererComponent.transform.position.x < Camera.main.transform.position.x &&
                _rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
        }
    }

    // 3 - Activate itself.
    private void Spawn()
    {
        _hasSpawn = true;

        // Enable everything
        // -- Collider
        _colliderComponent.enabled = true;
        // -- Moving
        _moveBehaviour.enabled = true;
        // -- Shooting
        foreach (var weapon in _weapons)
        {
            weapon.enabled = true;
        }
    }
}