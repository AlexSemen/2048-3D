using System;
using UnityEngine;

[Serializable]
public class AddCoinsData
{
    [field: SerializeField] private AddCoinsType _type;
    [field: SerializeField] private string _name;
    [field: SerializeField] private int _addCoins;

    public AddCoinsType Type => _type;
    public string Name => _name;
    public int AddCoins => _addCoins;
}
