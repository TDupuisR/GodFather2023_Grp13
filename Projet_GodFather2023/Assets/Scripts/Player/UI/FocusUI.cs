using UnityEngine;
using UnityEngine.UI;

public class FocusUI : MonoBehaviour
{
    [SerializeField] Slider m_slider;

    private void OnEnable()
    {
        FocusMode.OnFocusUse += ChangeValue;
    }
    private void OnDisable()
    {
        FocusMode.OnFocusUse -= ChangeValue;
    }

    void ChangeValue(float value)
    {
        m_slider.value = value;
    }
}
