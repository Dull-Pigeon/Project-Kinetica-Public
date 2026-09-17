using UnityEngine;

public class ImpactVFXManager : MonoBehaviour
{
    void OnEnable()
    {
        WeaponCollision.OnImpactOccured += HandleImpactVFX;
    }

    void OnDisable()
    {
        WeaponCollision.OnImpactOccured -= HandleImpactVFX;
    }

    private void HandleImpactVFX(ImpactData impactData)
    {
        Vector3 position = impactData.impactPoint + 0.1f * Vector3.down;
        Quaternion rotation = Quaternion.LookRotation(impactData.surfaceNormal);
        Instantiate(impactData.physicalMaterial.impactVFXPrefab, position, rotation);
    }
}
