using System;

[Serializable]
public class Wallet
{
    public event Action OnAddMoney;
    public event Action OnSpendMoney;

    public int Money
    {
        get => YG.YandexGame.savesData.Money;
        set
        {
            YG.YandexGame.savesData.Money = value;          
            YG.YandexGame.SaveProgress();
        }
    }

    public bool Spend(int value)
    {
        if(value <= Money)
        {
            Money -= value;
            OnSpendMoney?.Invoke();
            return true;
        }

        return false;
    }

    public void Add(int value)
    {
        Money += value;
        OnAddMoney?.Invoke();
    }
}