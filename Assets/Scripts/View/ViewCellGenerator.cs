using UnityEngine;

[RequireComponent(typeof(CellViewConfigProvider))]
public class ViewCellGenerator : MonoBehaviour
{
    [SerializeField] private ViewCell _prefabViewCell;
    
    private const int _cellEdge = 4;
    private const float positionOffset = 1.5f;

    private CellViewConfigProvider _cellViewConfigProvider;
    private Transform _spawnPoint;
    private Transform _transform;

    private void Awake()
    {
        _cellViewConfigProvider = GetComponent<CellViewConfigProvider>();
        _transform = transform;
        _spawnPoint = new GameObject("SpawnPoint").transform;
        _spawnPoint.SetParent(_transform);
    }

    public ViewCell[,] Init(Transform transformViewFace, bool isCollider = false)
    {
        _spawnPoint.position = transformViewFace.position;
        _spawnPoint.rotation = transformViewFace.rotation;
        _spawnPoint.SetParent(transformViewFace);

        ViewCell[,] viewCells = new ViewCell[_cellEdge, _cellEdge];

        _spawnPoint.localPosition = new Vector3(-positionOffset, positionOffset, 0);

        for (int i = 0; i < _cellEdge; i++)
        {
            for (int j = 0; j < _cellEdge; j++)
            {
                viewCells[i, j] = Instantiate(_prefabViewCell, _spawnPoint.position, transformViewFace.rotation);
                viewCells[i, j].transform.SetParent(transformViewFace);
                viewCells[i, j].Init(_cellViewConfigProvider);

                _spawnPoint.localPosition += new Vector3(transformViewFace.localScale.x, 0, 0);
            }

            _spawnPoint.localPosition = new Vector3(-positionOffset, _spawnPoint.localPosition.y - transformViewFace.localScale.y, 0);
        }

        _spawnPoint.SetParent(_transform);

        if (isCollider)
        {
            foreach(ViewCell viewCell in viewCells)
            {
                viewCell.gameObject.AddComponent<BoxCollider>();
            }
        }

        return viewCells;
    }
}
