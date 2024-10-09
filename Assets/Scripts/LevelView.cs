using UnityEngine;
using TMPro;
public class LevelView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject[] _complatedObjects;

    private void Start() => UpdateUI();

    private void OnEnable()
    {
        _levelText.text = $"Level {YG.YandexGame.savesData.Level}";
    }

    public void UpdateUI()
    {
        for(int i = 0; i < _complatedObjects.Length; i++)
        {
            _complatedObjects[i].SetActive(YG.YandexGame.savesData.Stage > i);
        }
    }
}