using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatHealthBar : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthBar(float currentHealth, float maxHealth)
    {
        if (currentHealth <= 0)
        {
            // Destroy the entire GameObject this script is attached to (which includes the Slider component)
            GameObject.Destroy(gameObject);
        }
        slider.value = currentHealth / maxHealth;
    }
}
