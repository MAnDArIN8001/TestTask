using System;
using UnityEngine;

namespace Utiles.Raycasters
{
    public class CameraTowardsScreenPointRaycaster : MonoBehaviour
    {
        [SerializeField] private float _raycastLength;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public RaycastHit ThrowRayTowardsScreenPoint(Vector2 screenPoint)
        {
            var ray = _camera.ScreenPointToRay(screenPoint);

            Physics.Raycast(ray, out var hitInfo, _raycastLength);
            
            Debug.DrawRay(ray.origin, ray.direction * _raycastLength, Color.red);

            return hitInfo;
        }
    }
}