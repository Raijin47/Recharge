using UnityEngine;

public class Soket : Base
{
    [SerializeField] private Adapter _adapter;
    public bool IsUse;
    public Plug Plug;

    public override bool IsCharging 
    {
        get => _isCharging;
        set 
        {
            _isCharging = value;
            if(Plug != null)
                Plug.IsCharging = value;
        }
    }

    public void Connect(Plug plug)
    {
        Game.Audio.OnPlugInSocket();
        Game.Particle.OnPlugInSocket(transform.position);
        if (_adapter.Plug != plug)
        {
            Plug = plug;
            Plug.IsCharging = IsCharging;
        }
    }

    public void Disconnect()
    {
        Plug = null;
    }
}