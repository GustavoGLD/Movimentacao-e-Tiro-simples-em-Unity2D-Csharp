using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //velocidade do player
    public float jumpHeight; //altura do pulo
    public float jumpTime; //tempo para pular e voltar ao chão
    public float fireRate; //tempo mínimo entre um tiro e outro
    public Rigidbody2D bullet; //prefab da munição
    

    protected Rigidbody2D body; //Rigidbody do personagem
    protected Animator animator; //Animações
    protected  float jumpSpeed; //Impulso do pulo (calculado altomaticamente)
    protected float clock; //Contagem do tempo (em segundo)


    void Start()
    {
        //Pegando Componentes
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        //Equação Horária dos Espaços
        //Calculo da gravidade
        body.gravityScale = ((2 * jumpHeight) / Mathf.Pow(jumpTime, 2)) / 9.8f;

        //Equação Horária da Velocidade
        //Calculo da velocidade inicial do pulo
        jumpSpeed = body.gravityScale * jumpTime * 9.8f;
    }

    //Chama as funções
    void Update()
    {
        Move(); Shoot();
    }

    //Função de Correr
    public void Move()
    {
        //Se as teclas "esquerda" ou "direira" forem acionadas
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Adiciona velocidade ao player
            body.velocity = new Vector2(speed * Input.GetAxisRaw("Horizontal"), body.velocity.y);

            //Ativa a animação de Correr (caso tenha uma)
            animator.SetBool("Run", true);

            //Muda a direção do personagem para Direita ou Esquerda dependendo do botão apertado
            if (Input.GetAxisRaw("Horizontal") > 0) { transform.eulerAngles = Vector3.zero;  }
            else { transform.eulerAngles = new Vector3(0, 180, 0);  }
        }
        //Se não
        else
        {
            //Tira a animação de correr (caso tenha uma)
            animator.SetBool("Run", false);

            //Tira a velocidade
            body.velocity = new Vector2(0, body.velocity.y);
        }
    }

    //Função de Atirar
    public void Shoot()
    {
        //Relógio que vai somando o tempo entre cada frame
        clock += Time.deltaTime;
        
        //Se o botão de atirar foi apertado considerando o tempo mínimo entre cada tiro
        if (Input.GetKey(KeyCode.Space) && clock >= fireRate)
        {
            //Ele vai duplicar o prefab da munição na posição do player
            Instantiate(bullet, transform.position, transform.rotation);

            //E zera o relógio
            clock = 0;
        }
    }

    //Quando o player está no chão
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Se o player está no chão e o botãop de pulo foi acionado
        if (Input.GetButton("Jump"))
        {
            //Ativa a animação de pulo (caso tenha uma)
            animator.SetBool("Jump", true);

            //Mantem a velocidade horizontal e põe a velocidade do pulo na vertical
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
        }
    }

    //Quando o player voltar ao chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Se o player não está apertando o botão de pular
        if (!Input.GetButton("Jump"))
        {
            //Desative a animação de pular
            animator.SetBool("Jump", false);
        }
    }
}
