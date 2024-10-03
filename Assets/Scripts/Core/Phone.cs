using System.Collections;
using UnityEngine;

public class Phone : Base
{
    [SerializeField] private Animator _animator;

    private float _charge;
    private Coroutine _chargeProcessCoroutine;

    public override bool IsCharging 
    {
        get => _isCharging;
        set 
        {
            if(value)
            {
                ReleaseCoroutine();
                _chargeProcessCoroutine = StartCoroutine(ChargeProcessCoroutine());
            }
            else
            {
                ReleaseCoroutine();
                _chargeProcessCoroutine = StartCoroutine(DischargeProcessCoroutine());
            }
        }      
    }

    private IEnumerator ChargeProcessCoroutine()
    {
        _animator.Play("Full");
        while(_charge <= 1)
        {
            _charge += Time.deltaTime;
            _animator.SetFloat("Full", _charge);
            yield return null;
        }
    }

    private IEnumerator DischargeProcessCoroutine()
    {
        while(_charge > 0)
        {
            _charge -= Time.deltaTime;
            _animator.SetFloat("Full", _charge);
            yield return null;
        }
        _animator.Play("Empty");
    }

    private void ReleaseCoroutine()
    {
        if(_chargeProcessCoroutine != null)
        {
            StopCoroutine(_chargeProcessCoroutine);
            _chargeProcessCoroutine = null;
        }
    }
}