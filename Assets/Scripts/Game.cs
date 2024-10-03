using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance; 

    private readonly GameAction _action = new();

    public static GameAction Action;

    private void Awake()
    {
        Instance = this;
        Action = _action;
    }

    public void Restart() => _action.SendRestart();
}