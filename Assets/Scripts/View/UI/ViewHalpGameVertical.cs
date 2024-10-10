using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI
{
    public class ViewHalpGameVertical : MonoBehaviour
    {
        [SerializeField] private Button _left;
        [SerializeField] private Button _right;
        [SerializeField] private List<GameObject> _panels;

        private const int DefaultIndex = 0;

        private int _index;

        private void OnEnable()
        {
            EnablByIndex(DefaultIndex);
        }

        private void OnDisable()
        {
            _panels[_index].SetActive(false);
        }

        public void OnClickLeft()
        {
            EnablByIndex(_index - 1);
        }

        public void OnClickRight()
        {
            EnablByIndex(_index + 1);
        }

        private void EnablByIndex(int index)
        {
            _panels[_index].SetActive(false);
            _panels[index].SetActive(true);

            _index = index;

            CheckDisplayingButtons();
        }

        private void CheckDisplayingButtons()
        {
            _left.interactable = _index > 0;
            _right.interactable = _index < _panels.Count - 1;
        }
    }
}
