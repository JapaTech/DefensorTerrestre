using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controla o funcionamento do Score
public class ScoreManager : MonoBehaviour
{
    //Vari�vel que mostra o score
    [SerializeField] TMP_Text score_Txt;
    int score = 0;
    //Vari�vel que mostra o record
    [SerializeField] TMP_Text record_Txt;
    int record = 0;

    //Constroi uma struct com os dados do score
    ScoreData data = new ScoreData();

    private void Awake() 
    {
        //Score inicial � 0
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

    //Iscri��o de eventos
    private void OnEnable() 
    {
        //Evento disparado quando o inimigo morrer
        InimigoControlador.InimigoMorreu += AumentaScore;    
    }

    //Desinscri��o do evento
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
    //Constro� uma struct para guardar as informa��es o record
    public ScoreRecord RecoredAtual = new ScoreRecord();

    //Fun��o que salva o record
    public void SalvaRecord(int _score)
    {
        RecoredAtual.record = _score;
        SaveManager.saveAtual.saveScoreData = RecoredAtual;
        SaveManager.Save();
    }

    //Fun��o que da load no record
    public int LoadRecord()
    {
        //Se n�o tiver arquivos
        if (SaveManager.Load() == false)
            //Salva o record como 0
            SalvaRecord(0);

        SaveManager.Load();
        //Atribui o valor do record para a vari�vel
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