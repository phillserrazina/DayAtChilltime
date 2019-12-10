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
}
