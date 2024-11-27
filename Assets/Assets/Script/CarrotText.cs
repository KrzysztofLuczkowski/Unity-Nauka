using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarrotCounterUI : MonoBehaviour
{
    public TextMeshProUGUI carrotText; // Referencja do tekstu
    public GameManager gameManager; // Referencja do GameManager

    void Update()
    {
        if (gameManager != null)
        {
            carrotText.text = $"Marchewki zebrane: {gameManager.CarrotsCollected} / {gameManager.carrotsNeeded}";
        }
    }
}
