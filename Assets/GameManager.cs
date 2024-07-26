using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region EDITOR STUFF
    [Header("UI")]
    public GameObject ShootObject;
    public GameObject ResultsObject;
    public GameObject RestartObject;

    [Header("Buttons")]
    public Button RockBtn;
    public Button PapperBtn;
    public Button ScissorsBtn;
    public Button ShootBtn;
    public Button RestartBtn;
    public Button ExitBtn;

    [Header("Text")]
    public TextMeshProUGUI UpperResults;
    public TextMeshProUGUI LowerResults; 
    #endregion
    #region LOCALDATA
    private GAMESTATE gameState;
    private readonly string TieState = "Its a Tie";
    private readonly string PlayerWins = "Player Wins";
    private readonly string ComputerWins = "Computer Wins";
    private readonly string placeHolder = "(Player) vs (Computer)";
    private readonly string Rock = "Rock";
    private readonly string Papper = "Papper";
    private readonly string Scissors = "Scissors";
    private readonly string invalid = "Invalid"; 
    #endregion

    private void OnEnable()
    {
        RockBtn.onClick.AddListener(RockStateSelected);
        PapperBtn.onClick.AddListener(PapperStateSelected);
        ScissorsBtn.onClick.AddListener(ScissorsStateSelected);
        ShootBtn.onClick.AddListener(ShootStateSelected);
        RestartBtn.onClick.AddListener(ReplayGame);
        ExitBtn.onClick.AddListener(ExitGame);
    }

    private void RockStateSelected()
    {
        gameState = GAMESTATE.ROCK;
        DisableAllActiveBtns();
        SetShootVisible();
    }

    private void PapperStateSelected()
    {
        gameState = GAMESTATE.PAPPER;
        DisableAllActiveBtns();
        SetShootVisible();
    }

    private void ScissorsStateSelected()
    {
        gameState = GAMESTATE.SCISSORS;
        DisableAllActiveBtns();
        SetShootVisible();
    }

    private void DisableAllActiveBtns()
    {
        RockBtn.interactable = false;
        ScissorsBtn.interactable = false;
        PapperBtn.interactable = false;
    }

    private void SetShootVisible()
    {
        ShootObject.SetActive(true);
    }

    private void SetVisibleLowerComponents()
    {
        RestartObject.SetActive(true);
        ResultsObject.SetActive(true);
    }

    private void ShootStateSelected()
    {
        WhoWins((int)gameState, AIMove());
        SetVisibleLowerComponents();
        ShootBtn.interactable = false;
    }

    private void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
       Application.Quit();
    }

    private int AIMove()
    {
        return Random.Range(0, 3);
    }

    private void WhoWins(int player, int computer)
    {
        if (player == computer)
        {
            ResultsDisplay(player, computer);
            LowerResults.text = TieState;
            return;
        }

        if(player == (int)GAMESTATE.ROCK && computer == (int)GAMESTATE.SCISSORS)
        {
            ResultsDisplay(player, computer);
            FinalResultDisplay(PlayerWins);
            return;
        }

        if(player == (int)GAMESTATE.PAPPER && computer == (int)(GAMESTATE.ROCK))
        {
            ResultsDisplay(player, computer);
            FinalResultDisplay(PlayerWins);
            return;
        }

        if(player == (int)(GAMESTATE.SCISSORS) && computer  == (int)(GAMESTATE.PAPPER))
        {
            ResultsDisplay(player, computer);
            FinalResultDisplay(PlayerWins);
            return;
        }

        ResultsDisplay(player, computer);
        FinalResultDisplay(ComputerWins);



    }

    private void ResultsDisplay(int player, int computer)
    {
        UpperResults.text = WhatState(player) + placeHolder + WhatState(computer);
    }

    private void FinalResultDisplay(string winner)
    {
        LowerResults.text = winner;
    }

    private string WhatState(int gameState)
    {
        switch(gameState)
        {
            case 0:
                return Rock;
            case 1:
                return Papper;
            case 2:
                return Scissors;
            default:
                return invalid;
        }
    }

    private void OnDisable()
    {
        RockBtn.onClick.RemoveListener(RockStateSelected);
        PapperBtn.onClick.RemoveListener(PapperStateSelected);
        ScissorsBtn.onClick.RemoveListener(ScissorsStateSelected);
        ShootBtn.onClick.RemoveListener(ShootStateSelected);
        RestartBtn.onClick.RemoveListener(ReplayGame);
        ExitBtn.onClick.RemoveListener(ExitGame);
    }
}


public enum GAMESTATE : int
{
    ROCK = 0,
    PAPPER = 1,
    SCISSORS = 2,
}
