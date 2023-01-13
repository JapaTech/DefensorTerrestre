using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Fun��o que salva o jogo em JSON
public static class SalvarControlador
{
    //Constro� o SaveAtual
    public static SalvarDados saveAtual = new SalvarDados();

    //Vari�vel com o nome do arquivo
    const string nomeDoArquivo = "score";
    //Vari�vel com a extens�o do arquivo
    const string extensaoDoArquivo = "json";

    //Fun��o de Salva o jogo
    public static bool Save()
    {
        //Endere�o do arquivo
        string enderecoDoArquivo = $"{Application.persistentDataPath}/{nomeDoArquivo}.{extensaoDoArquivo}";
        //Cria o arquivo em JSON
        string salvaJson = JsonUtility.ToJson(saveAtual, true);
        //Aplica criptografia no script
        salvaJson = Criptografia.Criptografar(salvaJson);
        //Escreve o arquivo em JSON
        File.WriteAllText(enderecoDoArquivo, salvaJson);

        //Retorna verdade se conseguir chegar no final
        return true;
    }

    //Fun��o que carrega o jogo 
    public static bool Load()
    {
        //Endere�o do Arquivo
        string enderecoDoArquivo = $"{Application.persistentDataPath}/{nomeDoArquivo}.{extensaoDoArquivo}";
        //Cria um tempor�rio
        SalvarDados temp = new SalvarDados();

        //Verifica se tem um arquivo para ler
        if (File.Exists(enderecoDoArquivo))
        {
            //Converte o arquivo para string
            string loadJson = File.ReadAllText(enderecoDoArquivo);
            //Descriptografa o arquivo
            loadJson = Criptografia.Descriptografar(loadJson);
            //Coloca os dados no tempor�rio
            temp = JsonUtility.FromJson<SalvarDados>(loadJson);
        }
        else
        {
            //Se n�o tiver retorna falso
            Debug.Log("File n�o existe");
            return false;
        }
        //O save Atual � igual ao tempor�rio
        saveAtual = temp;
        return true;
    }
}

//Criptografia dos arquivos json
public class Criptografia
{
    //Deslocamendo na tabela ASCII
    static int[] deslocamentos = new int[] { 10, 6, -10, 5, 3 };

    //Fun��o que executa a criptografia
    public static string Criptografar(string texto)
    {
        //Cria um array de chars das informa��es recebidas
        char[] caracteres = texto.ToCharArray();

        //Encada elmento desse array
        for (int c = 0; c < caracteres.Length; c++)
        {
            //Faz o deslocamento na Tabela ASCII para criptografar
            caracteres[c] = DeslocaCaractere(caracteres[c], PegaProximoDeslocamento(c));
        }
        //Retorna o texto criptrafado
        return new string(caracteres);
    }

    //Fun��o que l� o texto cifrado e converte paara o normal
    public static string Descriptografar(string cifra)
    {
        //Pega os caracteres da cifra e guarda em uma char
        char[] caracteres = cifra.ToCharArray();

        //Desloca os caracteres da cifra de forma inversa ao da criptogravia
        for (int c = 0; c < caracteres.Length; c++)
        {
            caracteres[c] = DeslocaCaractere(caracteres[c], -PegaProximoDeslocamento(c));
        }

        //Retorna o texto descriptografado
        return new string(caracteres);
    }

    //Fun�ao que pega o pr�ximo deslocamento
    private static int PegaProximoDeslocamento(int indice)
    {
        //Enquando a quantidade indice for menor que o deslocamento
        while (indice >= deslocamentos.Length)
            //Desconta o deslocamento �ndice
            indice -= deslocamentos.Length;

        //Retorna o �nde gerado
        return deslocamentos[indice];
    }

    //Desloca os carcateres na tabela ascci
    private static char DeslocaCaractere(char caractere, int quantidade)
    {
        //Pega o �ltimo indice do array de caracteres
        int indiceFinal = ((int)caractere + quantidade);

        //Se o �ndice final for menor que 0
        while (indiceFinal < 0)
        {
            //Desloca o caractere pelo valor m�ximo
            indiceFinal = indiceFinal + char.MaxValue;
        }
        //Caso contr�rio
        if (indiceFinal > char.MaxValue)
        {
            //Muda o �ndice final
            indiceFinal = (indiceFinal % char.MaxValue);
        }
        //Retorna o char deslocado
        return (char)indiceFinal;
    }

}
