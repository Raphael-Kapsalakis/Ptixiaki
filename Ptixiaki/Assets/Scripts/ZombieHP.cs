using UnityEngine;
using UnityEngine.UI;

public class ZombieHp : MonoBehaviour
{
    public Slider healthSlider;

    public void SetHealth(float value)
    {
        healthSlider.value = Mathf.Clamp01(value);
    }

}



