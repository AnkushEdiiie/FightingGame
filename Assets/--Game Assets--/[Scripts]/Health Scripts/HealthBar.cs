using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthslider;
    public float health;
    private float lerpSpeed = 0.005f;

    public void UpdateSlider()
    {
        if(healthSlider.value != health)
        {
            healthSlider.value = health;
        }

        if(healthSlider.value != easeHealthslider.value)
        {
            easeHealthslider.value = Mathf.Lerp(easeHealthslider.value, healthSlider.value, lerpSpeed);
        }
    }
}
