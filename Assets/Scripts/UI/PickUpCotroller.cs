using UnityEngine;
using TMPro;
using Zenject;

public class PickUpCotroller : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private PlayerPicker _playerPicker;

    [Inject]
    private void Initialize(PlayerPicker playerPicker)
    {
        _playerPicker = playerPicker;
    }

    private void Start()
    {
        _text.enabled = false;
    }

    private void OnEnable()
    {
        _playerPicker.OnFindPickableObject += EnableText;
        _playerPicker.OnLostPickableObject += DisableText;
    }

    private void OnDisable()
    {
        _playerPicker.OnFindPickableObject -= EnableText;
        _playerPicker.OnLostPickableObject -= DisableText;
    }

    private void EnableText()
    {
        _text.enabled = true;
    }

    private void DisableText()
    {
        _text.enabled = false;
    }
}
