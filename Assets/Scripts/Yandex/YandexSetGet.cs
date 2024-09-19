using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YandexSetGet : MonoBehaviour
{
    [SerializeField] private SavingLoading _savingLoading;

    public event Action<int> ToApplyYandex;

    private const float TimeBetween = 5.1f;
    private const int NullIndex = 0;

    private float _delay = -1;
    private int _indexs = 0;
    private int _currentIndex = 0;
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
                _savingLoading.Load(TimeBetween);
                _isNeedLoad = false;
            }
            else
            {
                ToApplyYandex?.Invoke(_currentIndex++);

                if (_currentIndex >= _indexs)
                {
                    _currentIndex = NullIndex;
                }
            }

            _delay = TimeBetween;
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