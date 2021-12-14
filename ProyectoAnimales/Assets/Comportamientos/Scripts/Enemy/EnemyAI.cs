using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public Animator myAnimator;

    [SerializeField]
    protected Transform knifePos;

    [SerializeField]
    public float movementSpeed;

    private bool facingRight;

    [SerializeField]
    private GameObject knifePrefab;

    public Collider2D sight;

    private float throwTimer;
    private float throwAnimTimer;
    private float throwCoolDown = 3f;
    private bool canThrow = true;
    private float throwKnifeTimer;
    private bool canSpawn = true;

    protected bool Attack { get; set; }

    public GameObject Target { get; set; }

    private IEnemyState currentState;

    public bool tieneLlave = false;

    public Text estadoTxt;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myAnimator = GetComponent<Animator>();

        ChangeState(new IdleState());
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Execute();

        LookAtTarget();
    }

    public void ChangeDirection()
    {
        Debug.Log("changeDir");
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public void ThrowKnife()
    {

        throwTimer += Time.deltaTime;

        if (throwTimer >= throwCoolDown)
        {
            canThrow = true;
            throwTimer = 0f;
        }
        if (canThrow)
        {
            movementSpeed = 0f;
            myAnimator.SetBool("throw", true);
            InstKnife();
            throwAnimTimer += Time.deltaTime;
            if (throwAnimTimer >= 0.5f)
            {
                throwAnimTimer = 0f;
                myAnimator.SetBool("throw", false);
                canThrow = false;
                movementSpeed = 4f;
            }
        }
    }

    private void InstKnife()
    {
        throwKnifeTimer += Time.deltaTime;
        if(throwKnifeTimer >= 0.5f)
        {
            canSpawn = true;
        }
        if(canSpawn)
        {
            if (facingRight)
            {
                Debug.Log("right");
                knifePrefab.transform.localScale = new Vector3(transform.localScale.x * 1, 1, 1);
                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.identity);
                tmp.GetComponent<Knife>().Initialize(Vector2.right);
            }
            else
            {
                Debug.Log("left");
                //knifePrefab.transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.identity);
                tmp.transform.localScale = new Vector3(tmp.transform.localScale.x * -1, 1, 1);
                tmp.GetComponent<Knife>().Initialize(Vector2.left);
            }
            canSpawn = false;
            throwKnifeTimer = 0f;
        }
    }

    private void LookAtTarget()
    {
        if(Target != null)
        {
            float xDir = Target.transform.position.x - transform.position.x;

            if(xDir < 0 && facingRight || xDir > 0 && !facingRight)
            {
                ChangeDirection();
            }
        }
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        StartCoroutine(reactivarSight());

        currentState = newState;

        currentState.Enter(this);
    }

    public void Move()
    {
        myAnimator.SetFloat("speed", 1);
        transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    IEnumerator reactivarSight()
    {
        Debug.Log("reactivarVista");
        yield return new WaitForSeconds(1);
        sight.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.MyOnTriggerEnter(collision);
    }

}
