using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;            // Prêdkoœæ auta
    public bool moveForward = true;    // Czy auto porusza siê w górê osi Z (do przodu)
    public float respawnZ = 23f;       // Pozycja, w której auto pojawia siê ponownie (pocz¹tek drogi)
    public float destroyZ = -24f;      // Pozycja, w której auto znika (koniec drogi)

    private Vector3 startPosition;     // Pozycja startowa auta

    void Start()
    {
        startPosition = transform.position; // Zapamiêtujemy pocz¹tkow¹ pozycjê
    }

    void Update()
    {
        // Ustal kierunek ruchu
        float direction = moveForward ? 1 : -1;

        // Porusz auto w wybranym kierunku na osi Z
        transform.Translate(Vector3.forward * direction * speed * Time.deltaTime);

        // SprawdŸ, czy auto opuœci³o drogê
        if (moveForward && transform.position.z > respawnZ)
        {
            // Auto wraca na pocz¹tek po dolnej stronie
            transform.position = new Vector3(startPosition.x, startPosition.y, destroyZ);
        }
        else if (!moveForward && transform.position.z < destroyZ)
        {
            // Auto wraca na pocz¹tek po górnej stronie
            transform.position = new Vector3(startPosition.x, startPosition.y, respawnZ);
        }
    }
}

