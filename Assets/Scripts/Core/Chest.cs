public class Chest : Device
{
    protected override void Charge(bool value)
    {
        if (IsComplated) return;

        IsComplated = value;

        if (IsComplated)
        {
            _animator.Play("Open");
        }
    }
}