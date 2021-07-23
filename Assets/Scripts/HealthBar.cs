using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public Slider slider;

  // set max health
  public void SetMaxHealth(float health)
  {
    slider.maxValue = health;
    slider.value = health;
  }
  // update current health 
  public void UpdateHealth(float health)
  {
    slider.value = health;
  }
}
