using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private TextMeshProUGUI healthText;
    private Health _health;
    private Spawner spwnr;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void SetSpawner(Spawner spw) => spwnr = spw;

    public void TakeDamage(Bullet b)
    {
        _health.TakeDamage(b._damage);
        if (_health.health <= 0)
        {
            spwnr.DestroyedEnemies();
        }
        spwnr.AddPoints((ulong)b._damage);
        healthText.text = _health.health.ToString();
        if(b.gameObject != null)
            Destroy(b.gameObject);
    }

    private void Update()
    {
        Movement();
    }

    public void SetHealth(int helath)
    {
        _health.SetHealth(helath);
        healthText.text = _health.health.ToString();
    }

    private void Movement()
    {
        transform.position = transform.position - (Vector3.down * (_speed * 3 * Time.deltaTime));
        if(transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

}
