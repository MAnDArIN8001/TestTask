using DG.Tweening;
using UnityEngine;

namespace UI.Effects
{
    public class ScalingEffect : MonoBehaviour
    {
        [SerializeField] private float _scalingTime;

        [Space, SerializeField] private Ease _scalingEase;

        [Space, SerializeField] private Transform _scalingTarget;

        private Tween _fadeTween;

        private void OnDestroy()
        {
            if (_fadeTween is not null && _fadeTween.active)
            {
                _fadeTween.Kill();
            }
        }

        public void Play(Vector3 value)
        {
            if (_fadeTween is not null && _fadeTween.active)
            {
                _fadeTween.Kill();
            }

            _fadeTween = _scalingTarget.DOScale(value, _scalingTime).SetEase(_scalingEase);
        }
    }
}