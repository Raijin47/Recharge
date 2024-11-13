using System.Collections;
using UnityEngine;

public class Pusher : Base
{
    [SerializeField] private Transform _pushTransfrom;
    [SerializeField] private Transform _helperTransform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector3 _helperOffset;

    [Space(10)]
    [SerializeField] private float _speed;

    private Coroutine _updateProcessCoroutine;
    private Vector3 _target;
    private Vector3 _helperTarget;

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

    private IEnumerator UpdateProcessCoroutine(bool isPush)
    {
        _target = isPush ? _offset : Vector3.zero;
        _helperTarget = isPush ? _helperOffset : Vector3.zero;

        while (_pushTransfrom.localPosition != _target)
        {
            _pushTransfrom.localPosition = Vector3.MoveTowards(_pushTransfrom.localPosition, _target, Time.deltaTime * _speed);
            _helperTransform.localPosition = Vector3.MoveTowards(_helperTransform.localPosition, _helperTarget, Time.deltaTime * _speed);
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