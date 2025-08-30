using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickCounter : MonoBehaviour
{
    /* <----Collapse & Expand as needed
CHALLENGE DESCRIPTION
    Create a tiny click-counter survival game. 
    A UI bar slowly deflates; the only way to keep it from hitting zero is to click a button that refills the bar by a small amount. 
    Display an optional click count. 
    When the bar reaches zero, the game is effectively over (you can stop input or show a simple “Game Over” message).

CHALLENGE GOALS
    1. Build a minimal “keep-alive” clicker using Unity UI.
    2. Make a progress bar that continuously decreases over time using Time.deltaTime.
    3. Increase the bar by a fixed amount when a UI Button is clicked.
    4. Expose difficulty settings (drain rate, click gain) in the Inspector.
    */

    [Header("UI")]
    [SerializeField] private Slider _progressBar;
    [SerializeField] private Button _clickButton;
    [SerializeField] private TextMeshProUGUI _clickCountText;
    [SerializeField] private GameObject _gameOverUI;

    [Space(10)]
    [SerializeField] private float _maxProgressBarValue;
    [SerializeField] private float _currentProgressBarValue;
    [SerializeField] private float _drainRate = 20f;
    [SerializeField] private float _refillAmount = 1f;
    [SerializeField] private int _clickCount = 0;
    [SerializeField] private bool _gameOver = false;
    
    private void Start()
    {
        _maxProgressBarValue = _progressBar.maxValue;
        _currentProgressBarValue = _maxProgressBarValue;

        SetGameOverUI();
    }

    private void Update()
    {
        if (!_gameOver)
        {
            DrainProgressBar();
        }
    }

    private void DrainProgressBar()
    {
        _progressBar.value -= _drainRate * Time.deltaTime;
        _currentProgressBarValue = _progressBar.value;

        if (_currentProgressBarValue == 0)
        {
            _gameOver = true;
            SetGameOverUI();
        }
    }

    public void OnHeartClick()
    {
        _progressBar.value += _refillAmount;
        _currentProgressBarValue = _progressBar.value;

        _clickCount++;

        UpdateClickCountText();
    }

    public void RestartGame()
    {
        Debug.Log("Restart Button Clicked");
        _gameOver = false;
        _progressBar.value = _maxProgressBarValue;
        _currentProgressBarValue = _progressBar.value;
        _clickCount = 0;
        UpdateClickCountText();
        SetGameOverUI();
    }

    private void UpdateClickCountText()
    {
        _clickCountText.text = _clickCount.ToString();
    }

    private void SetGameOverUI()
    {
        _clickButton.interactable = !_gameOver;
        _gameOverUI.SetActive(_gameOver);
    }

}