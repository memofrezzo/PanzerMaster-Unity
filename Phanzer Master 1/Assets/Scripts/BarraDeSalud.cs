using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarCanvas : MonoBehaviour
{
    [SerializeField] private Image barImage;

    // Start is called before the first frame update
    public void UpdateHealthBar(float maxHealth, float health)
    {
        barImage.fillAmount = health / maxHealth;
    }
}
