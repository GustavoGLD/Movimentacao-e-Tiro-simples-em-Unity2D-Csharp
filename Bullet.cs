using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 12f; //Velocidade da munição
    public float lifeTime = 2f; //Tempo para a munição ser exluída depois de atirada

    private Rigidbody2D body; //Rigidbody da munição
    private GameObject player; //GameObject do player

    void Start()
    {
        //(coloque Tag no player)
        //Pegar componentes
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        //Se o player está voltado para a direita
        if (player.transform.eulerAngles.y == 0)
        {
            //A munição vai para a direita
            body.velocity = new Vector2(speed, 0);
        }

        //Se o player está voltado para a esquerda
        else if (player.transform.eulerAngles.y == 180)
        {
            //A munição vai para esquerda
            body.velocity = new Vector2(-speed, 0);
        }

        //Destrua a munição depois de um tempo que atirada
        Destroy(gameObject, lifeTime);
    }
}
