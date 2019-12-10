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
        playerName = "Player";
        totalTries = 0;

        cardInitializer = FindObjectOfType<CardInitializer>();
        cardInitializer.InitializeCards();
        amountOfTries = cardInitializer.AmountOfPairs;

        uiManager = FindObjectOfType<UIManager>();
        uiManager.Initialize(this);

        allCorrectCardObjects = FindObjectsOfType<CorrectCardUI>();

        waitingForReset = false; 
    }

    private void Update() {

        if (playerWonTrigger || !gameBoard.activeSelf) return;

        // neededUniqueCards is the amount of unique cards there are on the board.
        // If the pairs found equals that number, the player has won
        playerWon = (numberOfPairsFound == cardInitializer.neededUniqueCards);

        if (playerWon) {
            finalScore = (totalTries * 5) + timeElapsedInSeconds;
            uiManager.TriggerWinnerScreen(finalScore);
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

                int accessIndex = allCorrectCardObjects.Length-numberOfPairsFound-1;
                allCorrectCardObjects[accessIndex].image.sprite = currentGuess[0].cardSprite;
                numberOfPairsFound++;
            }
            else {
                StartCoroutine(ResetCards());
            }

            currentGuess.Clear();
            totalTries++;
        }
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
}
