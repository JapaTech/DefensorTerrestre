using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script que controla o comportamento dos tiros
public class MoverBala : MonoBehaviour
{
    //Rigibody da bala
    Rigidbody2D rb;
    //Velocidade da bala (deve  ser mudado no editor, no eixo Y apenas)
    [SerializeField] Vector2 velocidade;

    // Start is called before the first frame update
    void Start()
    {
        //Atribui o rigibody para a variável
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //Movimenta a bala com base no vetor2
        rb.velocity = velocidade;
    }
}
