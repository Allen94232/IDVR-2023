using UnityEngine;
using UnityEngine.UI;

public class CooldownIndicator : MonoBehaviour
{
    public GameObject pc;
    public float cooldownTime = 0.0f;
    private float cooldownTimer;
    private Image progressBar;


    void Start()
    {
        progressBar = GetComponent<Image>();
        cooldownTimer = 0.0f;
    }

    void Update()
    {
        cooldownTime = pc.GetComponent<PlayerController>().fireCoolDown;

        // If the cooldown timer is running, update the progress bar fill amount.
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;

            // Update the progress bar fill amount based on the remaining cooldown time.
            progressBar.fillAmount = cooldownTimer / cooldownTime;
        }
        else
        {
            // Cooldown is complete; set the progress bar to full.
            progressBar.fillAmount = 0;
        }
    }

    // Call this function when the "on fire" action is triggered.
    public void StartCooldown()
    {
        cooldownTimer = cooldownTime;
    }
}