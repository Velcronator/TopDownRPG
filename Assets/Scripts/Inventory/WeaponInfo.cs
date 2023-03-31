using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;        
}
