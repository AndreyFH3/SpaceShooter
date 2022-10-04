using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Gun : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float fireRate;
    [SerializeField] private float bulletForce;
    [SerializeField] private int damage;
    private IEnumerator ShootCoroutine;


    private void Awake()
    {
        ShootCoroutine = Shooting();    
    }


    public void StartShoot()
    {
        StartCoroutine(ShootCoroutine);
    }

    public void StopShoot()
    {
        StopCoroutine(ShootCoroutine);
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds shootingRate = new WaitForSeconds(1f / YandexGame.savesData.shootingSpeed);
        while (true)
        {
            GenerateBullet();
            yield return shootingRate;
        }
    }

    private void GenerateBullet()
    {
        Bullet b = Instantiate(_bullet, transform.position, transform.rotation, transform);
        b.SetDamage(damage * YandexGame.savesData.ShootingDamage);
        b.ShootDirection(transform.TransformDirection(Vector2.up * bulletForce));
    }

}
