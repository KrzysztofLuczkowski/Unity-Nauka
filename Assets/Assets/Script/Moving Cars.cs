using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float speed = 5f;            // Pr�dko�� auta
    public bool moveForward = true;    // Czy auto porusza si� w g�r� osi Z (do przodu)
    public float respawnZ = 23f;       // Pozycja, w kt�rej auto pojawia si� ponownie (pocz�tek drogi)
    public float destroyZ = -24f;      // Pozycja, w kt�rej auto znika (koniec drogi)

    private Vector3 startPosition;     // Pozycja startowa auta

    void Start()
    {
        startPosition = transform.position; // Zapami�tujemy pocz�tkow� pozycj�
    }

    void Update()
    {
        // Ustal kierunek ruchu
        float direction = moveForward ? 1 : -1;

        // Porusz auto w wybranym kierunku na osi Z
        transform.Translate(Vector3.forward * direction * speed * Time.deltaTime);

        // Sprawd�, czy auto opu�ci�o drog�
        if (moveForward && transform.position.z > respawnZ)
        {
            // Auto wraca na pocz�tek po dolnej stronie
            transform.position = new Vector3(startPosition.x, startPosition.y, destroyZ);
        }
        else if (!moveForward && transform.position.z < destroyZ)
        {
            // Auto wraca na pocz�tek po g�rnej stronie
            transform.position = new Vector3(startPosition.x, startPosition.y, respawnZ);
        }
    }
}

