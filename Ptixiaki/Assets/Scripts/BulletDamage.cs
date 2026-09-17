using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damageAmount = 20;
    public float lifetime = 3f;
    public GameObject bloodEffectPrefab;
    public float bloodEffectLifetime = 1.5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if we hit a zombie
        ZombieAI zombie = collision.gameObject.GetComponent<ZombieAI>();
        if (zombie != null)
        {
            zombie.TakeDamage(damageAmount);

            // Spawn blood effect at hit point
            if (bloodEffectPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                GameObject bloodFX = Instantiate(bloodEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

                // Destroy blood effect after a short delay
                Destroy(bloodFX, bloodEffectLifetime);
            }
        }

        // Destroy bullet on any hit
        Destroy(gameObject);
    }
}
