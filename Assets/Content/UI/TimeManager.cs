using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timeText; 
    [SerializeField] private TextMeshProUGUI dateText; 

    [Header("Speed Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button normalSpeedButton; // x1
    [SerializeField] private Button doubleSpeedButton; // x2
    [SerializeField] private Button quadSpeedButton;   // x4

    [Header("Button Highlight Colors")]
    [SerializeField] private Color activeColor = Color.white;                 
    [SerializeField] private Color inactiveColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("Base Time Settings")]
    [SerializeField] private float baseGameSpeed = 60f; 

    
    private float currentSpeedMultiplier = 1f;

    
    private DateTime currentInGameTime = new DateTime(2026, 8, 15, 12, 00, 0);
    public static event Action<DateTime> OnNewInGameDay;
    public DateTime CurrentDate => currentInGameTime;
    private void Start() // Initialize button listeners and update the UI at the start of the game
    {
        
        if (pauseButton) pauseButton.onClick.AddListener(() => SetGameSpeed(0f));
        if (normalSpeedButton) normalSpeedButton.onClick.AddListener(() => SetGameSpeed(1f));
        if (doubleSpeedButton) doubleSpeedButton.onClick.AddListener(() => SetGameSpeed(2f));
        if (quadSpeedButton) quadSpeedButton.onClick.AddListener(() => SetGameSpeed(4f));

        UpdateButtonVisuals();
        UpdateUI();
    }

    private void Update()
    {
        
        if (currentSpeedMultiplier > 0f)
        {
            AdvanceTime();
            UpdateUI();
        }
    }

    private void AdvanceTime() // Advance the in-game time based on the current speed multiplier and base game speed
    {
        int previousDay = currentInGameTime.Day;

        
        float effectiveSpeed = baseGameSpeed * currentSpeedMultiplier;
        currentInGameTime = currentInGameTime.AddSeconds(Time.deltaTime * effectiveSpeed);

        if (currentInGameTime.Day != previousDay)
        {
            OnNewInGameDay?.Invoke(currentInGameTime);
        }
    }

    
    public void SetGameSpeed(float multiplier) // Set the game speed multiplier and update button visuals
    {
        currentSpeedMultiplier = multiplier;

        UpdateButtonVisuals();
    }

    private void UpdateUI() // Update the time and date display
    {
        if (timeText != null)
            timeText.text = currentInGameTime.ToString("HH:mm tt");

        if (dateText != null)
            dateText.text = currentInGameTime.ToString("dd/MM/yyyy");
    }

    private void UpdateButtonVisuals() // Update the button colors based on the current speed multiplier
    {
        SetButtonColor(pauseButton, currentSpeedMultiplier == 0f);
        SetButtonColor(normalSpeedButton, currentSpeedMultiplier == 1f);
        SetButtonColor(doubleSpeedButton, currentSpeedMultiplier == 2f);
        SetButtonColor(quadSpeedButton, currentSpeedMultiplier == 4f);
    }

    private void SetButtonColor(Button btn, bool isActive)
    {
        if (btn == null) return;

        // Ensure the button remains clickable
        btn.interactable = true;

        // Set image color to active (light) or inactive (dark)
        Image btnImage = btn.GetComponent<Image>();
        if (btnImage != null)
        {
            btnImage.color = isActive ? activeColor : inactiveColor;
        }
    }
}
