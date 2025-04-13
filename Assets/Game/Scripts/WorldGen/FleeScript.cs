using UnityEngine;

public class FleeScript : MonoBehaviour
{
    // Referencja do obiektu gracza. Mo¿esz przypisaæ j¹ rêcznie w Inspectorze lub wyszukaæ automatycznie.
    public Transform player;

    // Odleg³oœæ, przy której obiekt zacznie uciekaæ.
    public float fleeDistance = 5.0f;

    // Prêdkoœæ ucieczki.
    public float speed = 3.0f;

    // Update jest wywo³ywane co klatkê.
    void Update()
    {
        // Jeœli referencja do gracza nie zosta³a ustawiona, spróbuj j¹ znaleŸæ za pomoc¹ tagu "Player".
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return; // Jeœli nadal nie znaleziono gracza, opuszczamy Update.
        }

        // Obliczanie dystansu pomiêdzy aktualnym obiektem a graczem.
        float distance = Vector3.Distance(transform.position, player.position);

        // Jeœli gracz znajduje siê w pobli¿u (mniej ni¿ fleeDistance), obiekt ucieka.
        if (distance < fleeDistance)
        {
            // Obliczanie kierunku ucieczki - od wektora miêdzy gracza a obiektem.
            Vector3 fleeDirection = (transform.position - player.position).normalized;

            // Aktualizacja pozycji obiektu. Korzystamy z Time.deltaTime, aby ruch by³ p³ynny i niezale¿ny od liczby klatek na sekundê.
            transform.position += fleeDirection * speed * Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) < fleeDistance)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(fleeDirection * speed, ForceMode.VelocityChange);
            }
        }
    }

}
