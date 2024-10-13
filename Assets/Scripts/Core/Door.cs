using System.Collections;
using UnityEngine;

public class Door : Base
{
    [SerializeField] private Transform[] _doors;
    [SerializeField] private Vector3[] _closePosition;
    [SerializeField] private Vector3[] _openPosition;

    [Space(10)]
    [SerializeField] private float _speed;

    private Coroutine _updateProcessCoroutine;
    private Vector3 _leftTarget;
    private Vector3 _rightTarget;

    public override bool IsCharging 
    {
        get => _isCharging;
        set 
        { 
            _isCharging = value;

            ReleaseCoroutine();
            _updateProcessCoroutine = StartCoroutine(UpdateProcessCoroutine(value));
        }
    }

    private IEnumerator UpdateProcessCoroutine(bool IsOpen)
    {
        _leftTarget = IsOpen ? _openPosition[0] : _closePosition[0];
        _rightTarget = IsOpen ? _openPosition[1] : _closePosition[1];

        while (_doors[0].localPosition != _leftTarget && _doors[1].localPosition != _rightTarget)
        {
            _doors[0].localPosition = Vector3.MoveTowards(_doors[0].localPosition, _leftTarget, Time.deltaTime * _speed);
            _doors[1].localPosition = Vector3.MoveTowards(_doors[1].localPosition, _rightTarget, Time.deltaTime * _speed);
            yield return null;
        }
    }

    private void ReleaseCoroutine()
    {
        if (_updateProcessCoroutine != null)
        {
            StopCoroutine(_updateProcessCoroutine);
            _updateProcessCoroutine = null;
        }
    }
}