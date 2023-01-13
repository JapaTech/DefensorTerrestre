using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Controla as ações que o inimigo faz
public class InimigoControlador : MonoBehaviour
{
    //Variável do Rigibody
    Rigidbody2D rb;
    //Velocidade que o inimigo se move (mexer apenas no editor no Y o inimigo se mover para baixo)
    [SerializeField] Vector2 velocidade;
    //Ponto onde o tiro do sai
    [SerializeField] Transform pontoTiro;
    //Prefab da bala que o inimigo spawna
    [SerializeField] MoveBullet bala;
    //De quanto em quanto tempo o inimigo atira
    [SerializeField] float tempoAtirar;

    //Ação disparada ao inimigo morrer
    public static Action<InimigoControlador> InimigoMorreu;

    // Start is called before the first frame update
    void Start()
    {
        //Acessar o rigibody do inimigo 
        rb = GetComponent<Rigidbody2D>();
        //Faz o inimigo atirar em um intervalo
        InvokeRepeating("Atirar", 0.1f, tempoAtirar);
    }

    void FixedUpdate()
    {
        //Move o inimigo para baixo;
        rb.velocity = velocidade;
    }

    //Função para spawna a bala do inimigo
    void Atirar()
    {
        Instantiate(bala, pontoTiro.position, transform.rotation);
    }

    //Verifica o que o inimigo colidiu
    private void OnCollisionEnter2D(Collision2D other)
    {
        //Se o inimigo colidir com o "Tiro"
        if (other.gameObject.CompareTag("Tiro"))
        {
            //Chama a função Morreu
            Morreu();
            //Detrói "other" (quem colidiu)
            Destroy(other.gameObject);
        }
    }

    //Função quando a vida do inimigo chega a 0 ou menos
    private void Morreu()
    {
        //Invoca a ação dizendo que este inimigo morreu
        InimigoMorreu?.Invoke(this);
        //Destrói esse objeto;]
        Destroy(gameObject);
    }
}
