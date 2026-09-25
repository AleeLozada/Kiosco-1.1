using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using TMPro;
public class Cliente : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3.0f;
    private Transform destinoActual;
    [Header("Pedido")]
    public List<Producto> pedido = new List<Producto>();
    public TextMeshProUGUI textoPedido; // Texto flotante sobre el cliente (World Space Canvas)
    void Update()
    {
        //El generador le da un punto de fila, se movera hacia el
        if(destinoActual != null)
        {
            float nuevoX = Mathf.MoveTowards(
                transform.position.x,
                destinoActual.position.x,
                velocidad * Time.deltaTime
            );
            transform.position = new Vector3(
                nuevoX,
                transform.position.y,
                transform.position.z
            );
        }
    }

    //Metodo para detener al cliente cuando choque con el mostrador
    public void AsignarNuevoDestino(Transform nuevoPunto)
    {
        destinoActual = nuevoPunto;
    }

    // Genera un pedido aleatorio a partir del catálogo completo del kiosco
    public void GenerarPedido(List<Producto> catalogoCompleto, int minProductos = 1, int maxProductos = 3)
    {
        pedido.Clear();
        int cantidad = UnityEngine.Random.Range(minProductos, maxProductos);
        for (int i = 0; i < cantidad; i++)
        {
            Producto elegido = catalogoCompleto[UnityEngine.Random.Range(0, catalogoCompleto.Count)];
            pedido.Add(elegido);
        }
        MostrarPedidoTexto();
    }

    void MostrarPedidoTexto()
    {
        if (textoPedido == null) return;
        string texto = "Quiero: ";
        for (int i = 0; i < pedido.Count; i++)
        {
            texto += pedido[i].nombre;
            if (i < pedido.Count - 1) texto += ", ";
        }
        textoPedido.text = texto;
    }
    public int ObtenerTotalPedido()
    {
        int total = 0;
        foreach (Producto p in pedido) total += p.precio;
        return total;

    }

    // Este metodo lo llamara la caja registradora al terminar el cobro
    public void DesaparecerCliente()
    {
        // Acá podrian sumar puntos antes de destruirlo
        Debug.Log("Cliente satisfecho se retira.");
        Destroy(gameObject);
    }
}
