using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Line : MonoBehaviour
{
    public VictoryUI victoryUI; // Referencja do VictoryUI

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Sprawdzamy, czy to gracz dotkn¹³ mety
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                if (gm.CarrotsCollected >= gm.carrotsNeeded) // Sprawdzanie liczby marchewek
                {
                    Debug.Log("Wygra³eœ!"); // Tymczasowe debugowanie
                    gm.WinGame(); // Wywo³anie metody wygranej w GameManager
                    victoryUI.ShowVictoryText(); // Pokazanie tekstu wygranej
                }
                else
                {
                    Debug.Log("Brakuje marchewek!"); // Jeœli gracz nie zebra³ wystarczaj¹cej liczby marchewek
                    victoryUI.ShowNotEnoughCarrotsText(); // Pokazanie tekstu o braku marchewek
                }
            }
        }
    }
}
