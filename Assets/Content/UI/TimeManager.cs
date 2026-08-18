using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI weekCountText;

    [Header("Speed Controls")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button cycleSpeedButton;
    [SerializeField] private TextMeshProUGUI speedButtonText;

    [Header("Button Highlight Colors")]
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("Base Time Settings")]
    [SerializeField] private float baseGameSpeed = 60f;

    [Header("Quota UI")]
    [SerializeField] private Slider quotaProgressBar;
    [SerializeField] private int quotaIntervalDays = 7;

    private readonly float[] speedMultipliers = { 1f, 2f, 4f };
    private int currentSpeedIndex = 0;
    private bool isPaused = false;

    private DateTime startDate; 
    private DateTime currentInGameTime;
    private DateTime lastQuotaResetTime;
    public static event Action<DateTime> OnNewInGameDay;
    public DateTime CurrentDate => currentInGameTime;

    private void Awake()
    {
        // Enforce base start date strictly on initialization
        startDate = new DateTime(2026, 8, 15, 12, 0, 0);
        currentInGameTime = startDate;
        lastQuotaResetTime = startDate;
    }

    private void Start() // Initializes the time manager and sets up UI elements
    {
        lastQuotaResetTime = currentInGameTime; 

        if (pauseButton) pauseButton.onClick.AddListener(TogglePause); 
        if (cycleSpeedButton) cycleSpeedButton.onClick.AddListener(CycleToNextSpeed);

        if (quotaProgressBar != null) 
        {
            quotaProgressBar.minValue = 0f;
            quotaProgressBar.maxValue = 1f;
        }

        UpdateSpeedUI();
        UpdateButtonVisuals();
        UpdateUI();
    }

    private void Update() // Advances in-game time and updates UI
    {
        if (!isPaused)
        {
            AdvanceTime();
            UpdateQuotaProgressBar();
            UpdateUI();
        }
    }

    private void AdvanceTime() // Advances the in-game time based on the current speed and checks for new days
    {
        int previousDay = currentInGameTime.Day;

        float effectiveSpeed = baseGameSpeed * speedMultipliers[currentSpeedIndex];
        currentInGameTime = currentInGameTime.AddSeconds(Time.deltaTime * effectiveSpeed);

        if (currentInGameTime.Day != previousDay)
        {
            OnNewInGameDay?.Invoke(currentInGameTime);
        }
    }

    private void UpdateQuotaProgressBar() // Updates the quota progress bar based on the elapsed time since the last reset
    {
        if (quotaProgressBar == null) return;

        TimeSpan timeElapsed = currentInGameTime - lastQuotaResetTime;

        
        if (timeElapsed.TotalDays >= quotaIntervalDays)
        {
            lastQuotaResetTime = currentInGameTime;
            timeElapsed = TimeSpan.Zero;
        }

        double elapsedSeconds = timeElapsed.TotalSeconds;
        double totalSecondsInCycle = TimeSpan.FromDays(quotaIntervalDays).TotalSeconds;

        quotaProgressBar.value = Mathf.Clamp01((float)(elapsedSeconds / totalSecondsInCycle));
    }
    public void TogglePause() // Toggles the pause state of the game and updates button visuals
    {
        isPaused = !isPaused;
        UpdateButtonVisuals();
    }

    public void CycleToNextSpeed() // Cycles through the speed multipliers and updates the UI 
    {
        
        if (isPaused)
        {
            isPaused = false;
        }

        currentSpeedIndex = (currentSpeedIndex + 1) % speedMultipliers.Length;
        UpdateSpeedUI();
        UpdateButtonVisuals();
    }

    private void UpdateSpeedUI() // Updates the speed button text based on the current speed index
    {
        if (speedButtonText == null) return;

        
        switch (currentSpeedIndex)
        {
            case 0:
                speedButtonText.text = ">";      // Normal Speed (1x)
                break;
            case 1:
                speedButtonText.text = ">>";    // Double Speed (2x)
                break;
            case 2:
                speedButtonText.text = ">>>";   // Quad Speed (4x)
                break;
        }
    }

    private void UpdateUI() 
    {
        if (timeText != null)
            timeText.text = currentInGameTime.ToString("hh:mm");

        if (dateText != null)
            dateText.text = currentInGameTime.ToString("dd/MM/yyyy");

        if (weekCountText != null)
        {
            int currentWeek = ((int)(currentInGameTime - startDate).TotalDays / 7) + 1;
            weekCountText.text = $"Week {currentWeek}";
        }
    }

    private void UpdateButtonVisuals()
    {
        // Pause button is activeColor when paused, inactiveColor (greyed out) when running
        SetButtonColor(pauseButton, isPaused);

        // Speed button is inactiveColor (greyed out) when paused, activeColor when running
        SetButtonColor(cycleSpeedButton, !isPaused);
    }

    private void SetButtonColor(Button btn, bool isActive) 
    {
        if (btn == null) return;
        btn.interactable = true;

        Image btnImage = btn.GetComponent<Image>();
        if (btnImage != null)
        {
            btnImage.color = isActive ? activeColor : inactiveColor;
        }
    }
}
