using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViegoUI : MonoBehaviour
{
    public Slider HealthBarSlider;
    public Slider ExperienceBarSlider;
    void Update()
    {
        HealthBarSlider.value = GetComponentInParent<Viego>().hp/GetComponentInParent<Viego>().maxHp;
        ExperienceBarSlider.value = GetComponentInParent<Viego>().exp / GetComponentInParent<Viego>().maxExp;
    }
}
