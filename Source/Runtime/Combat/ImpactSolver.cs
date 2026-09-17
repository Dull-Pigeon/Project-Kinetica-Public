using UnityEngine;

    public enum ImpactResultType
    {
        Glanced,
        Deflected,
        Squashed,
        Penetrated
    }

    public struct ImpactResult
    {        
        public float impactAlignment;
        public ImpactResultType resultType;
        public Vector3 deformForce;
        public float contactPressure;
        public bool isBroken;
    }

    public static class ImpactSolver
    {
        private const float GLANCE_DOT_THRESHOLD = 0.25f;

        public static ImpactResult EvaluateImpact(
            Vector3 attackForce,
            Vector3 surfaceNormal,
            PhysicalMaterialSO physicalMaterial,
            WeaponDataSO weaponData
        )
        {
            ImpactResult result = new ImpactResult();

            Vector3 normalizedAttackDirection = attackForce.normalized;
            Vector3 normalizedSurfaceNormal = surfaceNormal.normalized;

            result.impactAlignment = Vector3.Dot(normalizedAttackDirection, -normalizedSurfaceNormal);

            if (result.impactAlignment < GLANCE_DOT_THRESHOLD)
            {
                result.resultType = ImpactResultType.Glanced;
                result.deformForce = Vector3.zero;
                result.contactPressure = 0f;
                result.isBroken = false;
                return result;
            }

            float effectiveForce = attackForce.magnitude * result.impactAlignment;
            result.deformForce = -normalizedSurfaceNormal * effectiveForce;

            if (weaponData.hardness < physicalMaterial.hardness)
            {
                result.resultType = ImpactResultType.Deflected;
                result.deformForce *= 0.2f;
                result.contactPressure = 0f;
                result.isBroken = false;
                return result;
            }

            float effectivePressure = effectiveForce / weaponData.area;
            result.contactPressure = effectivePressure;

            if (effectivePressure < physicalMaterial.criticalPressure)
            {
                result.resultType = ImpactResultType.Squashed;
                result.isBroken = false;
            }
            else
            {
                result.resultType = ImpactResultType.Penetrated;
                result.isBroken = true;
            }

            return result;
        }
    }
