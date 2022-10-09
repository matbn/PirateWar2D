using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Color lowColor;
    [SerializeField] private Color highColor;
    [SerializeField] private Image fillRef;

    public void SetupSlider(float maxHealth) {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        fillRef.color = Color.Lerp(lowColor, highColor, healthBar.normalizedValue);
    }
    public void SetHealth(float health)
    {
        healthBar.value = health;
        fillRef.color = Color.Lerp(lowColor, highColor, healthBar.normalizedValue);
    }
}
