using System;

public class GameAction
{
    public event Action<Plug> OnPlagDown;
    public event Action OnPlagUp;
    public event Action OnRestart;

    public void SendPlagDown(Plug plug) => OnPlagDown?.Invoke(plug);
    public void SendPlagUp() => OnPlagUp?.Invoke();

    public void SendRestart() => OnRestart?.Invoke();
}