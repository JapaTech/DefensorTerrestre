using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controla o jogador
public class PlayerControlador : MonoBehaviour
{
    //Vida inicial do jogador para ser colocada no editor
    [SerializeField] int vidaInicial;
    //Vida interna
    int vida;
    //Texto que exibe a vida do jogador
    [SerializeField] TMP_Text vida_txt;

    //Armazena o RigidBody
    Rigidbody2D rb;
    //Velocidade com que o jogador se move
    [SerializeField] float velocidade;
    //Variável para receber os valores do inputs
    Vector3 inputs;
    //Prefab da bala
    [SerializeField] MoveBullet bala;
    //Posição que a bala sai
    [SerializeField] Transform pontoTiro;
    
    //Verifica o cooldown se o jogador pode atirar
    bool pedeParaAtira;
    //Cooldown do tiro para ser colocada no editor
    [SerializeField] [Range (1, 10)] float shootRateBase;
    //Coodown  interno do tiro
    float shootRate;

    // Start is called before the first frame update
    void Start()
    {
        //Atribuição das variáveis
        shootRate = shootRateBase;
        rb = GetComponent<Rigidbody2D>();
        vida_txt = GameObject.Find("barraDeVida_txt").GetComponent<TMP_Text>();
        vida = vidaInicial;
        AtualizaVida();
    }

    // Update is called once per frame
    void Update()
    {
        //Diminui o shootRate a cada segunda
        shootRate -= Time.deltaTime;

        DetectInputMovimento();
        DetectInputShoot();
    }

    private void FixedUpdate()
    {
        //Move a nave conforme o jogador fornece os inputs
        rb.velocity = inputs * velocidade;
        
        Shoot();
    }

    //Detecta os inputs recebidos atribuidos ao eixo horizontais  (Teclas: A, D, Seta Esquera e Seta Direita)
    private void DetectInputMovimento()
    {
        inputs.x = Input.GetAxis("Horizontal");
    }

    //Detecta se o jogador quer atirar
    private void DetectInputShoot()
    {
        //Se o jogador aperdar W
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Ele pede para atirar
            pedeParaAtira = true;
        }
    }

    //Controla a instância do tiro do jogador
    private void Shoot()
    {
        //Verifica se o tiro ainda está em cooldown, se tiver descarta o pedido de tiro e  sai da função
        if (shootRate > 0)
        {
            pedeParaAtira = false;
            return;
        }

        //Se o tiro não está em cooldow e o jgador pedeParaAtirar
        if (shootRate < 0 && pedeParaAtira)
        {
            //Cria o tiro
            Instantiate(bala, pontoTiro.position, bala.transform.rotation);
            //Coloca o cooldow para o valor iniciar
            shootRate = shootRateBase;
            //Torna falso pedeParaAtirar
            pedeParaAtira = false;
        }
    }

    //Verifica colisão do jogador
    private void OnCollisionEnter2D(Collision2D other)
    {
        /*Se o jogador colidir com outro tiro (O tiro do jogador está em uma layer diferente do jogador, 
         * uma que a física ignora) ou com o inimigo
        */
        if (other.gameObject.CompareTag("Tiro") || other.gameObject.CompareTag("Inimigo"))
        {
            //Diminui a vida do jogador
            vida--;
            //Verifica se o jogador morreu
            if (vida <= 0)
                Morreu();

            AtualizaVida();
            
            //Detroí quem colidiu
            Destroy(other.gameObject);
        }
    }

    //Altera o texto da vida mostrada na UI
    private void AtualizaVida()
    {
        vida_txt.text = "Vida: " + vida.ToString();
    }

    //Função que ativa quando jogador morre (vida é menor ou igual a 0)
    private void Morreu()
    {
        vida = 0;
        //Ativa o painel final
        GameObject.FindObjectOfType<GameStartEnd>().Final();
        Destroy(gameObject);
    }
}
