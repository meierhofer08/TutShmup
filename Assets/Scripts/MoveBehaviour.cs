using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simply moves the current game object
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class MoveBehaviour : MonoBehaviour
{
    // 1 - Designer variables

    /// <summary>
    /// Object speed
    /// </summary>
    [SerializeField] private Vector2 speed = new Vector2(10, 10);

    /// <summary>
    /// Moving direction
    /// </summary>
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 _movement;
    private Rigidbody2D _rigidbodyComponent;

    private void Start()
    {
        _rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 2 - Movement
        _movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y);
    }

    private void FixedUpdate()
    {
        // Apply movement to the rigidbody
        _rigidbodyComponent.velocity = _movement;
    }
}