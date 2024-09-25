using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonViewDatasList", menuName = "Data/ButtonViewDatasList")]
public class ButtonlViewDatasList : ScriptableObject
{
    [SerializeField] private List<ButtonViewData> _buttons;

    public IReadOnlyList<ButtonViewData> Buttons => _buttons;
}
