using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class IAPajaro : MonoBehaviour
{
    public GameObject enemigo;
    private Vector3 posicionCuandoAtaca = new Vector3(0, 0, 0);
    private bool colisionando = false;
    private bool colisionandoEnemigo = false;

    private bool yaAgarrado = false;

    //path del barrido
    public GameObject path1;
    public GameObject path2;
    public GameObject path3;
    public GameObject path4;
    private int pathObjetivo = 1;//1, 2, 3 o 4
    private bool sentidoPath = true;//sentido horario = true, antihorario = false

    public bool direccion = true;//true para la derecha y false para la izquierda
    public  bool veoEnemigosGlobal = false;
    //public  bool tieneLlaveGlobal = false;
    private Node arbol;
    public bool barriendo = false;
    public bool atacandoBajando = false;
    public bool atacandoSubiendo = false;

    // Start is called before the first frame update
    void Start()
    {
        //primero creamos el arbol de decision
        GetComponent<SpriteRenderer>().flipX = true;
        arbol = crearArbol();

    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        colisionando = true;
        if(col.gameObject.tag == "Player" && yaAgarrado == false && !enemigo.GetComponent<PlayerController>().tieneLlave) 
        {
            yaAgarrado = true;
            colisionandoEnemigo = true;
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemigo.GetComponent<Collider2D>(), true);
        }

        if (col.gameObject.tag.Equals("Player") && enemigo.GetComponent<PlayerController>().tieneLlave)
        {
            Debug.Log("colisiona");
            enemigo.transform.GetChild(1).gameObject.GetComponent<LifesScript>().LoseLife();
            enemigo.transform.GetChild(1).gameObject.GetComponent<LifesScript>().UpdateLivesUI();
            if (enemigo.transform.GetChild(1).gameObject.GetComponent<LifesScript>().livesRemaining == 0)
            {
                SceneManager.LoadScene("MenuIAs");
            }
        }

    }
    void OnCollisionExit2D(Collision2D col)
    {
        colisionando = false;
        /*
        if (col.gameObject.tag == "Player")
            colisionandoEnemigo = false;
        */
    }


    // Update is called once per frame
    void Update()
        {

        actualizarVeoEnemigos(enemigo);

        string accion = arbol.comprobar(enemigo.GetComponent<PlayerController>().tieneLlave, veoEnemigosGlobal);
        if (accion == "vigilar" && barriendo == false && atacandoSubiendo == false && atacandoBajando == false)//tenemos que tener en cuenta que no se encuentra barriendo para no cortar la animación a medias
        {
            vigilar();
        }
        else if (accion == "atacar" && barriendo == false && atacandoSubiendo == false && atacandoBajando == false)
        {
            atacandoBajando = true;
            posicionCuandoAtaca = enemigo.transform.position;
        }
        else if (accion == "barrer" && barriendo == false && atacandoSubiendo == false && atacandoBajando == false)//tenemos que barrer y por tanto le enviamos la señal de que haga el barrido
        {

            barriendo = true;
            // en caso de estar más cerca del punto 1 se dirije a´el, sino, se dirige al 4
            if (true)
                //Math.Abs(transform.position.x - path1.transform.position.x) < Math.Abs(transform.position.x - path4.transform.position.x)
            {
                pathObjetivo = 1;
                sentidoPath = false;
                direccion = true;
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else //se dirige al 4
            {
                pathObjetivo = 4;
                sentidoPath = true;
                direccion = false;
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else if (barriendo == true)
        {
            barrer();
        }
        else if (atacandoSubiendo == true || atacandoBajando == true)
        {
            atacar();
        }
        

    }
    private void actualizarVeoEnemigos(GameObject enemigo)
    {
        if (!(enemigo.transform.position.y < 0 && ((enemigo.transform.position.x > 0.3f && enemigo.transform.position.x < 2.6f) ||
                                          (enemigo.transform.position.x > 7.4f && enemigo.transform.position.x < 12.7) ||
                                            (enemigo.transform.position.x > 18.6f && enemigo.transform.position.x < 30)))) {
            //el enemigo no se encuentra escondido debajo de los techitos

            if (direccion == true)//nos encontramos mirando a la derecha
            {
                if (enemigo.transform.position.x > transform.position.x) //y se encuentra a nuestra derecha
                {
                    veoEnemigosGlobal = true;
                }
                else
                {
                    veoEnemigosGlobal = false;
                }

            } else if (direccion == false)//nos encontramos mirando a la izqda
            {
                if (enemigo.transform.position.x < transform.position.x) //y se encuentra a nuestra izqda
                {
                    veoEnemigosGlobal = true;
                }
                else
                {
                    veoEnemigosGlobal = false;
                }

            }
        }
        else
        {
            veoEnemigosGlobal = false;
        }

    }
    private void barrer()
    {
        if (colisionandoEnemigo) 
        {
            enemigo.transform.position = transform.position;
        }
        switch (pathObjetivo) 
        {
            case 1:
                transform.position = Vector3.MoveTowards(transform.position, path1.transform.position, Time.deltaTime * 40);
                if (transform.position == path1.transform.position)
                {
                    if (sentidoPath == true)
                    {
                        barriendo = false;
                        yaAgarrado = false;
                    }
                    else 
                    {
                        pathObjetivo = 2;
                    } 
                }
                break;
            case 2:
                transform.position = Vector3.MoveTowards(transform.position, path2.transform.position, Time.deltaTime * 40);
                if (transform.position == path2.transform.position)
                {
                    if (sentidoPath == true)
                    {
                        pathObjetivo = 1;
                        colisionandoEnemigo = false;
                        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemigo.GetComponent<Collider2D>(), false);
                    }
                    else
                    {
                        pathObjetivo = 3;
                    }
                }
                break;
            case 3:
                transform.position = Vector3.MoveTowards(transform.position, path3.transform.position, Time.deltaTime * 40);
                if (transform.position == path3.transform.position)
                {
                    if (sentidoPath == true)
                    {
                        pathObjetivo = 2;
                    }
                    else
                    {
                        pathObjetivo = 4;
                        colisionandoEnemigo = false;
                        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemigo.GetComponent<Collider2D>(), false);
                    }
                }
                break;
            case 4:
                transform.position = Vector3.MoveTowards(transform.position, path4.transform.position, Time.deltaTime * 40);
                if (transform.position == path4.transform.position)
                {
                    if (sentidoPath == true)
                    {
                        pathObjetivo = 3;
                    }
                    else
                    {
                        barriendo = false;
                        yaAgarrado = false;
                    }
                }
                break;

        }
       
    }
    private void atacar()
    {
        if (atacandoBajando == true)//se abalanza sobre el enemigo hasta chocar con algo
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionCuandoAtaca, Time.deltaTime * 20);
            //lega a la posicion objetivo o choca con algun objeto
            if (transform.position == posicionCuandoAtaca || colisionando)
            {
                atacandoSubiendo = true;
                atacandoBajando = false;
            }

            } else if (atacandoSubiendo == true) //sube de vuelta hasta la altura de vigilancia
        {
            transform.Translate(0, 0.03f, 0);
            if (transform.position.y >= 12.38f)
            {
                atacandoSubiendo = false;
            }
        }
        
    }
    private void vigilar()
    {

        if (direccion)
        {
            transform.Translate(0.03f, 0, 0);
        }
        else
        {
            transform.Translate(-0.03f, 0, 0);
        }

        if (transform.position.x > 28.5f)
        {
            direccion = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (transform.position.x < -7.5f)
        {
            direccion = true;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    private Node crearArbol() {

        //creo los nodes de acciones
        Node barrer = new Node(3, "barrer");
        Node atacar = new Node(3, "atacar");
        Node vigilar = new Node(3, "vigilar");

        //creo los nodos de preguntas
        Node veoEnemigos = new Node(2, "veoEnemigos");
        Node tieneLLave = new Node(2, "tieneLlave");

        //creo el arbol
        Node aux1 = new Node(0);
        aux1.children.Add(barrer);
        aux1.children.Add(veoEnemigos);

        Node izq = new Node(0);
        izq.children.Add(atacar);
        izq.children.Add(tieneLLave);

        Node dcha = new Node(1);
        dcha.children.Add(vigilar);
        dcha.children.Add(aux1);

        Node padre = new Node(1);
        padre.children.Add(dcha);
        padre.children.Add(izq);

        return padre;

    }


    private class Node
    {

        public List<Node> children = new List<Node>();
        public int tipo = 0; //0 si es un nodo secuencia (->), 1 si es un nodo selector (?), 2 si es una pregunta, y 3 si es una acción
        public string accionPregunta = "";

        public Node(int tipo_)
        {
            tipo = tipo_;
        }
        public Node(int tipo_, string accionPregunta_)
        {
            tipo = tipo_;
            accionPregunta = accionPregunta_;
        }

        public string comprobar(bool tieneLlave, bool veoEnemigos) {
            string aux = "noAccion";
            //dependiendo de que tipo de nodo sea realizamos una comprobacion diferente
            switch (tipo) {

                case 1://nodo selector(?)
                    //comprobamos los nodos empezando por el ultimo de la lista(el mas a la izq) siempre que no halla dado uno ya una accion
                    bool aux3 = false;//variable que define que ya tenemos una accion
                    int i = 1;
                    do {
                        aux = children[children.Count - i].comprobar(tieneLlave,veoEnemigos);
                        if (aux != "noAccion" && aux != "true" && aux != "false") {
                            aux3 = true;
                        }

                        i++;
                    } while (children.Count - i >= 0 && aux3 == false);
                    

                    break;
                case 0://nodo secuencial(->)
                       //en este caso comprobamos el nodo mas a la izquierda y si tiene una accion lo devolvemos como en el anterior,
                       //           pero en este caso si el primer nodo da resultado negativo dejamos de comprobar

                    bool aux4 = false;//variable que define que ya tenemos una accion
                    int i2 = 1;
                    do
                    {
                        aux = children[children.Count - i2].comprobar(tieneLlave, veoEnemigos);
                        if (aux != "noAccion" && aux != "true")
                        {
                            aux4 = true;
                        }

                        i2++;
                    } while (children.Count - i2 >= 0 && aux4 == false);

                    break;
                case 2://nodo pregunta
                    bool aux2 = false;
                    if (accionPregunta == "tieneLlave") {
                        aux2 = tieneLlave;
                    } else if (accionPregunta == "veoEnemigos") {
                        aux2 = veoEnemigos;
                    }

                    if (aux2) {
                        aux = "true";
                    }
                    else {
                        aux = "false";
                    }

                    break;
                case 3://nodo accion
                    aux = accionPregunta;


                    break;
            }

            return aux;

        }
        //primero añadir nodos de la derecha si queremos realizar la busqueda de izq a dcha.

    }

}


