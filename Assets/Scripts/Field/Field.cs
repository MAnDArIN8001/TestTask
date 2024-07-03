using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
    public event Action OnCorrectCombination;

    [SerializeField] private Generator _generator;

    [SerializeField] private Point[] _points;

    private Point[] _currentCombination;

    private void Awake()
    {
        _currentCombination = new Point[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            _currentCombination[i] = null;
        }
    }

    private void OnEnable()
    {
        foreach (var point in _points)
        {
            point.OnPointAdded += HandlePointAdded;
            point.OnPointRemoved += HandlePointRemoved;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _points)
        {
            point.OnPointAdded -= HandlePointAdded;
            point.OnPointRemoved -= HandlePointRemoved;
        }
    }

    private void HandlePointAdded(Point point)
    {
        if (_currentCombination.Contains(point))
        {
            return;
        }

        int indexOfPoint = _points.ToList().IndexOf(point);

        _currentCombination[indexOfPoint] = point;

        HandleCombinationResult();
    }

    private void HandlePointRemoved(Point point)
    {
        if (!_currentCombination.Contains(point))
        {
            return;
        }

        int indexOfPoint = _points.ToList().IndexOf(point);

        _currentCombination[indexOfPoint] = null;

        HandleCombinationResult();
    }

    private void HandleCombinationResult()
    {
        bool isCorrectCombination = CheckCombination();

        if (isCorrectCombination)
        {
            OnCorrectCombination?.Invoke();
        }
    }

    private bool CheckCombination()
    {
        bool isCorrectCombination = true;

        Point[] combination = _generator.WinCombination;

        for (int i = 0; i < combination.Length; i++)
        {
            string winPoint = combination[i] is null ? "null" : combination[i].name;
            string currentPoint = _currentCombination[i] is null ? "null" : _currentCombination[i].name;

            Debug.Log(winPoint + " " + currentPoint);
        }

        for (int i = 0; i < combination.Length; i++)
        {
            if ((combination[i] is not null && _currentCombination[i] is null)
                || (combination[i] is null && _currentCombination[i] is not null))
            {
                isCorrectCombination = false;

                break;
            }
        }

        return isCorrectCombination;
    }
}
