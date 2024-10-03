using UnityEngine;

public class Adapter : Base
{
    [SerializeField] private Soket[] _sokets;
    [SerializeField] private bool _isActive;
    [SerializeField] private Plug _plug;

    public Plug Plug { get => _plug; }

    public override bool IsCharging
    {
        get => _isCharging;
        set
        {
            _isCharging = value;
            for (int i = 0; i < _sokets.Length; i++)
                _sokets[i].IsCharging = _isCharging;
        }                      
    }

    private void Start()
    {
        IsCharging = _isActive;
    }
}