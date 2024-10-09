using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _slider.onValueChanged.AddListener((t) => { _text.text = $"{t}%"; });
    }
}