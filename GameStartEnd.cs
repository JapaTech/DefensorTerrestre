using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Liga e desliga objetos necess�rios no come�o e no fim do jogo. Pausa o jogo
public class GameStartEnd : MonoBehaviour
{
    //Musica
    [SerializeField] AudioSource musica;
    //GameObject que pergunta se o jogador quer realmente sair
    [SerializeField] GameObject painelSair;
    //GameObject que exibe as configura��es
    [SerializeField] GameObject painelConfig;
    //GameObject que pergunta se o jogador quer realmente voltar ao menuu
    [SerializeField] GameObject painelVoltarMenu;
    bool pausado;
    
    // Start is called before the first frame update
    void Start()
    {
        //Desativa os gameObjects necess�rios
        painelSair.SetActive(false);
        painelConfig.SetActive(false);
        painelVoltarMenu.SetActive(false);
        //Liga a m�sica
        musica.Play();
        //Torna o Tempo interno 1
        Time.timeScale = 1;
    }

    private void Update()
    {
        VerificaPausa();
    }

    //A��es que acontecem quando o jogo acaba
    public void Final()
    {
        //Tempo interno vai para 0
        Time.timeScale = 0;
        //Para m�sica
        musica.Stop();
        //Ativa a pergunta se o jogador quer sair ou recome�ar
        painelSair.SetActive(true);
    }

    //Recarrega a cena
    public void Recomecar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Volta para o menu inicial
    public void VoltarAoMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Pausa ou tira do pause o jogo, conforme o jogador aperta ESC
    public void VerificaPausa()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;
            ManipulaMenu();
        }
    }

    //Ativa as configura��es conforme se o jogo est� pausado ou n�o
    public void ManipulaMenu()
    {
        //Se tiver pausado
        if (pausado)
        {
            //O tempo � 0
            Time.timeScale = 0;
            //Ativa as configura��es
            painelConfig.SetActive(true);
        }
        else
        {
            //O tempo � 1
            Time.timeScale = 1;
            //Desativa as configura��es
            painelConfig.SetActive(false);
        }
    }

    //Desativa a tela de pause
    public void Ok_btn()
    {
        Time.timeScale = 1;
        painelConfig.SetActive(false);
    }

    //Verifica se o jogador quer voltar ao menu
    public void AtivaVoltarAoMenu()
    {
        painelVoltarMenu.SetActive(true);
    }

    //Desativa gameObject que pergunta se o jogador quer voltar ao menu
    public void DesativaVoltarAoMenu()
    {
        painelVoltarMenu.SetActive(false);
    }
}
