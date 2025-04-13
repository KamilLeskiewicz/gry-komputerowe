using UnityEngine;

public class FleeScript : MonoBehaviour
{
    // Referencja do obiektu gracza. Mo�esz przypisa� j� r�cznie w Inspectorze lub wyszuka� automatycznie.
    public Transform player;

    // Odleg�o��, przy kt�rej obiekt zacznie ucieka�.
    public float fleeDistance = 5.0f;

    // Pr�dko�� ucieczki.
    public float speed = 3.0f;

    // Update jest wywo�ywane co klatk�.
    void Update()
    {
        // Je�li referencja do gracza nie zosta�a ustawiona, spr�buj j� znale�� za pomoc� tagu "Player".
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            return; // Je�li nadal nie znaleziono gracza, opuszczamy Update.
        }

        // Obliczanie dystansu pomi�dzy aktualnym obiektem a graczem.
        float distance = Vector3.Distance(transform.position, player.position);

        // Je�li gracz znajduje si� w pobli�u (mniej ni� fleeDistance), obiekt ucieka.
        if (distance < fleeDistance)
        {
            // Obliczanie kierunku ucieczki - od wektora mi�dzy gracza a obiektem.
            Vector3 fleeDirection = (transform.position - player.position).normalized;

            // Aktualizacja pozycji obiektu. Korzystamy z Time.deltaTime, aby ruch by� p�ynny i niezale�ny od liczby klatek na sekund�.
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
