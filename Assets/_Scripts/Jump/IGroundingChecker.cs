using UnityEngine;

namespace Jump
{
    public  interface IGroundingChecker
    {
        float RayToGroundDistance { get; set; }
        Vector3 RayToGroundPosition { get; set; }
        LayerMask GroundLayer { get; set; }
        Color RayColor { get; set; }

        bool IsGroundingByRaycast(Vector3 objectPosition);
    }
}
