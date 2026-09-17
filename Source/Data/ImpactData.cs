using UnityEngine;

public readonly struct ImpactData
{
    public readonly PhysicalMaterialSO physicalMaterial;
    public readonly WeaponDataSO weaponData;
    public readonly ImpactResult impactResult;
    public readonly Vector3 impactPoint;
    public readonly Vector3 surfaceNormal;
    public readonly Vector3 velocity;

    public ImpactData(
        PhysicalMaterialSO physicalMaterial,
        WeaponDataSO weaponData,
        ImpactResult impactResult,
        Vector3 impactPoint,
        Vector3 surfaceNormal,
        Vector3 velocity
    )
    {
        this.physicalMaterial = physicalMaterial;
        this.weaponData = weaponData;
        this.impactResult = impactResult;
        this.impactPoint = impactPoint;
        this.surfaceNormal = surfaceNormal;
        this.velocity = velocity;
    }
}
