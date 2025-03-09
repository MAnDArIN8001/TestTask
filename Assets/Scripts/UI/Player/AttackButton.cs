using UI.Effects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Player
{
    public class AttackButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [FormerlySerializedAs("_fadeEffect")] [Space, SerializeField] private ScalingEffect scalingEffect;

        public void Show()
        {
            scalingEffect.Play(Vector3.one);
        }

        public void Hide()
        {
            scalingEffect.Play(Vector3.zero);
        }

        public void AddListener(UnityAction action) => _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action) => _button.onClick.RemoveListener(action);
    }
}