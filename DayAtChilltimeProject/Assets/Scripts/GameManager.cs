using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private GameObject gameBoard = null;

    public string playerName { get; private set; }
    public int totalTries { get; private set; }
    public float timeElapsedInSeconds;
    private int amountOfTries;

    private CardInitializer cardInitializer;

    private List<Card> currentGuess = new List<Card>();

    public bool waitingForReset { get; private set; }
    private UIManager uiManager;

    private CorrectCardUI[] allCorrectCardObjects;
    private int numberOfPairsFound = 0;

    private bool playerWon = false;
    private bool playerWonTrigger = false;  // Used so that the game logic stops running once the player has won
    private float finalScore = 0;

    // EXECUTION FUNCTIONS

    private void Start() {
        SaveManager saveManager = FindObjectOfType<SaveManager>();

        playerName = saveManager == null ? "Player Name" : SaveManager.currentData.playerName;
        totalTries = saveManager == null ? 0 : SaveManager.currentData.playerTries;
        timeElapsedInSeconds = saveManager == null ? 0f : SaveManager.currentData.playerTime;

        allCorrectCardObjects = FindObjectsOfType<CorrectCardUI>();

        cardInitializer = FindObjectOfType<CardInitializer>();
        cardInitializer.InitializeCards();
        amountOfTries = cardInitializer.AmountOfPairs;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.Initialize(this);

        waitingForReset = false; 
    }

    private void Update() {

        if (playerWonTrigger || !gameBoard.activeSelf) return;

        // neededUniqueCards is the amount of unique cards there are on the board.
        // If the pairs found equals that number, the player has won
        playerWon = (numberOfPairsFound == cardInitializer.neededUniqueCards);

        if (Input.GetKeyDown(KeyCode.C)) playerWon = true;

        if (playerWon) {
            finalScore = (totalTries * 5) + timeElapsedInSeconds;
            uiManager.TriggerWinnerScreen(finalScore);
            SaveManager.DeletePlayerData(playerName);
            
            playerWonTrigger = true;
            return;
        }

        timeElapsedInSeconds += Time.deltaTime;
        HandleGuessEnd();
    }

    // METHODS

    private void HandleGuessEnd() {
        if (currentGuess.Count >= amountOfTries) {
            if (CheckIfGuessIsCorrect() == true) {
                foreach (var card in currentGuess) {
                    card.Find();
                }

                AddToCorrectCardZone(currentGuess[0].cardSprite);
            }
            else {
                StartCoroutine(ResetCards());
            }

            currentGuess.Clear();
            totalTries++;
        }
    }

    public void AddToCorrectCardZone(Sprite sprite) {
        foreach (var s in allCorrectCardObjects) {
            if (s.image.sprite == sprite)
                return;
        }

        int accessIndex = allCorrectCardObjects.Length-numberOfPairsFound-1;
        allCorrectCardObjects[accessIndex].image.sprite = sprite;
        numberOfPairsFound++;
    }

    private bool CheckIfGuessIsCorrect() {
        Sprite val = currentGuess[0].cardSprite;
        bool goodGuess = true;

        foreach (var card in currentGuess) {
            // If all the cards match, the loop is completely skipped, leaving the goodGuess as true
            if (card.cardSprite == val) continue;
            goodGuess = false;
        }

        return goodGuess;
    }

    private void AddScore(float score) {
        LeaderboardScore lbScore = new LeaderboardScore(playerName, score, timeElapsedInSeconds);
        LeaderboardData lbData = SaveManager.GetLeaderboardData();
        
        lbData.allScores.Add(lbScore);
        lbData.allScores.Sort( 
            delegate(LeaderboardScore x, LeaderboardScore y) { 
                return x.playerScore.CompareTo(y.playerScore); 
            }
        );

        SaveManager.SaveLeaderboardData(lbData);
    }

    private IEnumerator ResetCards() {
        // Add delay here so that the player has time to see the last card
        waitingForReset = true;
        yield return new WaitForSeconds(0.5f);

        foreach (var card in cardInitializer.allCards) {
            card.Reset();
        }

        waitingForReset = false;
    }

    public void AddToGuess(Card c) {
        currentGuess.Add(c);
    }

    private int[] GetCardStates() {
        var answer = new List<int>();

        foreach (var card in cardInitializer.allCards) {
            answer.Add(card.GetState());
        }

        return answer.ToArray();
    }

    public void SaveGame() {
        PlayerData playerData = new PlayerData();

        playerData.playerName = playerName;
        playerData.playerTries = totalTries;
        playerData.playerTime = timeElapsedInSeconds;

        playerData.cardSprites = cardInitializer.UsedCardIndexes.ToArray();
        playerData.cardStates = GetCardStates();

        SaveManager.SavePlayerData(playerData);
    }

    public void PlayAgainButton() {
        PlayerData playerData = new PlayerData();
        playerData.CreateNewData(playerName);
        SaveManager.SavePlayerData(playerData);

        SaveManager.currentData = playerData;

        FindObjectOfType<MenuManager>().GoToScene("GameScene");
    }
}
