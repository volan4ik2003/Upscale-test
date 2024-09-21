using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private KeyCollector _keyCollector;
    private CharacterMove _characterMove;
    public TextMeshProUGUI KeysScore, TimerText;
    private bool isPaused = false;
    private int KeysCount = 0;
    private bool IsWin = false;

    public Transform pausePanel;
    public Transform victoryPanel;
    public Transform defeatPanel;
    public float animationDuration = 0.5f;

    private Vector2 hiddenPosition;
    private Vector3 visiblePosition = Vector3.zero;

    private float timeElapsed = 0f;

    public void OnEnable()
    {
        SetupButtonListeners(pausePanel);
        SetupButtonListeners(victoryPanel);
        SetupButtonListeners(defeatPanel);
    }

    public void OnDisable()
    {
        RemoveButtonListeners(pausePanel);
        RemoveButtonListeners(victoryPanel);
        RemoveButtonListeners(defeatPanel);
    }

    void Start()
    {
        _keyCollector = FindAnyObjectByType<KeyCollector>();
        _characterMove = FindAnyObjectByType<CharacterMove>();

        hiddenPosition = new Vector3(Screen.width, 0, 0);

        SetPanelsHidden();
    }

    private void Update()
    {
        // Check is Esc pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !IsWin)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        timeElapsed += Time.deltaTime;
        UpdateTimerText();
    }

    private void SetPanelsHidden()
    {
        pausePanel.localPosition = hiddenPosition;
        victoryPanel.localPosition = hiddenPosition;
        defeatPanel.localPosition = hiddenPosition;
    }

    private void SetupButtonListeners(Transform panel)
    {
        Button[] buttons = panel.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button.name));
        }
    }

    private void RemoveButtonListeners(Transform panel)
    {
        Button[] buttons = panel.GetComponentsInChildren<Button>();
        foreach (var button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void OnButtonClicked(string buttonName)
    {
        switch (buttonName)
        {
            case "ResumeButton":
                ResumeGame();
                break;
            case "ExitButton":
                ExitGame();
                break;
            case "RestartButton":
                RetryGame();
                break;
        }
    }

    // Pause game
    public void PauseGame()
    {
        isPaused = true;

        _characterMove.enabled = false;

        pausePanel.DOLocalMove(visiblePosition, animationDuration).SetEase(Ease.OutBounce);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Resume game
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        _characterMove.enabled = true;

        pausePanel.DOLocalMove(hiddenPosition, animationDuration).SetEase(Ease.InOutCubic);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowWinPanel()
    {
        IsWin = true;
        victoryPanel.DOLocalMove(visiblePosition, animationDuration).SetEase(Ease.OutBounce);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowLosePanel()
    {
        defeatPanel.DOLocalMove(visiblePosition, animationDuration).SetEase(Ease.OutBounce);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void UpdateScore()
    {
        KeysCount++;
        KeysScore.text = KeysCount + "/" + _keyCollector.KeyNeedToExit;
    }

    public void RetryGame()
    {
        foreach (GameObject obj in FindObjectsOfType<GameObject>(true))
        {
            if (obj.transform.parent == null) 
            {
                Destroy(obj); // Remove objects from DontDestroyOnLoad
            }
        }

        SceneManager.LoadScene("Initial");
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);
        TimerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
