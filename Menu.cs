using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//Controla o Menu
public class Menu : MonoBehaviour
{
    //Referências dos elementos de UI
    [SerializeField] AudioMixer mixer;
    [SerializeField] TMP_Dropdown resolucao_DropDown;
    [SerializeField] Toggle telaCheia_toggle;
    [SerializeField] Slider volume_slider;

    //Resoluções
    Resolution[] resolucoesPermitidas;
    [SerializeField] int[] resLargura;
    [SerializeField] int[] resAltura;

    void Start()
    {
        //Pega o volume armazenado sistema e aplica no slider de volumer
        float volumeArmazenado = SalvaConfiguracoes.LoadVolume();
        volume_slider.value = volumeArmazenado;
        AlteraVolume(volumeArmazenado);

        //Altera o tamanho do array de resolução, depois adiciona as resoluções no dropdown
        resolucoesPermitidas = new Resolution[resLargura.Length];
        PopulaResolucoes();

        //Pega o valor da tela cheia salva no sistema e aciona ele no toggle
        int telaCheia = SalvaConfiguracoes.LoadFullScreen();
        ToggleTelaCheia(telaCheia == 0 ? false : true);

    }

    //Função que controla a telaCheia. Deve ser chamada num toggle
    public void ToggleTelaCheia(bool ligado)
    {
        //O valor recebido no toggle indica se o jogo está em tela cheia ou não
        Screen.fullScreen = ligado;
        //Muda o visual do toggle
        telaCheia_toggle.isOn = ligado;
        //Salva o valor do toggle
        SalvaConfiguracoes.SaveFullScreen(ligado == false ? 0 : 1);
    }

    //Função que adiciona as resoluções no dropdown
    public void PopulaResolucoes()
    {
        //Para cada resolução dentro do array de largura (definido no inspector)
        for (int i = 0; i < resLargura.Length; i++)
        {
            //Será adicionada uma altura e largura correspondente (definido no inspector)
            resolucoesPermitidas[i].width = resLargura[i];
            resolucoesPermitidas[i].height = resAltura[i];
            //O refreshRate em 60 sera mantido em 60 para todas
            resolucoesPermitidas[i].refreshRate = 60;
        }

        //Lista para armazenar os nomes das resoluções
        List<string> nomeResolucoes = new List<string>();

        //Adiciona os valores na lista
        for (int i = 0; i < resolucoesPermitidas.Length; i++)
        {
            nomeResolucoes.Add($"{resolucoesPermitidas[i].width}x{resolucoesPermitidas[i].height}");
        }

        //Limpa as opções de resolução
        resolucao_DropDown.ClearOptions();
        //Adiciona os nomes das resoluções no dropdown
        resolucao_DropDown.AddOptions(nomeResolucoes);
        //Garante que a Tela não mude
        ToggleTelaCheia(Screen.fullScreen);

    }

    //Função que controla a quando o usuário troca a resolução. Deve ser chamada num dropdown
    public void AtualizaResolucao(int novaResolucao)
    {
        //Altera a resolução
        Screen.SetResolution(resolucoesPermitidas[novaResolucao].width, resolucoesPermitidas[novaResolucao].height, Screen.fullScreen, 60);
        //Salva a resolição
        SalvaConfiguracoes.SaveResolucao(novaResolucao);
    }

    //Função que controla o volume .Deve ser chamada num slider
    public void AlteraVolume(float novoVolume)
    {
        SalvaConfiguracoes.saveVolume(novoVolume);
        //Converte o valor do mixer
        novoVolume = Mathf.Log10(novoVolume) * 20;
        //Coloca o valor novo no mixer
        mixer.SetFloat("volumeMaster", novoVolume);
    }
}
