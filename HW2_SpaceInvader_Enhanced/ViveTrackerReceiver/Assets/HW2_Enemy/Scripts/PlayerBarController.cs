using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBar;

    public void UpdateHealth(float fraction)
    {
        healthBar.fillAmount = fraction;
    }
}
