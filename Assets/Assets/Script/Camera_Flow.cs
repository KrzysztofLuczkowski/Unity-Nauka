using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;    // Obiekt gracza
    public Vector3 offset = new Vector3(0, 15, 0); // Pozycja kamery nad graczem
    public float minX = -24f;   // Granica w lewo
    public float maxX = 23f;    // Granica w prawo
    public float minZ = -10f;   // Granica w dó³ (oœ Z)
    public float maxZ = 10f;    // Granica w górê (oœ Z)

    void LateUpdate()
    {
        // Wylicz pozycjê kamery wzglêdem gracza
        Vector3 targetPosition = player.position + offset;

        // Ograniczenie pozycji kamery do wyznaczonych granic
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Aktualizacja pozycji kamery
        transform.position = targetPosition;
    }
}
