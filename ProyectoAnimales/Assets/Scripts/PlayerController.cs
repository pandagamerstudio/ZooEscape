using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.InputSystem;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using ExitGames.Client.Photon;

public class PlayerController : MonoBehaviourPun,IPunObservable, IOnEventCallback
{


    public enum gra { 
    normal,
    izquierda,
    derecha,
    arriba
    }
    public gra g;
    public int id;

    public int idCanvas;
    public bool canvasBool;

    public float runSpeed = 2f;
    public float jumpSpeed = 3f;
    Rigidbody2D rb2D;
    CheckGround cg;

    public float doubleJumpSpeed = 2.5f;
    //public float pushForce = 10f;

    public bool canDoubleJump = false;
    public bool attached = false;

    public Transform attachedTo;
    private GameObject disregard;
    
    public bool superJump = false;
    public float lowJumpMultiplier = 1f;
    public float fallMultiplier = 0.5f;

    public Player photonPlayer;
    public SpriteRenderer sr;
    public Animator anim;
    Vector2 ad;
    float Salto;
    float agarrar;
    float gravedad;
    bool activadaGravedad = false;
    bool top = false;
    Vector2 gravedadLados;
    public static PlayerController me;

    public GameObject canvas;
    //GameObject canvasEnt;
    public GameObject canvasPause;
    public GameObject canvasVidas;


    //Para quitar lag
    public Vector2 posicionReal;
    public Vector2 posicionUltimoPaquete;
    public double tiempoActual;
    public double tiempoActualPaquete;
    public double tiempoUltimoPaquete;
    public double tiempo;

    private Animator animator;

    private CheckGround checkGround;
    public bool chocandLatPlat;
    public int aux;

    bool cargarUnaVez = true;
   public Transform posI;
    bool padre = false;
    public int scale;
    //Efectos sonido
    AudioVolume sonido;


 
    void Awake(){
        rb2D = this.GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        cg = this.GetComponentInChildren<CheckGround>();
        animator = this.GetComponent<Animator>();
        checkGround = this.GetComponentInChildren<CheckGround>();
        
        posicionReal = new Vector2(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y);
        posicionUltimoPaquete = Vector2.zero;
        tiempoActual=0.0;
        tiempoActualPaquete = 0.0;
        tiempoUltimoPaquete = 0.0;
        tiempo = 0.0;
        chocandLatPlat = false;
        aux = 0;
        cargarUnaVez = true;
        scale = 1;

        sonido = GameObject.Find("AudioManager").GetComponent<AudioVolume>();
    }

    public bool isMobile;

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    static extern bool IsMobile();
#endif

    void CheckIfMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif
    }

    void Start(){

        if (!photonView.IsMine)
        {
            canvasVidas = this.transform.GetChild(1).gameObject;
            canvasVidas.SetActive(true);
        }


        if (!photonView.IsMine)
            return;

        canvasPause = this.transform.GetChild(3).gameObject;
        canvasVidas = this.transform.GetChild(1).gameObject;

        posI = gameObject.transform;
        var user = GetComponent<PlayerInput>().user;
        //canvas = GameObject.FindWithTag("Canvas");
        canvas = this.transform.GetChild(2).gameObject;
        canvas.SetActive(false);
        canvasVidas.SetActive(true);
        /*
        if (SystemInfo.deviceType == DeviceType.Desktop){
            user.ActivateControlScheme("Keyboard&Mouse");
        } else if (SystemInfo.deviceType == DeviceType.Handheld){
            user.ActivateControlScheme("Movil");   
        }*/

        CheckIfMobile();
        if(isMobile){
            canvas.SetActive(true);

            //canvasEnt = GameObject.FindWithTag("Canvas");
            //GameObject b = Instantiate(canvas, new Vector3(937f, 395f, 0), Quaternion.identity);
            //b.transform.parent = canvasEnt.transform;
        }else{
            Destroy(canvas);
        }
    }
    public void checkGravedad() {

        switch (g) {
            case gra.normal:
                rb2D.velocity = new Vector2(runSpeed * ad.x, rb2D.velocity.y);
                break;
            case gra.izquierda:
                rb2D.velocity = new Vector2(rb2D.velocity.x, -runSpeed * ad.x);
                break;
            case gra.derecha:
                rb2D.velocity = new Vector2(rb2D.velocity.x, runSpeed * ad.x);
                break;
            case gra.arriba:
                rb2D.velocity = new Vector2(-runSpeed * ad.x, rb2D.velocity.y);
                break;
        
        }
    
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            tiempo = tiempoActualPaquete - tiempoUltimoPaquete;
            tiempoActual += Time.deltaTime;
            if (!padre)
            {
                transform.position = Vector2.Lerp(posicionUltimoPaquete, posicionReal, (float)(tiempoActual / tiempo));
            }
           

        }
        else if(!chocandLatPlat) {
            
            checkGravedad();
            if (ad.x != 0)
            {
                animator.SetBool("Walk", true);
            }
            else {
                animator.SetBool("Walk", false);

            }
        }
    }

    public void Pause(InputAction.CallbackContext callback)
    {
        OnPause();
    }

    public void OnPause(){
        if (!photonView.IsMine) return;
        
        canvasPause.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        if (isMobile)
            canvas.SetActive(false);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
      

        if (eventCode == 1)
        {
     
            moverJug();
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    [PunRPC]
    public void moverJug()
    {
        Debug.Log("4");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("5");
            //gameObject.transform.position = new Vector3(-6,-2.3f,0);
            gameObject.transform.position = GameManager.instance.spawnPoints[0].position;

            GameObject[] jugs = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject j in jugs){
                if (j.GetComponent<PlayerController>().photonPlayer != photonPlayer){
                    j.transform.position = GameManager.instance.spawnPoints[1].position;
                }
            }
        }
        else
        {
            //gameObject.transform.position = new Vector3(-1.1f, -2.3f, 0);
            gameObject.transform.position = GameManager.instance.spawnPoints[0].position;
        }
        cargarUnaVez = true;

    }

    public void Movimiento(InputAction.CallbackContext callback) {
        Player p = PhotonNetwork.LocalPlayer;

        if (!photonView.IsMine && p != photonPlayer)
            return;

        ad = callback.ReadValue<Vector2>();

        Flip();
      
    }

    public void salto(InputAction.CallbackContext callback)
    {
        if (!photonView.IsMine && !canvasBool)
            return;

        if(attached)
            return;

        Salto = callback.ReadValue<float>();
        
        Saltar(Salto);
    }
    
    public void Gravedad(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;
        gravedad = callback.ReadValue<float>();

        if (gravedad > 0 && activadaGravedad == false){
            if (top == false){
                transform.eulerAngles = new Vector3(0,0,180f);
                Physics2D.gravity = new Vector2 (0,29.43f);
            } else {
                transform.eulerAngles = Vector3.zero;
                Physics2D.gravity = new Vector2 (0,-29.43f);
            }

            top = !top;
            StartCoroutine(activarGravedad());
        }
        
    }

    private IEnumerator activarGravedad()
    {
        activadaGravedad = true;
        yield return new WaitForSeconds(1f);
        activadaGravedad = false;
    }
    public void GravedadLados(InputAction.CallbackContext callback) {
        if (!photonView.IsMine)
            return;
        gravedadLados = callback.ReadValue<Vector2>();

        if (gravedadLados.x > 0){
            Physics2D.gravity = new Vector2 (29.43f, 0);
            transform.eulerAngles = new Vector3(0,0,90f);
        } else if (gravedadLados.x < 0){
            Physics2D.gravity = new Vector2 (-29.43f, 0);
            transform.eulerAngles = new Vector3(0,0,-90f);
        }
        
    }
   public void Flip(){

        switch (g) {
            case gra.normal:
                transform.eulerAngles = Vector3.zero;
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.izquierda:
            transform.eulerAngles = new Vector3(0, 0, -90f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.derecha:
                transform.eulerAngles = new Vector3(0, 0, 90f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
            case gra.arriba:
                transform.eulerAngles = new Vector3(0, 0, 180f);
                if (ad.x > 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f) * (scale);
                }
                else if (ad.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f) * scale;
                }
                break;
        }
    }

    void Saltar(float s){
        float jumpM = 1f;
        if (scale == 2)
            jumpM = 1.5f;

        Vector2 velocidad=Vector2.zero;
        Vector2 velocidadDoble = Vector2.zero;
        Vector2 velocidadSuper = Vector2.zero;
        switch (g) {
            case gra.normal:
                velocidad= new Vector2(rb2D.velocity.x, jumpSpeed * jumpM);
                velocidadDoble= new Vector2(rb2D.velocity.x, doubleJumpSpeed * jumpM);
                velocidadSuper= new Vector2(rb2D.velocity.x, Physics2D.gravity.y);
                break;
            case gra.izquierda:
                velocidad = new Vector2(jumpSpeed * jumpM, rb2D.velocity.y);
                velocidadDoble = new Vector2(doubleJumpSpeed * jumpM, rb2D.velocity.y);
                velocidadSuper= new Vector2(-Physics2D.gravity.y, rb2D.velocity.y);
                break;
            case gra.derecha:
                velocidad = new Vector2(-jumpSpeed * jumpM, rb2D.velocity.y);
                velocidadDoble = new Vector2(-doubleJumpSpeed * jumpM, rb2D.velocity.y);
                velocidadSuper= new Vector2(Physics2D.gravity.y, rb2D.velocity.y);
                break;
            case gra.arriba:
                velocidad= new Vector2(rb2D.velocity.x, -jumpSpeed * jumpM);
                velocidadDoble= new Vector2(rb2D.velocity.x, -doubleJumpSpeed * jumpM);
                velocidadSuper= new Vector2(rb2D.velocity.x, -Physics2D.gravity.y);
                break;
        }
        if (cg.isGrounded)
        {
            canDoubleJump = true;
            sonido.playSfx("salto");
            rb2D.velocity =velocidad;
            
        }
        else
        {
            if (s > 0f)
            {
                if (canDoubleJump)
                {
                    rb2D.velocity = velocidadDoble;
                    canDoubleJump = false;
                    sonido.playSfx("salto");
                    return;
                }
            }
        }

        if (superJump)
        {
            if (rb2D.velocity.y > 0 && s == 0)
                rb2D.velocity = velocidadSuper;
        }
    }

    public void OnSaltarButton(){
        if (!photonView.IsMine)
            return;

        Saltar(1);
    }

    public void OnMoveButton(float dir){
        if (!photonView.IsMine)
            return;

        ad = new Vector2(dir, 0);
        Flip();
    }

    public void OnReiniciar(){
        if (cargarUnaVez && PhotonNetwork.IsMasterClient)
        {
            // PhotonNetwork.LoadScene("SceneName");
            cargarUnaVez = false;
            Debug.Log("2");
            /*if (SceneManager.GetActiveScene().name == "Level1")
            {
                Debug.Log("3");
                GameObject.Find("SpawnManager").GetComponent<SpawnManagerLevel1>().reiniciarNivel();
                //   photonView.RPC("moverJug", RpcTarget.All);
                RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                PhotonNetwork.RaiseEvent(1, null, raiseEvent, SendOptions.SendReliable);
            }
            else if (SceneManager.GetActiveScene().name == "Level2")
            {
                GameObject.Find("SpawnManager").GetComponent<SpawnManagerLevel1>().reiniciarNivel();
                photonView.RPC("moverJug2", RpcTarget.All);
            }
            else
            {
                GameObject.Find("SpawnManager").GetComponent<SpawnManagerLevel3>().reiniciarNivel();
                photonView.RPC("moverJug3", RpcTarget.All);
            }*/

            //GameObject.Find("SpawnManager").GetComponent<SpawnManagerLevel1>().reiniciarNivel();
            //RaiseEventOptions raiseEvent = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            //PhotonNetwork.RaiseEvent(1, null, raiseEvent, SendOptions.SendReliable);

            PhotonNetwork.DestroyAll();
            PlayerPrefs.SetString ("Scene", SceneManager.GetActiveScene().name);
            PhotonNetwork.LoadLevel("Recargar");


        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(new Vector2(transform.position.x, transform.position.y));
        }
        else {
            tiempoActual = 0.0;
            posicionUltimoPaquete = transform.position;
            posicionReal = (Vector2)stream.ReceiveNext();
            tiempoUltimoPaquete = tiempoActualPaquete;
            tiempoActualPaquete = info.SentServerTime;
        }
    }

    [PunRPC]
    public void Initialize(Player player){
        id = player.ActorNumber;
        photonPlayer = player;

        GameManager.instance.players[id - 1] = this;

        this.gameObject.layer = 9+id;
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("platMovil")&&!padre)
        {
            padre = true;
          //  Vector3 scala = transform.localScale;
           transform.SetParent(collision.gameObject.transform);
          //  transform.SetParent(collision.gameObject.transform);
         //   transform.localScale = scala;
                
        }
        //No quirto que los objetos copia de los otro jugadores lo ejecuten
        if (!photonView.IsMine)
            return;
        if (!collision.gameObject.tag.Equals("suelo") && !collision.gameObject.tag.Equals("Objects") && !collision.gameObject.tag.Equals("platMovil")) {
            return;
        }

   
      
        if ( !checkGround.isGrounded)
        {
            chocandLatPlat = true;

        }
        else if(checkGround.isGrounded) {
            chocandLatPlat = false;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (!photonView.IsMine)
            return;

        if (!collision.gameObject.tag.Equals("suelo") && !collision.gameObject.tag.Equals("Objects") && !collision.gameObject.tag.Equals("platMovil")) {
            return;
        }

        if ( checkGround.isGrounded)
        {
            chocandLatPlat = false;

        }
        else {
            chocandLatPlat = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("platMovil") &&padre)
        {
            transform.SetParent(null);

            padre = false;

        }
        if (!photonView.IsMine)
            return;
        if (!collision.gameObject.tag.Equals("suelo") && !collision.gameObject.tag.Equals("Objects") && !collision.gameObject.tag.Equals("platMovil")) {
            return;
        }
 
            chocandLatPlat = false;
        
     
    }


    public void OnclickCanvas(){
        StartCoroutine(canvasCo());
    }

    IEnumerator canvasCo(){
        canvasBool = true;
        yield return new WaitForSeconds(2.0f);
        canvasBool = false;
    }

    public void changeScale(float f){
        transform.localScale = new Vector3(f,f,f);
        scale = (int)f;
    }



}
