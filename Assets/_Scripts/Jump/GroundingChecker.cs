
using UnityEngine;
using System;

namespace Jump
{
    [Serializable]
    public class GroundingChecker : IGroundingChecker
    {
        [SerializeField, Range(0f, 5f)]
        private float _groundRayDistance;
        [SerializeField]
        private Vector3 _rayToGroundPosition;
        [SerializeField]
        private LayerMask _groundLayer;
        [SerializeField]
        private Color _rayColor;

        public float RayToGroundDistance { get => _groundRayDistance; set => _groundRayDistance = value; }
        public Vector3 RayToGroundPosition { get => _rayToGroundPosition; set => _rayToGroundPosition = value; }
        public LayerMask GroundLayer { get => _groundLayer; set => _groundLayer = value; }
        public Color RayColor { get => _rayColor; set => _rayColor = value; }
        public bool IsGroundingByRaycast(Vector3 objectPosition) => 
            Physics2D.Raycast(objectPosition + RayToGroundPosition, Vector2.down, RayToGroundDistance, GroundLayer);
    }
}
