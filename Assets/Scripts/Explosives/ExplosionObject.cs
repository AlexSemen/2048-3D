using UnityEngine;

[CreateAssetMenu(fileName = "ExplosionObject", menuName = "Data/ExplosionObject")]
public class ExplosionObject : ScriptableObject
{
    [SerializeField] private ExplosionType _type;
    [SerializeField] private int _prise;
    [SerializeField] private Sprite _noActivSprite;
    [SerializeField] private Sprite _activSprite;
    
    public ExplosionType Type => _type;
    public int Price => _prise;
    public Sprite NoActivSprite => _noActivSprite;
    public Sprite ActivSprite => _activSprite;
}
