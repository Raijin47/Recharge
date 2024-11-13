using System.Collections;
using UnityEngine;

public class MiniCar : Base
{
    [SerializeField] private Transform _carTransform;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    [SerializeField] private float _speed;

    private bool _isStart;
    private Vector3 _target;
    private Coroutine _updateProcessCoroutine;

    public override bool IsCharging 
    { 
        get => _isCharging;
        set 
        {
            ReleaseCoroutine();
            if (value) _updateProcessCoroutine = StartCoroutine(UpdateProcess());
        }
    }

    private void Start() => _target = _startPoint.localPosition;

    private IEnumerator UpdateProcess()
    {
        while(true)
        {
            while(_carTransform.transform.localPosition != _target)
            {
                _carTransform.localPosition = Vector3.MoveTowards(_carTransform.localPosition, _target, _speed * Time.deltaTime);
                yield return null;
            }

            _target = _isStart ? _startPoint.localPosition : _endPoint.localPosition;
            _isStart = !_isStart;
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