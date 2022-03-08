using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
{

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnPlayerScored;

    [SerializeField] private float tapForce = 250;
    [SerializeField] private float tiltSmooth = 5;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Rigidbody2D rigidBody;
    private Quaternion downRotation;
    private Quaternion forwadRotation;

    GameManager gameManager;

    private void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();

        downRotation = Quaternion.Euler(0, 0, -90);

        forwadRotation = Quaternion.Euler(0, 0, 35);

        gameManager = GameManager.Instance;
    }
    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.simulated = true;     
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPosition;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (gameManager.gameOver) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = forwadRotation;
            rigidBody.velocity = Vector3.zero;
            rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ScoreZone")
        {
            //register a score event
            OnPlayerScored(); //event sent to GameManager;
            //play a sound
        }
        if (collision.gameObject.tag == "DeadZone")
        {
            //register a dead event
            OnPlayerDied();// event send to GameManager;
            //play a sound
            rigidBody.simulated = false;
        }
    }


}
