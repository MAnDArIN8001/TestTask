using System.Collections;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [SerializeField] private float _timeToRegenerateField;

    [SerializeField] private Field[] _fields;

    private Generator _generator;

    private void Awake()
    {
        _generator = GetComponent<Generator>();
    }

    private void OnEnable()
    {
        foreach (var field in _fields)
        {
            field.OnCorrectCombination += HandleWin;
        }
    }

    private void OnDisable()
    {
        foreach (var field in _fields)
        {
            field.OnCorrectCombination -= HandleWin;
        }
    }

    private void HandleWin()
    {
        StartCoroutine(RegenrateFieldOnTime());
    }

    private IEnumerator RegenrateFieldOnTime()
    {
        yield return new WaitForSeconds(_timeToRegenerateField);

        _generator.GenerateField();
    }
}
