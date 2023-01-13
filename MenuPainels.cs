using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Controla a visibildade dos paineis no menu (inicial) e troca para a cena do jogo
public class MenuPainels : MonoBehaviour
{
    //GameObject do painel de sair
    [SerializeField] GameObject sair_grp;
    //GameObject do painel de créditos
    [SerializeField] GameObject credito_grp;

    //Sai da cena da tela inicial e vai para a tela do jogo
    public void Jogar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    //Liga o gameobject que pergunta para o jogador se ele quer realmente sair
    public void AtivaPainelSair()
    {
        sair_grp.SetActive(true);
    }

    //Desliga o gameobject que pergunta se o jogador quer sair
    public void DesativaPainelSair()
    {
        sair_grp.SetActive(false);
    }

    //Fecha o jogo
    public void SairBtn()
    {
        Application.Quit();
    }

    //Liga o gameobject com os créditos
    public void AtivaPainelCreditos()
    {
        credito_grp.SetActive(true);
    }

    //Desliga o gameobject com os créditos
    public void DesativaPainelCreditos()
    {
        credito_grp.SetActive(false);
    }
}
