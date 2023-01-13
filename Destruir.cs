using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Desativa o que interagir com esse colisor
public class Destruir : MonoBehaviour
{
    //Quando entrar em colisão
    private void OnTriggerEnter2D(Collider2D other)
    {   
        //Desativa o objeto que colidiu
        other.gameObject.SetActive(false);
    }
}
