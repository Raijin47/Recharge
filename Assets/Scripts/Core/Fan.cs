using System.Collections;
using UnityEngine;

public class Fan : Base
{
    [SerializeField] private Plug _plug;
    [SerializeField] private Transform _fan;
    [SerializeField] private Rigidbody[] _papers;

    [SerializeField] private float _force;
    [SerializeField] private float _speed;
    [SerializeField] private float _dalay;

    private float _current;

    private Coroutine _fanProcessCoroutine;

    public override bool IsCharging 
    {
        get => _isCharging;
        set 
        {
            _isCharging = value;

            Release();
            if(value) _fanProcessCoroutine = StartCoroutine(FanProcess());
        }
    }

    private IEnumerator FanProcess()
    {
        _current = _dalay;

        while (IsCharging)
        {
            if(_current > 0) _current -= Time.deltaTime;
            else
            {
                foreach (Rigidbody paper in _papers)
                {
                    paper.AddForce((paper.position - transform.position).normalized * _force);
                }
            }

            _fan.Rotate(_speed * Time.deltaTime * Vector3.back);
            yield return null;
        }
    }

    private void Release()
    {
        if (_fanProcessCoroutine != null)
        {
            StopCoroutine(_fanProcessCoroutine);
            _fanProcessCoroutine = null;
        }
    }
}