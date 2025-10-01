using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poition : MonoBehaviour
{
    public enum Effect
    {
        health, 
        exp,
        oneKick,
        dashUp
    }

    [SerializeField] private Effect _effect;
    [SerializeField] private Health _health;
    [SerializeField] private Exp _exp;

    public Effect EffectPoition { get => _effect; set => _effect = value; }
    
    public float Health { get => _health.value; set => _health.value = value; }

    public float Exp { get => _exp.value; set => _exp.value = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (_effect)
            {
                case Effect.health:
                    {
                        collision.GetComponent<HealthSystem>().AddHealth(_health.value);
                    }
                    break;
                case Effect.exp:
                    {
                        collision.GetComponent<HealthSystem>().Upgrade.AddExpirience(_exp.value);
                    }
                    break;
                case Effect.dashUp:
                    {
                        Slider slider = collision.GetComponent<PlayerMoviement>().DashSlider;
                        slider.value = slider.maxValue;
                    }
                    break;
                case Effect.oneKick:
                    {
                        collision.GetComponent<HealthSystem>().CanDamage = false;
                    }
                    break;
            }

            Destroy(gameObject);
        }
    }
}

[Serializable]
public struct Health
{
    public float value;
}

[Serializable]
public struct Exp
{
    public float value;
}

[Serializable]
public struct DashUp
{
    public void Recovery(Slider slider)
    {
        slider.value = slider.maxValue;
    }    
}
