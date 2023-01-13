using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Controla a vida que a terra tem
public class VidaTerra : MonoBehaviour
{
    //Vida da Terra
    [SerializeField] int vidaTerra;
    //Texto que exibe a vida da Terra na UI
    [SerializeField] TMP_Text vidaTerra_txt;

    private void Start() {
        AtualizaVida();
    }

    //Verifica a colisão da Terra
    private void OnTriggerEnter2D(Collider2D other) {
        //Se Colidiu com um Inimigo
        if(other.gameObject.CompareTag("Enemy"))
        {
            //Dimiui a vida em 1;
            vidaTerra--;
            //Muda a UI
            AtualizaVida();
            // Verifica se o a Terra está sem vida
            if(vidaTerra <= 0)
                Morreu();

            //Desativa quem colidiu
            other.gameObject.SetActive(false);
        }
    }

    //Muda a exibição da vida conforme dano é recebido
    private void AtualizaVida()
    {
        vidaTerra_txt.text = "Defesa da Terra: " + vidaTerra.ToString();
    }

    //Controla o que acontece quando morre (Vida menor ou igual a zero)
    private void Morreu()
    {
        //Confirma que a vida é 0 para evitar mostrar valor negativo
        vidaTerra = 0;
        AtualizaVida();
        //Ativa o painell final
        GameObject.FindObjectOfType<GameStartEnd>().Final();
    }
}
