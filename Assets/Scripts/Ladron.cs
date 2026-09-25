using UnityEngine;

public class Ladron : MonoBehaviour
{
    public float velocidad = 3f;
    public Transform jugador;
    public float distanciaDeteccion = 5f;
    public float tiempoVidaSinDetectar = 6f;

    void Start()
    {
        if (jugador == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) jugador = playerObj.transform;
        }
        Destroy(gameObject, tiempoVidaSinDetectar);
    }
    private void Update()
    {
        if (jugador == null) return;
        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia <= distanciaDeteccion)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.position += (Vector3)direccion * velocidad * Time.deltaTime;
            if (direccion.x != 0)
            {
                Vector3 escala = transform.localScale;
                escala.x = Mathf.Abs(escala.x) * Mathf.Sign(direccion.x);
                transform.localScale = escala;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D otro)
    {if (otro.CompareTag("Player"))
        {
            GestorGanancias.Instancia?.Robar();
            Destroy(gameObject);
        }
    }
}
