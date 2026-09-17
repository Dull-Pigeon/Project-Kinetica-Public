using UnityEngine;

[CreateAssetMenu(fileName = "PhysicalMaterial_", menuName = "Kinetica/Physical Material")]
public class PhysicalMaterialSO : ScriptableObject
{
    [Header("Physical Properties")]
    public string materialName = "Default";

    [Tooltip("Hardness Level (1 - 10)")]
    [Range(1f, 10f)]
    public float hardness = 1f;

    [Tooltip("Elasticity (0 - 1)")]
    [Range(0f, 1f)]
    public float elasticity = 0.1f;

    [Tooltip("Critical Pressure")]
    public float criticalPressure = 50f;

    [Tooltip("Visual Effects")]
    public GameObject impactVFXPrefab;
}
