using System.Collections;
using TMPro;
using UnityEngine;
using Data.CellData;
using Data.CellData.ColorData;
using Main;

namespace View
{
    public class ViewCell : MonoBehaviour
    {
        private readonly float _timeBlowUp = 0.08f;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private Transform _transformViewBlock;

        private CellViewConfigProvider _cellViewConfigProvider;
        private Renderer _rendererViewBlock;
        private Renderer _renderer;
        private Cell _cell;
        private Vector3 _defaultPosition;
        private WaitForSeconds _waitForSecondsBlowUp;

        public Cell Cell => _cell;
        public Transform TransformViewBlock => _transformViewBlock;

        private void Awake()
        {
            _renderer = gameObject.GetComponent<Renderer>();
            _rendererViewBlock = _transformViewBlock.GetComponent<Renderer>();
            _defaultPosition = _transformViewBlock.localPosition;

            _waitForSecondsBlowUp = new WaitForSeconds(_timeBlowUp);
        }

        public void Init(CellViewConfigProvider cellViewConfigProvider)
        {
            _cellViewConfigProvider = cellViewConfigProvider;
        }

        public void SetCell(Cell cell)
        {
            _cell = cell;

            if (cell.IsBoom == false)
            {
                DrawBlock();
            }
            else
            {
                StartCoroutine(BlowUp());
            }
        }

        private void DrawBlock()
        {
            if (_transformViewBlock != null)
                _transformViewBlock.localPosition = _defaultPosition;

            if (_cellViewConfigProvider == null)
                return;

            if (_cell.Block != null)
            {
                _rendererViewBlock.gameObject.SetActive(true);
                _rendererViewBlock.material.color = _cellViewConfigProvider.GetColor((CellType)_cell.Block.Meaning);
                _text.text = _cell.Block.Meaning.ToString();
            }
            else
            {
                _rendererViewBlock.gameObject.SetActive(false);
                _text.text = "";
            }

            if (_cell.IsTargetCell)
            {
                _renderer.material.color = _cellViewConfigProvider.GetColor(CellType.Target);
            }
            else
            {
                _renderer.material.color = _cellViewConfigProvider.GetColor(CellType.NoTarget);
            }
        }

        private IEnumerator BlowUp()
        {
            _rendererViewBlock.material.color = _cellViewConfigProvider.GetColor(CellType.Target);
            yield return _waitForSecondsBlowUp;

            _cell.SetBoom(false);
            _cell.SetBlock(null);

            DrawBlock();
        }
    }
}
