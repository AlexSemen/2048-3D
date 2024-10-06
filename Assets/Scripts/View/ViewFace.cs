using UnityEngine;

public class ViewFace : MonoBehaviour
{
    [SerializeField] private AnimationBlockMove _animationBlockMove;
    
    private Face _face;
    private ViewCell[,] _viewCells;

    public Face Face => _face;

    public void Init(ViewCell[,] viewCells, AnimationBlockMove animationBlockMove)
    {
        _viewCells = viewCells;
        _animationBlockMove = animationBlockMove;
    }

    public void SetFace(Face face) 
    {
        _face = face;
    }

    public void DisplayCell()
    {
        if(_face == null) 
            return;

        foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_face.CellEdge - 1)))
        {
            _viewCells[valueIJ.I, valueIJ.J].SetCell(_face.GetCell(valueIJ.I, valueIJ.J));
        }

        //for (int i = 0; i < _face.CellEdge; i++)
        //{
        //    for (int j = 0; j < _face.CellEdge; j++)
        //    {
        //        _viewCells[i, j].SetCell(_face.GetCell(i, j));
        //    }
        //}
    }

    public void MoveBlock(int i, int j, MoveType moveType)
    {
        if (_animationBlockMove == null)
            return;

        _animationBlockMove.Move(_viewCells[i, j].TransformViewBlock, moveType);
    }
}
