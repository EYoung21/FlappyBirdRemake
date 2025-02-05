using Mono.Cecil.Cil;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// using System.Collections;
// using System.Collections.Generic;

public class ChickenController : MonoBehaviour
{

    public TextMeshPro scoreText;

    public AudioClip[] flappySounds;

    private AudioSource _chickenAudioSource;

    private Rigidbody2D chickenRigidbody;

    private SpriteRenderer _chickenSpriteRenderer;

    [Header("Movement")]
    public float speed;
    [Header("Jumping")]
    public float flapForce;

    [Header("Animation")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int currScore = 0;

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Obstacle")) {
            ReloadScene();
        } 
        // else if (other.gameObject.CompareTag("ScoreTrack")) {
        //     currScore++;
        //     _chickenAudioSource.Play(); //should play bing chilling
        // }
    }

    // private void OnTriggerEnter2D(Collision2D collision) {
    //     currScore++;
    //     _chickenAudioSource.Play(); //should play bing chilling
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        currScore++;
        _chickenAudioSource.Play(); //should play bing chilling
        scoreText.text = "SCORE: " + currScore.ToString();
    }

    private void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void handleJump() {
        Vector2 velocity = chickenRigidbody.linearVelocity;
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = flapForce;
            chickenRigidbody.linearVelocity = velocity;
            PlayFlappingSound();
        }
    }

    private void MoveChickenForward() {
        // POSITION BASED: transform.position += Vector3.right * speed * Time.deltaTime;
        Vector2 velocity = chickenRigidbody.linearVelocity;
        velocity.x = speed;
        chickenRigidbody.linearVelocity = velocity;
    }

    void Start()
    {
        chickenRigidbody = GetComponent<Rigidbody2D>();
        _chickenAudioSource = GetComponent<AudioSource>();
        _chickenSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void PlayFlappingSound() {
        _chickenAudioSource.PlayOneShot(flappySounds[Random.Range(0, flappySounds.Length - 1)]); //not playing for some reason??
    }

    // Update is called once per frame
    void Update()
    {
        //sets sprite accordingly
        var yvelocity = chickenRigidbody.linearVelocity.y;
        if (yvelocity > 0) {
            _chickenSpriteRenderer.sprite = spriteUp;
        } else if (yvelocity < 0) {
            _chickenSpriteRenderer.sprite = spriteDown;
        }
        handleJump(); //must be called before MoveChickenForward for some reason
        MoveChickenForward();

        Vector2 normalizedDir = chickenRigidbody.linearVelocity.normalized;
        transform.right = normalizedDir;

        scoreText.text = "Current Score: " + currScore.ToString();
    }
}
