using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _textMoney;

    private int _rewardMoney;

    private readonly string EnterSpin = "EnterSpin";
    private readonly string Enter = "Enter";
    private readonly string Exit2 = "ExitWin2";
    private readonly string Exit = "ExitWin";



    public void NextStage() => Game.Level.MoveNextStage();

    public void EnterWin()
    {
        _animator.Play(YG.YandexGame.savesData.IsSpin? EnterSpin : Enter);
        _rewardMoney = Random.Range(40, 80);
        _textMoney.text = $"+{_rewardMoney}";
    }

    public void ExitWin()
    {
        _animator.Play(YG.YandexGame.savesData.IsSpin ? Exit2 : Exit);

        YG.YandexGame.savesData.IsSpin = !YG.YandexGame.savesData.IsSpin;
        Game.Wallet.Add(_rewardMoney);
    }
}