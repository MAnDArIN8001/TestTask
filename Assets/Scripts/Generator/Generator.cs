using System;
using System.Linq;
using UnityEngine;

public abstract class Generator : MonoBehaviour
{
    [SerializeField] protected GameObject _squarePrefab;

    [SerializeField] protected Point[] _cells;

    protected Point[] _winCombination;

    public Point[] WinCombination 
    {
        get
        {
            Point[] temp = new Point[_winCombination.Length];
            Array.Copy(_winCombination, temp, _winCombination.Length);

            return temp;
        }
    }

    public abstract void GenerateField();
}
