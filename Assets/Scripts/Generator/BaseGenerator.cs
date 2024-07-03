using UnityEngine;

public class BaseGenerator : Generator
{
    private void Awake()
    {
        _winCombination = new Point[_cells.Length];

        GenerateField();
    }

    public override void GenerateField()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            GenerationObjectType currentType = GetRandomType(GenerationObjectType.Empty, GenerationObjectType.Box);

            if (currentType == GenerationObjectType.Empty)
            {
                _winCombination[i] = null;

                continue;
            }

            GameObject cube = Instantiate(_squarePrefab, _cells[i].transform);

            cube.transform.localPosition = Vector3.zero;

            _winCombination[i] = _cells[i];
        }
    }

    private GenerationObjectType GetRandomType(params GenerationObjectType[] types)
    {
        int randomIndex = Random.Range(0, types.Length);

        return types[randomIndex];
    }
}
