using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public int _damage { get; private set; }

    void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    public void ShootDirection(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    public void SetDamage(int dmg)
    {
        if(_damage != 0) return;
        _damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name + " has colided with bullet");
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(this);

        }
    }
}
