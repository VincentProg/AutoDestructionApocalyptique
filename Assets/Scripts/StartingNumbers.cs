using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartingNumbers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _durationBetweenText = 0.5f;
    [SerializeField] string[] _textsToShow;
    [SerializeField] Color _colorLastText = Color.red;
    int _indexText = 0;
    public event Action OnStartingEnded;

    private void Start()
    {
        StartCoroutine(CoroutineNumber());
    }

    IEnumerator CoroutineNumber()
    {
        if (_indexText == _textsToShow.Length - 1)
        {
            _text.color = _colorLastText;
        }
        _text.text = _textsToShow[_indexText];
        _text.alpha = 1;
        _text.DOFade(0, _durationBetweenText).SetEase(Ease.InExpo);
        ++_indexText;
        yield return new WaitForSeconds(_durationBetweenText);
        if (_indexText < _textsToShow.Length)
        {
            StartCoroutine(CoroutineNumber());
        } else
        {
            OnStartingEnded?.Invoke();
        }
    }
}
