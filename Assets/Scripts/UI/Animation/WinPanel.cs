using UnityEngine;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly string EnterSpin = "EnterSpin";
    private readonly string Enter = "Enter";

    public void NextStage() => Game.Level.MoveNextStage();


    public void EnterWin()
    {
        _animator.Play(YG.YandexGame.savesData.IsSpin? EnterSpin : Enter);
        YG.YandexGame.savesData.IsSpin = !YG.YandexGame.savesData.IsSpin;
        YG.YandexGame.SaveProgress();
    }
}