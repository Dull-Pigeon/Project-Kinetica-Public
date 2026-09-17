using UnityEngine;

public class ImpactReceiver : MonoBehaviour
{
    private Renderer targetRender;
    public PhysicalMaterialSO physicalMaterial;
    private MaterialPropertyBlock propertyBlock;
    private static readonly int impactPoint = Shader.PropertyToID("_ImpactPoint");
    private static readonly int deformForce = Shader.PropertyToID("_DeformForce");
    private static readonly int impactRadius = Shader.PropertyToID("_ImpactRadius");
    private static readonly int hitTime = Shader.PropertyToID("_HitTime");
    private static readonly int frequency = Shader.PropertyToID("_Frequency");
    private static readonly int decay = Shader.PropertyToID("_Decay");
    private static readonly int holdTime = Shader.PropertyToID("_HoldTime");
    private static readonly int reboundFactor = Shader.PropertyToID("_ReboundFactor");

    void Awake()
    {
        targetRender = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void ReceiveImpact(Vector3 hitPoint, Vector3 hitForce, float hitRadius)
    {
        propertyBlock.SetVector(impactPoint, hitPoint);
        propertyBlock.SetVector(deformForce, hitForce / (1 + Mathf.Pow(physicalMaterial.hardness, 2f)) * Mathf.Lerp(0f, 0.08f, physicalMaterial.elasticity));
        propertyBlock.SetFloat(impactRadius, hitRadius);
        propertyBlock.SetFloat(hitTime, Time.time);
        propertyBlock.SetFloat(frequency, 10 + (1 - physicalMaterial.elasticity) * 40);
        propertyBlock.SetFloat(decay, 1 + (1 - physicalMaterial.elasticity) * 29);
        propertyBlock.SetFloat(holdTime, 0.1f / (1f + physicalMaterial.elasticity));
        propertyBlock.SetFloat(reboundFactor, 0.5f * physicalMaterial.elasticity);

        targetRender.SetPropertyBlock(propertyBlock);
    }
}
