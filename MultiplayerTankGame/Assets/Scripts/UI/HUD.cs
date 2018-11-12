using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    [SerializeField] private Image _healthBar;

    public void UpdateHealthDisplay(float maxHealth, float currentHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }
}
