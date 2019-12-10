using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavbarTextColorChange : MonoBehaviour
{
    [SerializeField] private Color selectedColor = new Color();
    [SerializeField] private Color deselectedColor = new Color();
    private Text text;
    private Button button;

    private void Start() {
        text = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
    }

    private void Update() {
        text.color = button.interactable ? deselectedColor : selectedColor;
    }
}
