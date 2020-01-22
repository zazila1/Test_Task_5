using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ShootEffect;
    [SerializeField] private int _Points = 0;

    //[SerializeField] private Weapon _CurrentWeapon;
    
    private bool canShoot = true;
    private float shootDelay = 0.01f;

    
    void Start()
    {
    }

    public void OnEnemyKilled(int reward)
    {
        _Points += reward;
    }
    
    private void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {
            _ShootEffect.Play();
            StartCoroutine(ShootingDelay(shootDelay));
        }    
    }

    private IEnumerator ShootingDelay(float delay)
    {
        canShoot = false;
        yield return new WaitForSeconds(delay);
        canShoot = true;
    }
}
