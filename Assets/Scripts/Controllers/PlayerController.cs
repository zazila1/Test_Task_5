using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _Score = 0;

    [SerializeField] private Weapon _CurrentWeapon;
    [SerializeField] private List<Weapon> _Weapons;
    
    public Action<int> _OnScoreChanged;
    
    private void Awake()
    {
        Application.targetFrameRate = 180;
        _CurrentWeapon = _Weapons[0];
    }

    private void Start()
    {
        _OnScoreChanged?.Invoke(_Score);
    }

    public void OnEnemyKilled(int reward)
    {
        _Score += reward;
        _OnScoreChanged?.Invoke(_Score);
    }


    private void Update()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            _CurrentWeapon.Shoot();
        }

        if (Input.GetKey("1"))
        {
            _CurrentWeapon = _Weapons[0] != null ? _Weapons[0] : _CurrentWeapon;
        }
        if (Input.GetKey("2"))
        {
            _CurrentWeapon = _Weapons[1] != null ? _Weapons[1] : _CurrentWeapon;
        }
        if (Input.GetKey("3"))
        {
            _CurrentWeapon = _Weapons[2] != null ? _Weapons[2] : _CurrentWeapon;
        }
    }
}
