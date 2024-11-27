using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Prêdkoœæ poruszania siê
    public float stepSize = 1f;   // Odleg³oœæ na jedno "oczko"
    public float jumpHeight = 5f; // Wysokoœæ skoku
    public float jumpSpeed = 10f; // Prêdkoœæ skoku
    public float jumpCooldown = 1f; // Czas oczekiwania pomiêdzy skokami

    private Rigidbody rb;  // Rigidbody gracza
    private Vector3 targetPosition; // Docelowa pozycja gracza
    private bool canJump = true;  // Flaga, która kontroluje czas pomiêdzy skokami

    private bool isDead = false;  // Flaga, która mówi, czy gracz zgin¹³

    // Granice ruchu gracza
    public float minX = -10f;
    public float maxX = 70f;
    public float minZ = -30f;
    public float maxZ = 30f;

    void Start()
    {
        targetPosition = transform.position; // Pocz¹tkowa pozycja gracza
        rb = GetComponent<Rigidbody>();  // Pobierz Rigidbody
        transform.rotation = Quaternion.Euler(0, 90, 0);  // Ustawienie pocz¹tkowej rotacji
    }

    void Update()
    {
        if (isDead)
            return;  // Jeœli gracz zgin¹³, nie wykonuj dalszego kodu

        // Obs³uguje ruch w czterech kierunkach
        if (canJump)
        {
            if (Input.GetKey(KeyCode.W)) // Przesuniêcie w prawo (X+)
            {
                MoveAndJump(Vector3.right, 90f); // Obrót o 90° w prawo
            }
            if (Input.GetKey(KeyCode.S)) // Przesuniêcie w lewo (X-)
            {
                MoveAndJump(Vector3.left, 270f); // Obrót o 270° w lewo
            }
            if (Input.GetKey(KeyCode.A)) // Przesuniêcie w górê (Z+)
            {
                MoveAndJump(Vector3.forward, 0f); // Obrót w osi Z+
            }
            if (Input.GetKey(KeyCode.D)) // Przesuniêcie w dó³ (Z-)
            {
                MoveAndJump(Vector3.back, 180f); // Obrót o 180° w osi Z-
            }
        }

        // Ograniczenie pozycji docelowej do wyznaczonych granic
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);

        // Poruszanie siê w kierunku docelowej pozycji
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    // Funkcja do skoku i przesuniêcia
    void MoveAndJump(Vector3 direction, float rotationAngle)
    {
        // Skok
        rb.velocity = new Vector3(rb.velocity.x, jumpSpeed * jumpHeight, rb.velocity.z);

        // Przesuniêcie o jedno "oczko" w wybranym kierunku
        targetPosition += direction * stepSize;

        // Zablokowanie kolejnego skoku przez okreœlony czas
        StartCoroutine(JumpCooldown());

        // Obracamy gracza w zale¿noœci od kierunku
        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);
    }

    // Funkcja odpowiadaj¹ca za cooldown skoku
    IEnumerator JumpCooldown()
    {
        canJump = false;  // Zablokuj mo¿liwoœæ skoku
        yield return new WaitForSeconds(jumpCooldown);  // Czekaj na okreœlony czas
        canJump = true;  // Zezwól na kolejny skok
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))  // Jeœli dotkniemy samochodu
        {
            StartCoroutine(HandleCarCollision());  // Obs³uguje kolizjê
        }
    }

    // Obs³uguje przewrócenie kurczaka po kolizji
    IEnumerator HandleCarCollision()
    {
        isDead = true;  // Ustawiamy flagê, ¿e gracz zgin¹³

        // Ustawiamy ragdoll w pozycji przewróconej
        rb.isKinematic = false;  // Zmieniamy na fizykê, aby ragdoll dzia³a³
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse); // Mo¿emy dodaæ jak¹œ si³ê, ¿eby przewróciæ

        // Czekamy 1 sekundê zanim zresetujemy scenê
        yield return new WaitForSeconds(1f);

        // Resetujemy ca³¹ scenê
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
