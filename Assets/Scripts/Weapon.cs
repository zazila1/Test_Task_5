using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : MonoBehaviour
{
    public string _Name;
    public Sprite _Icon;
    [SerializeField] private int _Damage;

    public int GetWeaponDamage()
    {
        return _Damage;
    }
}
