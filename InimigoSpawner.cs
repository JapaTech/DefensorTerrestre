using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spawna inimigos de tempos em tempos
public class InimigoSpawner : MonoBehaviour
{
    //O intervalo em que cada spwan acontece
    [SerializeField] float tempo;
    //O inimigo que será spwanado
    [SerializeField] GameObject inimigo;
    //Ponto inicial que o inimigo pode ser spawnado
    [SerializeField] Transform p1;
    //Ponto final que o inimigo pode ser spawnado. O inimigo será spwanda em uma linha reta entre 'p1' e 'p2'
    [SerializeField] Transform p2;
    
    // Start is called before the first frame update
    void Start()
    {
        //Chama a função que o inimigo spawna em um periódo
        InvokeRepeating("Spawn", 0.2f, tempo);
    }

    //Spawna o inimigo em um local aleatorio entre a position de 'p1' e 'p2'
    private void Spawn()
    {
        Instantiate(inimigo, new Vector3(Random.Range(p1.position.x, p2.position.x), transform.position.y, 0), inimigo.transform.rotation);
    }
}
