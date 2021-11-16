using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;

    public Slider slider;

    public Image fill;

    [SerializeField]
    TactictsMove UnitStat;

    void Start()
    {
        SetMaxHealth();
    }
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 0, 0);
        SetHealth();
    }
    public void SetMaxHealth()
    {
        slider.maxValue = UnitStat.MaxHealthStat;
        slider.value = UnitStat.CurrentHealthStat;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth()
    {
        slider.value = UnitStat.CurrentHealthStat;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
