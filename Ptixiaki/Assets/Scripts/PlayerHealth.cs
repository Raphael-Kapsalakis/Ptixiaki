using UnityEngine.SceneManagement;
using UnityEngine;



public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start() => currentHealth = maxHealth;

    public void TakeDamage(float amount)
    {
        currentHealth -= Mathf.RoundToInt(amount);
        Debug.Log("Player took damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died.");

        // Optional: Add delay before loading menu
        Invoke(nameof(LoadMainMenu), 2f); // 2 seconds after death
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with your actual menu scene name
    }
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log("Player healed. Current Health: " + currentHealth);
    }

}
