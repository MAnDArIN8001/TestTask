using UnityEngine;

[RequireComponent(typeof(Field))]
public class FieldView : MonoBehaviour
{
    [SerializeField] private GameObject _winEffect;

    [SerializeField] private Transform _effectSpawnPosition;

    private Field _field;

    private void Awake()
    {
        _field = GetComponent<Field>();
    }

    private void OnEnable()
    {
        _field.OnCorrectCombination += HandleCorrectResult;
    }

    private void OnDisable()
    {
        _field.OnCorrectCombination -= HandleCorrectResult;
    }

    private void HandleCorrectResult()
    {
        Instantiate(_winEffect, _effectSpawnPosition.position, Quaternion.identity);
    }
}
