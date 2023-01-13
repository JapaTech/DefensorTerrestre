using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Liga e desliga objetos necessários no começo e no fim do jogo. Pausa o jogo
public class GameStartEnd : MonoBehaviour
{
    //Musica
    [SerializeField] AudioSource musica;
    //GameObject que pergunta se o jogador quer realmente sair
    [SerializeField] GameObject painelSair;
    //GameObject que exibe as configurações
    [SerializeField] GameObject painelConfig;
    //GameObject que pergunta se o jogador quer realmente voltar ao menuu
    [SerializeField] GameObject painelVoltarMenu;
    bool pausado;
    
    // Start is called before the first frame update
    void Start()
    {
        //Desativa os gameObjects necessários
        painelSair.SetActive(false);
        painelConfig.SetActive(false);
        painelVoltarMenu.SetActive(false);
        //Liga a música
        musica.Play();
        //Torna o Tempo interno 1
        Time.timeScale = 1;
    }

    private void Update()
    {
        VerificaPausa();
    }

    //Ações que acontecem quando o jogo acaba
    public void Final()
    {
        //Tempo interno vai para 0
        Time.timeScale = 0;
        //Para música
        musica.Stop();
        //Ativa a pergunta se o jogador quer sair ou recomeçar
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

    //Ativa as configurações conforme se o jogo está pausado ou não
    public void ManipulaMenu()
    {
        //Se tiver pausado
        if (pausado)
        {
            //O tempo é 0
            Time.timeScale = 0;
            //Ativa as configurações
            painelConfig.SetActive(true);
        }
        else
        {
            //O tempo é 1
            Time.timeScale = 1;
            //Desativa as configurações
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
