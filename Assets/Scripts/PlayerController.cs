using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D cuerpoRigido;
    public float velocidad = 5f; // speed
    public float fuerzaSalto = 10f; //jumpForse
    private bool estaEnSuelo; //isGrounded 

    public Animator animator;
    public bool mirandoDerecha = true;

    //private SaludJugador salud;



    void Start()
    {
        cuerpoRigido = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //salud = GetComponent<SaludJugador>();
    }

    void Update()
    {

        //MOVIMIENTO HORIZONTAL
        float movimiento = Input.GetAxis("Horizontal");
        cuerpoRigido.linearVelocity = new Vector2(movimiento * velocidad, cuerpoRigido.linearVelocityY);

        //SALTO
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo)
        {


            cuerpoRigido.linearVelocityY = fuerzaSalto;
            estaEnSuelo = false;

        }
        //CALCULA VELOCIDAD PARA TRANSICION 
        float velocidadAnimacion = Mathf.Abs(movimiento);
        animator.SetFloat("Speed", velocidadAnimacion);


        //VOLTEAR SPRITE O PERSONAJE --> se voltea si se mueve el lado contrario para que se ejecute movimiento debe ser mayor o menor a 0.
        if (movimiento > 0 && !mirandoDerecha)
        {
            Voltear();
        }
        else if (movimiento < 0 && mirandoDerecha)
        {
            Voltear();
        }



    }
    //CUANDO EL PERSONAJE TOCAR OTRO OBJETO
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            estaEnSuelo = true;
        }
    }


    //CUANDO EL PERSONAJE DEJA DE TOCAR OTRO OBJETO
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            estaEnSuelo = false;
        }
    }

    // ---------- VOLTEAR ----------
    // Invierte hacia dónde mira el personaje
    void Voltear()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }
}

