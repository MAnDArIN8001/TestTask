using UnityEngine;
using TMPro;
using Zenject;

public class CommunicationTextController : MonoBehaviour
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
        _playerPicker.OnFindPickableObject += EnablePickUpText;
        _playerPicker.OnLostPickableObject += DisableText;
        _playerPicker.OnExitPoint += DisableText;
        _playerPicker.OnEnterPoint += HandlePointEnterance;
    }

    private void OnDisable()
    {
        _playerPicker.OnFindPickableObject -= EnablePickUpText;
        _playerPicker.OnLostPickableObject -= DisableText;
        _playerPicker.OnExitPoint -= DisableText;
        _playerPicker.OnEnterPoint -= HandlePointEnterance;
    }

    private void HandlePointEnterance(bool isEmpty)
    {
        if (isEmpty)
        {
            EnablePutUpText();
        }
        else
        {
            EnablePickUpText();
        }
    }

    private void EnablePickUpText()
    {
        _text.text = "Press \"F\" to pick up";
        _text.enabled = true;
    }

    private void EnablePutUpText()
    {
        _text.text = "Press \"F\" to put up";
        _text.enabled = true;
    }

    private void DisableText()
    {
        _text.enabled = false;
    }
}
