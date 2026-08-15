using System;
using UnityEngine;
using TMPro;

public class QuotaManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI quotaText; 

    [Header("Quota Settings")]
    [SerializeField] private int startingQuota = 1000;
    [SerializeField] private int weeklyQuotaIncrease = 1000;

    
    private int currentEngagementProgress = 0; // Tracks the current engagement progress


    private DateTime startDate = new DateTime(2026, 8, 15); // The date when the quota tracking starts

    private void OnEnable() 
    {
        TimeManager.OnNewInGameDay += HandleNewDay;
    }

    private void OnDisable() 
    {
        TimeManager.OnNewInGameDay -= HandleNewDay;
    }

    private void Start() // Initializes the quota UI at the start of the game
    {
        
        UpdateQuotaUI(CalculateCurrentTargetQuota(startDate));
    }

    private void HandleNewDay(DateTime currentInGameDate) // Updates the quota UI whenever a new in-game day is triggered
    {
        int targetQuota = CalculateCurrentTargetQuota(currentInGameDate);
        UpdateQuotaUI(targetQuota);
    }

    private int CalculateCurrentTargetQuota(DateTime currentDate) // Calculates the current target quota based on the number of weeks passed since the start date
    {
        int daysPassed = (currentDate.Date - startDate.Date).Days;
        int weeksPassed = daysPassed / 7; 

        return startingQuota + (weeklyQuotaIncrease * weeksPassed);
    }

    private void UpdateQuotaUI(int targetQuota) // Updates the quota UI with the current engagement progress and target quota
    {
        if (quotaText != null)
        {
            quotaText.text = $"Quota: {currentEngagementProgress}/{targetQuota}";
        }
    }

    public void AddEngagementProgress(int amount) // Increases the current engagement progress
    {
        currentEngagementProgress += amount;

        TimeManager timeManager = FindAnyObjectByType<TimeManager>();
        DateTime currentDate = (timeManager != null) ? timeManager.CurrentDate : startDate;

        UpdateQuotaUI(CalculateCurrentTargetQuota(currentDate));
    }
}
