using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Controla o Menu
public class Menu : MonoBehaviour
{
    //Refer�ncias dos elementos de UI
    [SerializeField] AudioMixer mixer;
    [SerializeField] TMP_Dropdown resolucao_DropDown;
    [SerializeField] Toggle telaCheia_toggle;
    [SerializeField] Slider volume_slider;

    //Resolu��es
    Resolution[] resolucoesPermitidas;
    [SerializeField] int[] resLargura;
    [SerializeField] int[] resAltura;

    void Start()
    {
        //Pega o volume armazenado sistema e aplica no slider de volumer
        float volumeArmazenado = SalvaConfiguracoes.LoadVolume();
        volume_slider.value = volumeArmazenado;
        AlteraVolume(volumeArmazenado);

        //Altera o tamanho do array de resolu��o, depois adiciona as resolu��es no dropdown
        resolucoesPermitidas = new Resolution[resLargura.Length];
        PopulaResolucoes();

        //Pega o valor da tela cheia salva no sistema e aciona ele no toggle
        int telaCheia = SalvaConfiguracoes.LoadFullScreen();
        ToggleTelaCheia(telaCheia == 0 ? false : true);

    }

    //Fun��o que controla a telaCheia. Deve ser chamada num toggle
    public void ToggleTelaCheia(bool ligado)
    {
        //O valor recebido no toggle indica se o jogo est� em tela cheia ou n�o
        Screen.fullScreen = ligado;
        //Muda o visual do toggle
        telaCheia_toggle.isOn = ligado;
        //Salva o valor do toggle
        SalvaConfiguracoes.SaveFullScreen(ligado == false ? 0 : 1);
    }

    //Fun��o que adiciona as resolu��es no dropdown
    public void PopulaResolucoes()
    {
        //Para cada resolu��o dentro do array de largura (definido no inspector)
        for (int i = 0; i < resLargura.Length; i++)
        {
            //Ser� adicionada uma altura e largura correspondente (definido no inspector)
            resolucoesPermitidas[i].width = resLargura[i];
            resolucoesPermitidas[i].height = resAltura[i];
            //O refreshRate em 60 sera mantido em 60 para todas
            resolucoesPermitidas[i].refreshRate = 60;
        }

        //Lista para armazenar os nomes das resolu��es
        List<string> nomeResolucoes = new List<string>();

        //Adiciona os valores na lista
        for (int i = 0; i < resolucoesPermitidas.Length; i++)
        {
            nomeResolucoes.Add($"{resolucoesPermitidas[i].width}x{resolucoesPermitidas[i].height}");
        }

        //Limpa as op��es de resolu��o
        resolucao_DropDown.ClearOptions();
        //Adiciona os nomes das resolu��es no dropdown
        resolucao_DropDown.AddOptions(nomeResolucoes);
        //Garante que a Tela n�o mude
        ToggleTelaCheia(Screen.fullScreen);

    }

    //Fun��o que controla a quando o usu�rio troca a resolu��o. Deve ser chamada num dropdown
    public void AtualizaResolucao(int novaResolucao)
    {
        //Altera a resolu��o
        Screen.SetResolution(resolucoesPermitidas[novaResolucao].width, resolucoesPermitidas[novaResolucao].height, Screen.fullScreen, 60);
        //Salva a resoli��o
        SalvaConfiguracoes.SaveResolucao(novaResolucao);
    }

    //Fun��o que controla o volume .Deve ser chamada num slider
    public void AlteraVolume(float novoVolume)
    {
        SalvaConfiguracoes.saveVolume(novoVolume);
        //Converte o valor do mixer
        novoVolume = Mathf.Log10(novoVolume) * 20;
        //Coloca o valor novo no mixer
        mixer.SetFloat("volumeMaster", novoVolume);
    }
}
