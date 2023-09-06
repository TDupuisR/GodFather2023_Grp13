using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusModeCooldown : MonoBehaviour
{
    Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        //FocusMode += UpdateCooldown;
    }

    private void OnDisable()
    {
        //FocusMode -= UpdateCooldown;
    }

    void UpdateCooldown(float cooldown)
    {
        slider.value = cooldown;
    }
}
