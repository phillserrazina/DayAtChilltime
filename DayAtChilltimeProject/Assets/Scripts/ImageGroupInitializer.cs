using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Helper class that renders a given image a given number of times
/// at the start of the scene instead of making the developer do it by hand.
/// </summary>
public class ImageGroupInitializer : MonoBehaviour
{
    [SerializeField] private GameObject imageObject = null;
    [SerializeField] private int numberOfObjects = 0;

    private void Awake() {
        for (int i = 0; i < numberOfObjects; i++) {
            Instantiate(imageObject, transform);
        }
    }
}
