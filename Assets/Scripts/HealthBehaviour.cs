using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle hitpoints and damages
/// </summary>
public class HealthBehaviour : MonoBehaviour
{
    /// <summary>
    /// Total hitpoints
    /// </summary>
    public int hp = 1;

    /// <summary>
    /// Enemy or player?
    /// </summary>
    [SerializeField] private bool isEnemy = true;

    /// <summary>
    /// Inflicts damage and check if the object should be destroyed
    /// </summary>
    /// <param name="damageCount"></param>
    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // 'Splosion!
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            
            // Boom!
            SoundEffectsHelper.Instance.MakeExplosionSound();
            
            // Dead!
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        var shot = otherCollider.gameObject.GetComponent<ShotBehaviour>();
        if (shot != null)
        {
            // Avoid friendly fire
            if (shot.isEnemyShot != isEnemy)
            {
                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }
}
