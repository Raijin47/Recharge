using UnityEngine;

public class Blender : Device
{
    [SerializeField] private ParticleSystem _particle;

    protected override void Charge(bool value)
    {
        if (IsComplated) return;

        IsComplated = value;

        if (IsComplated)
        {
            _animator.Play("Full");
        }
    }

    public void PlayParticle() => _particle.Play();
    public void StopParticle() => _particle.Stop();
}