using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    Vector3 rot = Vector3.zero;

    PlayerControls playerControls;
    public Vector2 movementInput;

    [SerializeField] AudioSource creatureSFX;
    [SerializeField] AudioSource homeSFX;
    // Start is called before the first frame update

    public int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    

    public string creatureName = "Chao";
    void Start()
    {
        rot = Vector3.zero;
        Input.gyro.enabled = true;

        if (playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.Movement.Moving.performed += i => movementInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //  Debug.Log(Input.gyro.attitude);
        transform.rotation = Input.gyro.attitude;
        Vector3 movementVector = new Vector3(movementInput.x, movementInput.y, 0);
        transform.position = transform.position + (movementVector);
        scoreText.text = score.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("House"))
        {

            Debug.Log("A Kasa");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("House"))
        {

            Debug.Log("A Kasa");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        homeSFX.Play();
        if (collision.gameObject.CompareTag("House"))
        {

            SceneManager.LoadSceneAsync("Assets/Scenes/Home.unity");
            Debug.Log("Entro la balubi");
        }
        if (collision.gameObject.CompareTag("Door"))
        {
            
            SceneManager.LoadSceneAsync("Assets/Scenes/SampleScene.unity");
            MenuController.instance.jugar();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Draggable dragged = collision.gameObject.GetComponent<Draggable>();
        if (dragged != null)
        {
            if (dragged.gameObject.CompareTag("Fruit"))
            {
                creatureSFX.Play();
                //Vibrator.Vibrate();
                    dragged.gameObject.SetActive(false);
                score += 1;

            }
        } 
    }
 
}
    

       

