using System;
using UnityEngine;

[Serializable]
public class ButtonViewData
{
    [field: SerializeField] private ButtonType _type;
    [field: SerializeField] private Sprite _sprite1;
    [field: SerializeField] private Sprite _sprite2;

    public ButtonType Type => _type;
    public Sprite Sprite1 => _sprite1;
    public Sprite Sprite2 => _sprite2;
}
