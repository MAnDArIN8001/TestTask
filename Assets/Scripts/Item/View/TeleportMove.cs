using UnityEngine;
using DG.Tweening;

namespace Item.View
{
    public class TeleportMove : MonoBehaviour
    {
        [Header("Animation params")]
        [SerializeField] private float _scalingTime;

        [Space, SerializeField] private Ease _scalingEase;

        [Header("Root")] 
        [SerializeField] private Transform _root;

        private Vector3 _defaultScale;

        private Tween _scalingTween;

        private void OnDestroy()
        {
            if (_scalingTween is not null && _scalingTween.active)
            {
                _scalingTween.Kill();
            }
        }

        public void TeleportToPoint(Vector3 point)
        {
            _defaultScale = _root.localScale;

            _scalingTween = _root.DOScale(Vector3.zero, _scalingTime)
                .SetEase(_scalingEase)
                .OnComplete(() => EndTeleportation(point));
        }

        public void BreakTeleportation()
        {
            if (_scalingTween is not null && _scalingTween.active)
            {
                _scalingTween.Kill();
            }
        }

        private void EndTeleportation(Vector3 point)
        {
            _root.position = point;
            _scalingTween = _root.DOScale(_defaultScale, _scalingTime).SetEase(_scalingEase);
        }
    }
}