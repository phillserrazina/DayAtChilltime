using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    [SerializeField] private InputField nameInputField = null;
    private Button button;

    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { OnClick(); } );
    }

    private void Update() {
        button.interactable = (nameInputField.text.Length > 0);
    }

    private void OnClick() {
        PlayerData playerData = SaveManager.GetPlayerData(nameInputField.text);
        SaveManager.currentData = playerData;
    }
}
