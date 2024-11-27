using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Pr�dko�� poruszania si�
    public float stepSize = 1f;   // Odleg�o�� na jedno "oczko"
    public float jumpHeight = 5f; // Wysoko�� skoku
    public float jumpSpeed = 10f; // Pr�dko�� skoku
    public float jumpCooldown = 1f; // Czas oczekiwania pomi�dzy skokami

    private Rigidbody rb;  // Rigidbody gracza
    private Vector3 targetPosition; // Docelowa pozycja gracza
    private bool canJump = true;  // Flaga, kt�ra kontroluje czas pomi�dzy skokami

    private bool isDead = false;  // Flaga, kt�ra m�wi, czy gracz zgin��

    // Granice ruchu gracza
    public float minX = -10f;
    public float maxX = 70f;
    public float minZ = -30f;
    public float maxZ = 30f;

    void Start()
    {
        targetPosition = transform.position; // Pocz�tkowa pozycja gracza
        rb = GetComponent<Rigidbody>();  // Pobierz Rigidbody
        transform.rotation = Quaternion.Euler(0, 90, 0);  // Ustawienie pocz�tkowej rotacji
    }

    void Update()
    {
        if (isDead)
            return;  // Je�li gracz zgin��, nie wykonuj dalszego kodu

        // Obs�uguje ruch w czterech kierunkach
        if (canJump)
        {
            if (Input.GetKey(KeyCode.W)) // Przesuni�cie w prawo (X+)
            {
                MoveAndJump(Vector3.right, 90f); // Obr�t o 90� w prawo
            }
            if (Input.GetKey(KeyCode.S)) // Przesuni�cie w lewo (X-)
            {
                MoveAndJump(Vector3.left, 270f); // Obr�t o 270� w lewo
            }
            if (Input.GetKey(KeyCode.A)) // Przesuni�cie w g�r� (Z+)
            {
                MoveAndJump(Vector3.forward, 0f); // Obr�t w osi Z+
            }
            if (Input.GetKey(KeyCode.D)) // Przesuni�cie w d� (Z-)
            {
                MoveAndJump(Vector3.back, 180f); // Obr�t o 180� w osi Z-
            }
        }

        // Ograniczenie pozycji docelowej do wyznaczonych granic
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Poruszanie si� w kierunku docelowej pozycji
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Funkcja do skoku i przesuni�cia
    void MoveAndJump(Vector3 direction, float rotationAngle)
    {
        // Skok
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * jumpHeight, rb.velocity.z);

        // Przesuni�cie o jedno "oczko" w wybranym kierunku
        targetPosition += direction * stepSize;

        // Zablokowanie kolejnego skoku przez okre�lony czas
        StartCoroutine(JumpCooldown());

        // Obracamy gracza w zale�no�ci od kierunku
        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }

    // Funkcja odpowiadaj�ca za cooldown skoku
    IEnumerator JumpCooldown()
    {
        canJump = false;  // Zablokuj mo�liwo�� skoku
        yield return new WaitForSeconds(jumpCooldown);  // Czekaj na okre�lony czas
        canJump = true;  // Zezw�l na kolejny skok
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))  // Je�li dotkniemy samochodu
        {
            StartCoroutine(HandleCarCollision());  // Obs�uguje kolizj�
        }
    }

    // Obs�uguje przewr�cenie kurczaka po kolizji
    IEnumerator HandleCarCollision()
    {
        isDead = true;  // Ustawiamy flag�, �e gracz zgin��

        // Ustawiamy ragdoll w pozycji przewr�conej
        rb.isKinematic = false;  // Zmieniamy na fizyk�, aby ragdoll dzia�a�
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse); // Mo�emy doda� jak�� si��, �eby przewr�ci�

        // Czekamy 1 sekund� zanim zresetujemy scen�
        yield return new WaitForSeconds(1f);

        // Resetujemy ca�� scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
