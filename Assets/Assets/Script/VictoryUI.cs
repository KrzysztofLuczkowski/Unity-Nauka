using System.Collections; 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Dodajemy, aby za�adowa� scen�

public class VictoryUI : MonoBehaviour
{
    public TMP_Text victoryText; // Zmieniamy na TMP_Text

    void Start()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false); // Na pocz�tku tekst jest ukryty
        }
        else
        {
            Debug.LogError("VictoryText not assigned in the Inspector!");
        }
    }

    public void ShowVictoryText()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true); // W��czamy tekst
            victoryText.text = "Wygra�e�!"; // Ustawiamy tekst na "Wygra�e�!"
        }

        // Uruchamiamy coroutine, kt�ra czeka 1 sekund� i potem resetuje scen�
        StartCoroutine(WaitAndRestart());
    }

    public void ShowNotEnoughCarrotsText()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true); // W��czamy tekst
            victoryText.text = "Brakuje marchewek!"; // Ustawiamy tekst na "Brakuje marchewek!"
        }
    }

    // Coroutine, kt�ra czeka 1 sekund� i potem resetuje poziom
    private IEnumerator WaitAndRestart()
    {
        // Czekamy 1 sekund�
        yield return new WaitForSeconds(1f);

        // Resetujemy scen� (prze�adowanie tej samej sceny)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
