using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum CardStates { Checked, Unchecked }

    [SerializeField] private Sprite cardSprite;
    [SerializeField] private CardStates currentState;
}
