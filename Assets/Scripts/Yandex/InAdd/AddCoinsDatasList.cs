using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IAddDatasList", menuName = "Data/AddCoinsDatasList")]
public class AddCoinsDatasList : ScriptableObject
{
    [SerializeField] private List<AddCoinsData> _inAddDatas;
    public IReadOnlyList<AddCoinsData> InAddDatas => _inAddDatas;
}
