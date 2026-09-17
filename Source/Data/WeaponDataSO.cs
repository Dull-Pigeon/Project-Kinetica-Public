using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData_", menuName = "Kinetica/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    [Header("Weapon Propertities")]
    public string weaponName = "Default";

    [Tooltip("Weapon Hardness (1-10)")]
    [Range(1f, 10f)]
    public float hardness = 5f;

    [Tooltip("Weapon Mass")]
    [Range(1f, 10f)]
    public float mass = 2f;

    [Tooltip("Contact Area")]
    [Range(0.01f, 1f)]
    public float area = 0.2f;

    [Tooltip("Detection Radius")]
    [Range(0.01f, 0.5f)]
    public float radius = 0.1f;
}
