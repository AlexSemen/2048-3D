using System;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionConfigProvider
{
    [SerializeField] private FaceController _faceController;
    
    private IExplosion _pointExplosion = new PointExplosion();
    private IExplosion _areaExplosion = new AreaExplosion();

    public ExplosionConfigProvider(FaceController faceController)
    {
        _faceController = faceController;
    }

    public List<Cell> GetCellTarget(Cell cell, ExplosionType explosionType)
    {
        switch (explosionType)
        {
            case ExplosionType.Point: 
                return _pointExplosion.GetCellTarget(cell, _faceController);

            case ExplosionType.Area:
                return _areaExplosion.GetCellTarget(cell, _faceController);

            default:
                throw new ArgumentException();
        }
    }
}
