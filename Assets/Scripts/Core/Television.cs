using System.Collections;
using UnityEngine;

public class Television : Device
{
    private Coroutine _updateProcessCoroutine;
    private readonly WaitForSeconds Dalay = new(0.4f);

    private float _charge;

    protected override void Charge(bool value)
    {
        if (value)
        {
            _charge = 0;
            _animator.SetInteger("Random", Random.Range(0, 5));
            _animator.SetFloat("Full", _charge);
            _animator.Play("Full");

            if (_updateProcessCoroutine != null)
            {
                StopCoroutine(_updateProcessCoroutine);
                _updateProcessCoroutine = null;
            }
            _updateProcessCoroutine = StartCoroutine(ChargeProcessCoroutine());
        }
        else
        {
            _animator.Play("Empty");
            IsComplated = false;
        }
    }

    private IEnumerator ChargeProcessCoroutine()
    {
        yield return Dalay;
        Game.Audio.TelevisionIsOn();

        while (_charge <= 1)
        {
            _charge += Time.deltaTime;
            _animator.SetFloat("Full", _charge);
            yield return null;
        }

        IsComplated = true;
    }
}