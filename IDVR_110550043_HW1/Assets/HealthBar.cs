using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image healthBarFill; // Reference to the Image component representing the health bar fill.
    public EnemyController enemyController; // Reference to the EnemyController script.

    // Initialize the health bar with the enemy's controller.
    public void Initialize(EnemyController controller)
    {
        enemyController = controller;
        healthBarFill.enabled = true;

        // Assuming that the health bar fill starts at full.
        healthBarFill.fillAmount = 1.0f;
    }

    // Update the health bar fill amount based on the enemy's health.
    private void Update()
    {
        if (enemyController != null)
        {
            // Calculate the fill amount as a ratio of current health to max health.
            float fillAmount = enemyController.CurrentHealth / enemyController.MaxHealth;

            // Clamp the fill amount to ensure it stays between 0 and 1.
            fillAmount = Mathf.Clamp01(fillAmount);

            // Update the health bar fill.
            
            healthBarFill.fillAmount = fillAmount;

        }
    }
}