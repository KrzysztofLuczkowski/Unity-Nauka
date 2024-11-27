using System.Collections; 
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Dodajemy, aby za³adowaæ scenê

public class VictoryUI : MonoBehaviour
{
    public TMP_Text victoryText; // Zmieniamy na TMP_Text

    void Start()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false); // Na pocz¹tku tekst jest ukryty
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
            victoryText.gameObject.SetActive(true); // W³¹czamy tekst
            victoryText.text = "Wygra³eœ!"; // Ustawiamy tekst na "Wygra³eœ!"
        }

        // Uruchamiamy coroutine, która czeka 1 sekundê i potem resetuje scenê
        StartCoroutine(WaitAndRestart());
    }

    public void ShowNotEnoughCarrotsText()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true); // W³¹czamy tekst
            victoryText.text = "Brakuje marchewek!"; // Ustawiamy tekst na "Brakuje marchewek!"
        }
    }

    // Coroutine, która czeka 1 sekundê i potem resetuje poziom
    private IEnumerator WaitAndRestart()
    {
        // Czekamy 1 sekundê
        yield return new WaitForSeconds(1f);

        // Resetujemy scenê (prze³adowanie tej samej sceny)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
