using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShockWaveUIManager : MonoBehaviour
{
    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    public void UpdateGauge(int newvalue)
    {
        slider.value = newvalue;
    }
}
