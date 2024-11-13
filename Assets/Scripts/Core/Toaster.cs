using UnityEngine;

public class Toaster : Device
{
    [SerializeField] private ParticleSystem _particle;

    protected override void Charge(bool value)
    {
        if (IsComplated) return;

        IsComplated = value;

        if (IsComplated)
        {
            _animator.Play("Full");
            _particle.Play();
        }
    }
}