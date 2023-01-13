using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Função que salva o jogo em JSON
public static class SalvarControlador
{
    //Constroí o SaveAtual
    public static SalvarDados saveAtual = new SalvarDados();

    //Variável com o nome do arquivo
    const string nomeDoArquivo = "score";
    //Variável com a extensão do arquivo
    const string extensaoDoArquivo = "json";

    //Função de Salva o jogo
    public static bool Save()
    {
        //Endereço do arquivo
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

    //Função que carrega o jogo 
    public static bool Load()
    {
        //Endereço do Arquivo
        string enderecoDoArquivo = $"{Application.persistentDataPath}/{nomeDoArquivo}.{extensaoDoArquivo}";
        //Cria um temporário
        SalvarDados temp = new SalvarDados();

        //Verifica se tem um arquivo para ler
        if (File.Exists(enderecoDoArquivo))
        {
            //Converte o arquivo para string
            string loadJson = File.ReadAllText(enderecoDoArquivo);
            //Descriptografa o arquivo
            loadJson = Criptografia.Descriptografar(loadJson);
            //Coloca os dados no temporário
            temp = JsonUtility.FromJson<SalvarDados>(loadJson);
        }
        else
        {
            //Se não tiver retorna falso
            Debug.Log("File não existe");
            return false;
        }
        //O save Atual é igual ao temporário
        saveAtual = temp;
        return true;
    }
}

//Criptografia dos arquivos json
public class Criptografia
{
    //Deslocamendo na tabela ASCII
    static int[] deslocamentos = new int[] { 10, 6, -10, 5, 3 };

    //Função que executa a criptografia
    public static string Criptografar(string texto)
    {
        //Cria um array de chars das informações recebidas
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

    //Função que lê o texto cifrado e converte paara o normal
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

    //Funçao que pega o próximo deslocamento
    private static int PegaProximoDeslocamento(int indice)
    {
        //Enquando a quantidade indice for menor que o deslocamento
        while (indice >= deslocamentos.Length)
            //Desconta o deslocamento índice
            indice -= deslocamentos.Length;

        //Retorna o índe gerado
        return deslocamentos[indice];
    }

    //Desloca os carcateres na tabela ascci
    private static char DeslocaCaractere(char caractere, int quantidade)
    {
        //Pega o último indice do array de caracteres
        int indiceFinal = ((int)caractere + quantidade);

        //Se o índice final for menor que 0
        while (indiceFinal < 0)
        {
            //Desloca o caractere pelo valor máximo
            indiceFinal = indiceFinal + char.MaxValue;
        }
        //Caso contrário
        if (indiceFinal > char.MaxValue)
        {
            //Muda o índice final
            indiceFinal = (indiceFinal % char.MaxValue);
        }
        //Retorna o char deslocado
        return (char)indiceFinal;
    }

}
