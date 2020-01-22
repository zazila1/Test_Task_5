using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string _Name;
    public Sprite _Icon;
    [SerializeField] private int _Damage;
    [SerializeField] private float shootDelay;
    
    private ParticleSystem _ShootEffect;
    private bool _CanShoot = true;

    private void Awake()
    {
        _ShootEffect = GetComponent<ParticleSystem>();
    }

    public int GetWeaponDamage()
    {
        return _Damage;
    }

    public void Shoot()
    {
        if (_CanShoot)
        {
            _ShootEffect.Play();
            StartCoroutine(ShootingDelay(shootDelay));
        }    
    }

    private IEnumerator ShootingDelay(float delay)
    {
        _CanShoot = false;
        yield return new WaitForSeconds(delay);
        _CanShoot = true;
    }
}
