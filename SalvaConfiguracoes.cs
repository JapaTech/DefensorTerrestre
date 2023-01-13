using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvaConfiguracoes
{
    //-------------VOLUME-----------
    const string SAVE_VOLUME_MUSICA = "VOLUME DA MUSICA";
    public static void saveVolume(float volume)
    {

        PlayerPrefs.SetFloat(SAVE_VOLUME_MUSICA, volume);
    }

    public static float LoadVolume()
    {
        return PlayerPrefs.GetFloat(SAVE_VOLUME_MUSICA, 1);
    }

    //------------FULLSCREEN---------
    const string SAVE_TELA_CHEIA = "TELA CHEIA";
    public static void SaveFullScreen(int telaCheia)
    {
        PlayerPrefs.SetInt(SAVE_TELA_CHEIA, telaCheia);
    }

    public static int LoadFullScreen()
    {
        return PlayerPrefs.GetInt(SAVE_TELA_CHEIA, 0);
    }


    //--------------- RESOLUÇÃO------------
    const string SAVE_RESOLUCAO = "RESOLUCAO TELA";

    public static void SaveResolucao(int indiceParaSalvar)
    {
        PlayerPrefs.SetInt(SAVE_RESOLUCAO, indiceParaSalvar);
    }

    public static int LoadResolucao(int resolucaoSalva)
    {
        return PlayerPrefs.GetInt(SAVE_RESOLUCAO, resolucaoSalva);
    }
}
