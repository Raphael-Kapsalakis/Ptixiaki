using UnityEngine;

public class MedkitPickup : MonoBehaviour
{
    public int healAmount = 100;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out PlayerHealth health))
        {
            health.Heal(healAmount);
            gameObject.SetActive(false); // Deactivate instead of destroy
        }
    }
}

