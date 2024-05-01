using UnityEngine;

public static class Tools
{
    public static bool InRadius(Vector3 initialPos, Vector3 target, float radius)
    {
        return (target-initialPos).sqrMagnitude <= radius * radius;
    }

    public static bool InAngle(Transform transform, Vector3 target, float angle)
    {
        var ang = Vector3.Angle(transform.forward, target - transform.position);
        return ang < angle * .5f && ang > -angle * .5f;
    }

    public static bool InSight(Vector3 initialPos, Vector3 target, LayerMask visionObstacles)
    {
        return !Physics.Raycast(initialPos, target, (target-initialPos).magnitude, visionObstacles);
    }
}