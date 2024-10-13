using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private PinData _pinData;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameAudio _audio;
    [SerializeField] private GameParticle _particle;

    [Space(10)]
    [SerializeField] private LevelHandler _level;
    [SerializeField] private Shop _shop;

    private readonly GameAction _action = new();
    private readonly ApplicationStatusDetector _task = new();
    
    public static Wallet Wallet;
    public static PinData Pin;
    public static GameAction Action;
    public static ApplicationStatusDetector Task;
    public static LevelHandler Level;
    public static GameAudio Audio;
    public static GameParticle Particle;

    private void Awake()
    {
        Action = _action;
        Task = _task;
        Pin = _pinData;
        Wallet = _wallet;
        Level = _level;
        Audio = _audio;
        Particle = _particle;
    }

    private void Start()
    {
        _level.Init();
        _shop.Init();
    }



    public void Restart() => _action.SendRestart();
    public void AddMoney(int value) => _wallet.Add(value);
}