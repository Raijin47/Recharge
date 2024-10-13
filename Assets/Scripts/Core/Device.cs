using UnityEngine;

public abstract class Device : Base
{
    [SerializeField] protected Animator _animator;

    private bool _isComplated;
    public bool IsComplated 
    { 
        get => _isComplated; 
        protected set
        {
            _isComplated = value;
            if(value) 
                if (Game.Task.Check())
                    Game.Action.SendWin();
        }
    }

    protected abstract void Charge(bool value);

    public override bool IsCharging
    {
        get => _isCharging;
        set => Charge(value);
    }

    protected void OnEnable()
    {
        Game.Task.AddDevice(this);
        IsComplated = false;
    }

    protected void OnDisable()
    {
        Game.Task.RemoveDevice(this);
        IsComplated = false;
    }
}