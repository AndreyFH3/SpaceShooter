using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI helthText;
    [SerializeField] private ParticleSystem _explosion;

    public int health { get; private set;
    }

    public void SetHealth(int hel)
    {
        health = hel;
    }

    public void TakeDamage(int Damage)
    {
        health -= Damage;
        if (health <= 0) Death();
    }

    private void Death()
    {
        ParticleSystem particle = Instantiate(_explosion,transform.position, Quaternion.identity);
        Destroy(particle.gameObject, 0.5f);
        Destroy(gameObject);
    }
}
