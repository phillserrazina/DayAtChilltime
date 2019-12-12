using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public enum CardStates { Checked, Unchecked, Found }

    public Sprite cardSprite;
    public CardStates currentState { get; private set; }
    private Image image;
    private Sprite cardBack;

    private GameManager gameManager;

    [SerializeField] private GameObject glowFX = null;

    private void Start() {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();

        cardBack = image.sprite;
        currentState = CardStates.Unchecked;
    }

    public void OnClick() {
        if (gameManager.waitingForReset) return;
        if (currentState == CardStates.Checked || currentState == CardStates.Found) return;

        image.sprite = cardSprite;
        currentState = CardStates.Checked;
        gameManager.AddToGuess(this);
        glowFX.SetActive(true);
    }

    public void Reset() {
        if (currentState == CardStates.Found) return;

        image.sprite = cardBack;
        currentState = CardStates.Unchecked;
        glowFX.SetActive(false);
    }

    public void Find() {
        currentState = CardStates.Found;
        glowFX.SetActive(false);
    }

    /// <summary>
    /// Utility function used to load the state of the card.
    /// <br>Indexes: 0 = Unchecked; 1 = Checked; 2 = Found.</br>
    /// </summary>
    /// <param name="state"></param>
    public void SetState(int state) {
        switch (state) {
            case 0:
                currentState = CardStates.Unchecked;
                break;
            
            case 1:
                currentState = CardStates.Checked;
                image.sprite = cardSprite;
                gameManager.AddToGuess(this);
                glowFX.SetActive(true);
                break;
            
            case 2:
                currentState = CardStates.Found;
                image.sprite = cardSprite;
                gameManager.AddToCorrectCardZone(cardSprite);
                break;
        }
    }

    /// <summary>
    /// Utility function used to save the state of the card.
    /// <br>Indexes: 0 = Unchecked; 1 = Checked; 2 = Found.</br>
    /// </summary>
    /// <returns></returns>
    public int GetState() {
        switch (currentState) {
            case CardStates.Unchecked: return 0;
            case CardStates.Checked: return 1;
            case CardStates.Found: return 2;
            default:
                Debug.Log("Card::GetState() --- Invalid state! Returned 0 (Unchecked) by default.");
                return 0;
        }
    }
}
