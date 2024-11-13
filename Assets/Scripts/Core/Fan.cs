using System.Collections;
using UnityEngine;

public class Fan : Base
{
    [SerializeField] private Base _blocking;
    [SerializeField] private Transform _fan;
    [SerializeField] private WindCar _car;
    [SerializeField] private Rigidbody[] _papers;

    [SerializeField] private bool _isPush = false;

    [Space(10)]
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

            if (value)
            {
                if (_isPush) _fanProcessCoroutine = StartCoroutine(PushProcess());
                else _fanProcessCoroutine = StartCoroutine(FanProcess());
            }  
        }
    }

    private IEnumerator PushProcess()
    {
        _current = _dalay;

        if (_blocking != null)
            while (!_blocking.IsCharging)
                yield return null;

        while (IsCharging)
        {
            if (_current > 0) _current -= Time.deltaTime;
            else _car.Push();

            _fan.Rotate(_speed * Time.deltaTime * Vector3.back);
            yield return null;
        }
    }

    private IEnumerator FanProcess()
    {
        _current = _dalay;

        if(_blocking != null)        
            while(!_blocking.IsCharging)            
                yield return null;
                   
        while (IsCharging)
        {
            if(_current > 0) _current -= Time.deltaTime;
            else
            {
                foreach (Rigidbody paper in _papers)                
                    paper.AddForce((paper.position - transform.position).normalized * _force);                
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