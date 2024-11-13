using System.Collections;
using UnityEngine;

public class Rotate : Base
{
    [SerializeField] private Transform _transfrom;
    [SerializeField] private Vector3 _targetRotate;

    private Coroutine _updateProcessCoroutine;

    public override bool IsCharging
    {
        get => _isCharging;
        set
        {
            _isCharging = value;

            ReleaseCoroutine();
            if(value)
            {
                _updateProcessCoroutine = StartCoroutine(UpdateProcessCoroutine());
            }
        }
    }

    private IEnumerator UpdateProcessCoroutine()
    {
        while (true)
        {
            _transfrom.Rotate(_targetRotate);
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