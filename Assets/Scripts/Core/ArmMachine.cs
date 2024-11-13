using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMachine : Base
{
    [SerializeField] private Animator _animator;


    public override bool IsCharging
    {
        get => _isCharging;
        set
        {
            _isCharging = value;

            if (value)
            {
                _animator.Play("Start");
            }
            else
            {
                _animator.Play("end");
            }
        }
    }
}