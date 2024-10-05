using System.Collections;
using UnityEngine;

public class Phone : Base
{
    [SerializeField] private Animator _animator;

    public bool IsComplated { get; set; }
    private float _charge;
    private Coroutine _updateProcessCoroutine;

    public override bool IsCharging 
    {
        get => _isCharging;
        set 
        {
            ReleaseCoroutine();
            if (value) _updateProcessCoroutine = StartCoroutine(ChargeProcessCoroutine());
            else _updateProcessCoroutine = StartCoroutine(DischargeProcessCoroutine());
        }      
    }

    private void OnEnable()
    {
        Game.Task.AddDevice(this);
        IsComplated = false;
    }

    private void OnDisable()
    {
        Game.Task.RemoveDevice(this);
        IsComplated = false;
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

        IsComplated = true;
        if (Game.Task.Check()) Game.Action.SendWin(); 
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