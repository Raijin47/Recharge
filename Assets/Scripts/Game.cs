using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PinData _pinData;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private LevelHandler _level;

    private readonly GameAction _action = new();
    private readonly ApplicationStatusDetector _task = new();

    public static Wallet Wallet;
    public static PinData Pin;
    public static GameAction Action;
    public static ApplicationStatusDetector Task;

    private void Awake()
    {
        Action = _action;
        Task = _task;
        Pin = _pinData;
        Wallet = _wallet;
    }

    private void Start()
    {
        _level.Init();
    }



    public void Restart() => _action.SendRestart();
    public void SetPin(int id) => _pinData.SetPin(id);


}