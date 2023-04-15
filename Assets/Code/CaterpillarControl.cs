using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CaterpillarControl : MonoBehaviour
{
    public float moveSpeed;
    public float steerSpeed = 180;
    public int gap = 60;
    public bool foodExists = false;
    public TextMeshProUGUI scoreNumText;
    //public TextMeshProUGUI diffMode;   //showing the difficulty while playing
    public static string difficultyPicked;
    public int score = 0;

    public GameObject bodySegment;
    public GameObject food;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> movementHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()  //new game
    {
        //diffMode.text = difficultyPicked;
        score = 0;
        GrowBody();
        GrowBody();
        GrowBody();
        GrowBody();
        FoodSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (difficultyPicked)
        {
            case "EASY":
                EasyMode();
                break;

            case "NORMAL":
                NormalMode();
                break;

            case "HARD":
                HardMode();
                break;
        }


        // moving forward
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        // movement history
        movementHistory.Insert(0, transform.position);

        // moving body parts
        int index = 0;

        foreach (var body in bodyParts)
        {
            Vector3 point = movementHistory[Mathf.Min(index * gap, movementHistory.Count - 1)];
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * moveSpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }

        // steering movement
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);

        // updating score
        scoreNumText.text = score.ToString();


    }

    // Growing caterpillar
    private void GrowBody()
    {
        GameObject body = Instantiate(bodySegment);
        bodyParts.Add(body);
    }

    // Spawning food
    private void FoodSpawn()
    {
        if (foodExists)
        {
            return;
        }

        food = GameObject.Instantiate(food);

        float x = Random.Range(-11.5f, 11.5f);
        float z = Random.Range(-8.0f, 8.0f);

        food.transform.position = new Vector3(x, 2.75f, z);
        foodExists = true;
    }

    //Despawning/Eating food
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Lettuce"))
        {
            Destroy(food);
            score++;
            foodExists = false;
            GrowBody();
            FoodSpawn();
        }

        if (collider.gameObject.CompareTag("Stone"))
        {
            GameOver();
        }

        if(collider.gameObject.CompareTag("Body") && (difficultyPicked == "NORMAL" || difficultyPicked == "HARD"))
        {
            GameOver();
        }
    }

    // Easy mode (wall hit)
    private void EasyMode()
    {
        moveSpeed = 4;
    }

    // Normal mode (wall hit + self hit)
    private void NormalMode()
    {
        EasyMode();
        moveSpeed = 6;
    }

    // Hard mode  (wall hit + self hit + faster+ every 6 food)
    private void HardMode()
    {
        NormalMode();
        moveSpeed = 8;
        gap = 40;
    }

    // Game over
    private void GameOver()
    {
        foreach (GameObject part in bodyParts)
        {
            Destroy(part);
        }
        bodyParts.Clear();

        SceneManager.LoadScene(2);
    }
}
