using Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explosives
{
    public class ExplosionConfigProvider
    {
        [SerializeField] private FaceController _faceController;

        private readonly IExplosion _pointExplosion = new PointExplosion();
        private readonly IExplosion _areaExplosion = new AreaExplosion();

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
}
