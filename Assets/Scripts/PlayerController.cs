using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _ShootEffect;

    private bool canShoot = true;

    private float shootDelay = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
