using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class DifficultyManager : MonoBehaviour
{
    [Header("References")]
    public EnemySpawner spawner;
    public TMP_Dropdown difficultyDropdown;
    private bool isInitialized = false;

    private void Start()
    {
        InitializeDropdown();
    }

    private void Update()
    {
        if ((isInitialized) && (spawner != null) && (difficultyDropdown != null))
        {
            if (difficultyDropdown.value != (int)spawner.currentDifficulty)
            {
                difficultyDropdown.SetValueWithoutNotify((int)spawner.currentDifficulty);
            }

            bool isInteractable = (!spawner.isSpawning && Time.timeScale > 0f);
            if (difficultyDropdown.interactable != isInteractable)
            {
                difficultyDropdown.interactable = isInteractable;
            }
        }
    }

    private void InitializeDropdown()
    {
        if (spawner == null)
        {
            return;
        }
        if (difficultyDropdown == null)
        {
            return;
        }

        difficultyDropdown.ClearOptions();
        string[] difficultyNames = Enum.GetNames(typeof(GameDifficulty));
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        foreach (string name in difficultyNames)
        {
            options.Add(new TMP_Dropdown.OptionData(name));
        }

        difficultyDropdown.AddOptions(options);
        difficultyDropdown.value = (int)spawner.currentDifficulty;
        difficultyDropdown.RefreshShownValue();
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChanged);
        isInitialized = true;
    }

    public void OnDifficultyChanged(int newDifficultyIndex)
    {
        if (!isInitialized)
        {
            return;
        }
        if (spawner != null)
        {
            if (Enum.IsDefined(typeof(GameDifficulty), newDifficultyIndex))
            {
                spawner.currentDifficulty = (GameDifficulty)newDifficultyIndex;
            }
        }
    }
}