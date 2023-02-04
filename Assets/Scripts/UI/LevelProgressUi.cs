using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _nextLevelText;
    [SerializeField] private Image _progressBar;
    [SerializeField] private LevelProgress _levelProgress;

    private void Update() {
        _progressBar.fillAmount = Mathf.Clamp01(_levelProgress.StoneDestroyed / (float) _levelProgress.StoneMax);

        _currentLevelText.text = _levelProgress.CurrentLevel.ToString();
        _nextLevelText.text = (_levelProgress.CurrentLevel + 1).ToString();
    }
}
