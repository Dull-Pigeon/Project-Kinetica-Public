using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    public WeaponDataSO weaponData;
    private readonly HashSet<Collider> hitColliders = new HashSet<Collider>();
    private bool isAttacking = false;
    private Vector3 previousPosition;
    public static event Action<ImpactData> OnImpactOccured;
    private LayerMask layerMask;
    [SerializeField] private Transform hitPoint;

    void Awake()
    {
        layerMask = LayerMask.GetMask("Target");
    }

    void FixedUpdate()
    {
        Vector3 displacement = hitPoint.position - previousPosition;
        Vector3 velocity = displacement / Time.fixedDeltaTime;
        Vector3 force = weaponData.mass * velocity;
        float distance = displacement.magnitude;

        if (isAttacking && distance > 0)
        {
            PerformSweep(displacement, distance, force, velocity);
        }

        previousPosition = hitPoint.position;
    }

    public void StartAttack()
    {
        isAttacking = true;
        hitColliders.Clear();
        previousPosition = hitPoint.position;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }

    private void PerformSweep(Vector3 displacement, float distance, Vector3 force, Vector3 velocity)
    {
        Vector3 direction = displacement / distance;

        if (Physics.SphereCast(
            previousPosition, 
            weaponData.radius, 
            direction, 
            out RaycastHit hitInfo, 
            distance,
            layerMask
            )
        )
        {
            if (hitColliders.Add(hitInfo.collider))
            {
                ProcessHit(hitInfo, force, velocity);
            }
        }
    }

    private void ProcessHit(RaycastHit hitInfo, Vector3 force, Vector3 velocity)
    {
        if (hitInfo.collider.TryGetComponent<ImpactReceiver>(out var receiver))
        {
            PhysicalMaterialSO physicalMaterial = receiver.physicalMaterial;
            ImpactResult result = ImpactSolver.EvaluateImpact(
                force,
                hitInfo.normal,
                physicalMaterial,
                weaponData
            );

            if (result.resultType != ImpactResultType.Glanced)
            {
                receiver.ReceiveImpact(
                    hitInfo.point,
                    result.deformForce,
                    weaponData.area
                );
            }

            OnImpactOccured?.Invoke(
                PackImpactData(
                    physicalMaterial,
                    result, 
                    hitInfo, 
                    velocity
                )
            );
        }
    }
    private ImpactData PackImpactData(PhysicalMaterialSO physicalMaterial, ImpactResult result, RaycastHit hitInfo, Vector3 velocity)
    {
        ImpactData impactData = new ImpactData(
            physicalMaterial,
            weaponData,
            result,
            hitInfo.point,
            hitInfo.normal,
            velocity
        );

        return impactData;
    }
}
