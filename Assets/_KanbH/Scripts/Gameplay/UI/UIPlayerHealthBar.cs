using UnityEngine;
using UnityEngine.UI;

public class UIPlayerHealthBar : MonoBehaviour
{
    private Slider _healthBarSlider;

    private void Awake()
    {
        _healthBarSlider = GetComponent<Slider>();
    }

    public void UpdateHealthBar(float fillAmount)
    {
        Debug.Log("health bar changed to "+fillAmount);
        _healthBarSlider.value = fillAmount;
    }
}
