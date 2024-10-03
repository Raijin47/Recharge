using UnityEngine;

public abstract class Base : MonoBehaviour
{
    protected bool _isCharging;
    public abstract bool IsCharging { get; set; }
}