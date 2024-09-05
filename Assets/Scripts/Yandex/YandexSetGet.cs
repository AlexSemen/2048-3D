using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexSetGet : MonoBehaviour
{
    [SerializeField] private SavingLoading _savingLoading;

    public event Action<int> ToApplyYandex;

    private const float _timeBetween = 5.1f;
    private float _delay = -1;
    private int _indexs = 0;
    private int _currentIndex = 0;
    private int _nullIndex = 0;
    private bool _isNeedLoad = true;

    private void Update()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }

        if (_delay <= 0)
        {
            if (_isNeedLoad)
            {
                _savingLoading.Load(_timeBetween);
                _isNeedLoad = false;
            }
            else
            {
                ToApplyYandex?.Invoke(_currentIndex++);

                if (_currentIndex >= _indexs)
                {
                    _currentIndex = _nullIndex;
                }
            }

            _delay = _timeBetween;
        }
    }

    public int GetIndexToApplyYandex()
    {
        return ++_indexs;
    }

    public void Load()
    {
        _isNeedLoad = true;
    }


}
