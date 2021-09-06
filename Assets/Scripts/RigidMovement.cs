using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RigidMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject gameOverText;
    public Text score;
    public GameObject boostBar;
    public GameObject jumpBar;

    public PlayfabManager playfabManager;


    private Rigidbody rb;
    private Vector3 velocityDirection;
    private float boostTimer = .0001f;
    private float jumpTimer = .0001f;

    private float time;
    private int timeInt;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (transform.position.y < 0)
        {
            GameOver();
        }

        time += Time.deltaTime;
        timeInt = (int)(time * 100);


        score.text = (100 * time).ToString("F0");

        velocityDirection = rb.velocity.normalized;

        boostBar.transform.localScale = new Vector3(1f, (1f - boostTimer/3), 1f);
        jumpBar.transform.localScale = new Vector3(1f, (1f - jumpTimer/3), 1f);

        if (boostTimer > 0.01)
        {
            boostTimer -= Time.deltaTime;
        }        
        
        if (jumpTimer > 0.01)
        {
            jumpTimer -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector3.forward * speed);
        }        
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(-Vector3.left * speed);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Boost();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Jump();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        GameOver();
    }

    public void Boost()
    {
        if (boostTimer < 0.02)
        {
            rb.AddForce(velocityDirection * 500);
            boostTimer = 3;
            Debug.Log("boosted");
        }
    }

    public void Jump()
    {
        if (jumpTimer < 0.02)
        {
            rb.AddForce(Vector3.up * 250);
            jumpTimer = 3;
            Debug.Log("jump");
        }
    }

    public void GameOver()
    {
        Debug.Log("game over");

        Time.timeScale = 0;

        gameOverText.SetActive(true);

        playfabManager.SendLeaderboard(timeInt);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}