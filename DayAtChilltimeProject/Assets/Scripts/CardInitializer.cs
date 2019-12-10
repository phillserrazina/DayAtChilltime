using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CardInitializer : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private Sprite[] allCardSprites = null;

    private List<Sprite> usedCards = new List<Sprite>();

    public int neededUniqueCards { get; private set; }
    [SerializeField] private int amountOfPairs = 0;
    public int AmountOfPairs { get { return amountOfPairs; } }

    public Card[] allCards { get; private set; }

    // METHODS

    public void InitializeCards() {
        allCards = FindObjectsOfType<Card>();
        usedCards = GetUsedCards();

        // Convert allCards array to List for easier use
        List<Card> remainingCardsToAssign = allCards.ToList();

        for (int i = 0; i < neededUniqueCards; i++) {
            for (int j = 0; j < AmountOfPairs; j++) {
                // Find a card, assign a sprite to it and then remove it from the list
                int cardPos = Random.Range(0, remainingCardsToAssign.Count-1);
                remainingCardsToAssign[cardPos].cardSprite = usedCards[i];
                remainingCardsToAssign.Remove(remainingCardsToAssign[cardPos]);
            }
        }
    }

    private List<Sprite> GetUsedCards() {
        var answer = new List<Sprite>();
        neededUniqueCards = allCards.Length / AmountOfPairs;
        // List that keeps track of which sprites haven't been used yet
        List<Sprite> canUseSprites = allCardSprites.ToList();

        for (int i = 0; i < neededUniqueCards; i++) {
            int index = Random.Range(0, canUseSprites.Count-1);
            answer.Add(canUseSprites[index]);
            canUseSprites.Remove(canUseSprites[index]);
        }

        return answer;
    }
}
