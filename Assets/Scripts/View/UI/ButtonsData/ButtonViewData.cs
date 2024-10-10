using System;
using UnityEngine;

namespace View.UI.ButtonsData
{
    [Serializable]
    public class ButtonViewData
    {
        [field: SerializeField] private ButtonType _type;
        [field: SerializeField] private Sprite[] _sprites;

        public ButtonType Type => _type;
        public Sprite[] Sprites => _sprites;
    }
}
