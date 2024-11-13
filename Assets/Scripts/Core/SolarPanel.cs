using UnityEngine;

public class SolarPanel : Base
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
}