using UnityEngine;

public class PressureButton : Base
{
    [SerializeField] private Base _target;
    public Base Target { get => _target; set => _target = value; }

    public override bool IsCharging
    {
        get => _isCharging;
        set
        {
            _isCharging = value;
            _target.IsCharging = _isCharging;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsCharging = true;
    }

    private void OnTriggerExit(Collider other)
    {
        IsCharging = false;
    }
}