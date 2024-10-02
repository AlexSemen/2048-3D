using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionObject", menuName = "Data/ExplosionObject")]
public class ExplosionObject : ScriptableObject
{
    [SerializeField] private ExplosionType _type;
    [SerializeField] private int _prise;
    
    public ExplosionType Type => _type;
    public int Price => _prise;
}
