using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DataList<T> : ScriptableObject
{
    [SerializeField] private List<T> _list;

    public IReadOnlyList<T> List => _list;
}
