using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarControl : MonoBehaviour
{
    public float moveSpeed = 5;
    public float steerSpeed = 180;
    public int gap = 60;
    public int bodySpeed = 5;
    public bool foodExists = false;

    public GameObject bodySegment;
    public GameObject food;

    private List<GameObject> bodyParts = new List<GameObject>();
    private List<Vector3> movementHistory = new List<Vector3>();

    // Start is called before the first frame update
    void Start()  //new game
    {
        bodyParts.Clear();
        GrowBody();
        GrowBody();
        GrowBody();
        GrowBody();
        FoodSpawn();  
    }

    // Update is called once per frame
    void Update()
    {

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
            body.transform.position += moveDirection * bodySpeed * Time.deltaTime;
            body.transform.LookAt(point);
            index++;
        }

        // steering movement
        float steerDirection = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDirection * steerSpeed * Time.deltaTime);
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
    private void OnTriggerEnter (Collider collider)   
    {
        if (collider.gameObject.CompareTag("Lettuce"))
        {
            Destroy(food);
            foodExists = false;
            GrowBody();
            FoodSpawn();
        }
    }

    // Easy mode (wall hit)

    // Normal mode (wall hit + self hit)

    // Hard mode  (wall hit + self hit + faster+ every 6 food)

    // Game over
    
}
