using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public string _Name;
    public Sprite _Icon;
    public ParticleSystem _ShootEffect;
}
