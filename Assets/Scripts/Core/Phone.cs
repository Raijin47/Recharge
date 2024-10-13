using System.Collections;
using UnityEngine;

public class Phone : Device
{
    private Coroutine _updateProcessCoroutine;

    private float _charge;

    protected override void Charge(bool value)
    {
        ReleaseCoroutine();
        if (value) _updateProcessCoroutine = StartCoroutine(ChargeProcessCoroutine());
        else _updateProcessCoroutine = StartCoroutine(DischargeProcessCoroutine());
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

        Game.Audio.OnPhoneIsCharged();
        IsComplated = true;
    }

    private IEnumerator DischargeProcessCoroutine()
    {
        while(_charge > 0)
        {
            _charge -= Time.deltaTime;
            _animator.SetFloat("Full", _charge);
            yield return null;
        }
        IsComplated = false;
        _animator.Play("Empty");
    }

    private void ReleaseCoroutine()
    {
        if(_updateProcessCoroutine != null)
        {
            StopCoroutine(_updateProcessCoroutine);
            _updateProcessCoroutine = null;
        }
    }
}