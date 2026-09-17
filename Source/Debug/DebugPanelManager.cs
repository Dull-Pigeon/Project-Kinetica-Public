using TMPro;
using UnityEngine;

public class DebugPanelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;

    void OnEnable()
    {
        WeaponCollision.OnImpactOccured += DisplayDebugPanel;
    }

    void OnDisable()
    {
        WeaponCollision.OnImpactOccured -= DisplayDebugPanel;
    }

    private void DisplayDebugPanel(ImpactData impactData)
    {
        debugText.text = 
            $"IMPACT DEBUG\n\n" + 

            $"[MATERIAL]\n" +
            $"Name: {impactData.physicalMaterial.materialName}\n" + 
            $"Hardness: {impactData.physicalMaterial.hardness:F2}\n" + 
            $"Elasticity: {impactData.physicalMaterial.elasticity:F2}\n" + 
            $"Critical Pressure: {impactData.physicalMaterial.criticalPressure:F2}\n\n" + 
            
            $"[WEAPON]\n" + 
            $"Name: {impactData.weaponData.weaponName}\n" + 
            $"Hardness: {impactData.weaponData.hardness:F2}\n" + 
            $"Mass: {impactData.weaponData.mass:F2}\n" + 
            $"Area: {impactData.weaponData.area:F2}\n" + 
            $"Radius: {impactData.weaponData.radius:F2}\n\n"+ 

            $"[RESULT]\n" +
            $"Alignment: {impactData.impactResult.impactAlignment:F2}\n" + 
            $"Type: {impactData.impactResult.resultType}\n" +
            $"Pressure: {impactData.impactResult.contactPressure:F2}\n" +
            $"Deform Force: {impactData.impactResult.deformForce.magnitude:F2}\n" +
            $"Broken: {(impactData.impactResult.isBroken ? "YES" : "NO")}\n" + 
            $"Velocity: {impactData.velocity.magnitude:F2}\n" + 
            $"Impact Point: {impactData.impactPoint}"; 
    }
}
