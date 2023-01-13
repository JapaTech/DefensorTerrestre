using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controla o funcionamento do Score
public class ScoreManager : MonoBehaviour
{
    //Variável que mostra o score
    [SerializeField] TMP_Text score_Txt;
    int score = 0;
    //Variável que mostra o record
    [SerializeField] TMP_Text record_Txt;
    int record = 0;

    //Constroi uma struct com os dados do score
    ScoreData data = new ScoreData();

    private void Awake() 
    {
        //Score inicial é 0
        score = 0;
        //Carrega o record
        record = data.LoadRecord();
    }

    public void Start()
    {
        //Atualiza
        AtualizaScore();
        //Muda o texto do record para o salvo
        record_Txt.text = "Recorde: " + record.ToString(); 
    }

    //Iscrição de eventos
    private void OnEnable() 
    {
        //Evento disparado quando o inimigo morrer
        InimigoControlador.InimigoMorreu += AumentaScore;    
    }

    //Desinscrição do evento
    private void OnDisable() 
    {   
        InimigoControlador.InimigoMorreu -= AumentaScore;    
    }

    //Aumenta score sempre que o inimigo morrer
    private void AumentaScore(InimigoControlador inimigo)
    {
        score += 5;
        AtualizaScore();
    }

    //Altera o texto do score do jogador na tela
    private void AtualizaScore()
    {
        score_Txt.text = "Pontos: " + score.ToString();
        SalvarRecord();
    }

    //Salva os pontos do jogador como record, se ele fizer mais pontos que o record anterior;
    public void SalvarRecord()
    {
        if(data.RecoredAtual.record < score)
        {
            data.SalvaRecord(score);
            record_Txt.text = "Recorde: " + data.LoadRecord();
        }
    }
}

//Salvamento dos records
public class ScoreData
{
    //Constroí uma struct para guardar as informações o record
    public ScoreRecord RecoredAtual = new ScoreRecord();

    //Função que salva o record
    public void SalvaRecord(int _score)
    {
        RecoredAtual.record = _score;
        SaveManager.saveAtual.saveScoreData = RecoredAtual;
        SaveManager.Save();
    }

    //Função que da load no record
    public int LoadRecord()
    {
        //Se não tiver arquivos
        if (SaveManager.Load() == false)
            //Salva o record como 0
            SalvaRecord(0);

        SaveManager.Load();
        //Atribui o valor do record para a variável
        RecoredAtual = SaveManager.saveAtual.saveScoreData;
        //Retorna o valor carregado
        return RecoredAtual.record;
    }
}

//Struct do record
[System.Serializable]
public struct ScoreRecord
{
    public int record;
}