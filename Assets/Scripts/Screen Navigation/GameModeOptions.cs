using Assets.Scripts.Players.Difficulty.Enums;
using Assets.Scripts.Players.GameModes.Enums;
using Assets.Scripts.Screen_Navigation.StaticClasses;
using Assets.Scripts.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModeOptions : MonoBehaviour
{
    public GameObject __CPUPlayerCountToggles;
    public GameObject __DifficultyToggles;
    private GameSettings __GameSettings;
    public GameObject __GameTypeToggles;
    public GameObject __SingleOrMultipleRoundsToggles;

    public void MainMenu()
    {
        SaveGameSettings.SaveSettings(__GameSettings);
        SceneManager.LoadScene(SceneNames.MAIN_MENU);
    }
    public void MultipleRounds()
    {
        __GameSettings.SelectedRound = RoundMode.MultipleRounds;
    }

    public void SetCPUPlayersToOne()
    {
        __GameSettings.CPUPlayerCount = 1;
    }

    public void SetCPUPlayersToThree()
    {
        __GameSettings.CPUPlayerCount = 3;
    }

    public void SetCPUPlayersToTwo()
    {
        __GameSettings.CPUPlayerCount = 2;
    }

    public void SetEasyDifficulty()
    {
        __GameSettings.SelectedDifficulty = DifficultyLevel.Easy;
    }

    public void SetGameTypeToHPMode()
    {
        __GameSettings.IsHPMode = true;
    }

    public void SetGameTypeToStandard()
    {
        __GameSettings.IsHPMode = false;
    }

    public void SetHardDifficulty()
    {
        __GameSettings.SelectedDifficulty = DifficultyLevel.Hard;
    }

    public void SetInsaneDifficulty()
    {
        __GameSettings.SelectedDifficulty = DifficultyLevel.Insane;
    }

    public void SetNormalDifficulty()
    {
        __GameSettings.SelectedDifficulty = DifficultyLevel.Normal;
    }
    public void SingleRound()
    {
        __GameSettings.SelectedRound = RoundMode.SingleRound;
    }

    private void Start()
    {
        __GameSettings = SaveGameSettings.LoadSettings() ?? new GameSettings();
        __DifficultyToggles.transform.GetChild((int)__GameSettings.SelectedDifficulty).GetComponent<Toggle>().isOn = true;
        __SingleOrMultipleRoundsToggles.transform.GetChild((int)__GameSettings.SelectedRound).GetComponent<Toggle>().isOn = true;
        __CPUPlayerCountToggles.transform.GetChild(__GameSettings.CPUPlayerCount - 1).GetComponent<Toggle>().isOn = true;
        __GameTypeToggles.transform.GetChild(!__GameSettings.IsHPMode ? 0 : 1).GetComponent<Toggle>().isOn = true;
    }

}
