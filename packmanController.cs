using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class packmanController : MonoBehaviour {

    
    public GameObject packman;

    public GameObject ghostBusters;
    public GameObject ghostBuster;
    public GameObject bonusTime;
    public GameObject bonusLife;
    public AudioClip numnum;
    public AudioClip eatGhost;
    public AudioClip eatBonus;
    AudioSource audio;
    public float movementSpeed = 0f;
    public Text countObjects;
    public Text time;
    public Text winText;
    public Text lifes;
    public Button mainMenu;
    public Button playAgain;
    public Text pause;
    public bool paused;
    public Vector3     up = new Vector3(0, 0, 0),
                        right = new Vector3(0, 90, 0),
                        down = new Vector3(0, 180, 0),
                        left = new Vector3(0, 270, 0),
                        currentDirection = new Vector3(0, 0, 0);
    Animator anim;
    public int count;
    public float timeLeft;
    private Rigidbody rb;
    public GameObject[] ghostColor;
    public GameObject[] ghosts;
    private Color color;
    public int lifeCount;
    public int tempTime;
    public float bonusTimeLeft;
    bool isMoving;
    public float currentAcceleration;


    ghostController[] gh;


    // Use this for initialization
    void Start()
    {
       gh = GetComponents<ghostController>();
        
        currentAcceleration = Input.acceleration.y;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        count = 0;
        timeLeft = 299;
        mainMenu.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        ghostBusters.gameObject.SetActive(false);
        ghostBuster.gameObject.SetActive(false);
        bonusTime.gameObject.SetActive(false);
        bonusLife.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        lifeCount = 2;
        lifes.text = lifeCount.ToString();
        ghosts = GameObject.FindGameObjectsWithTag("ghost");
        
    }
	// Update is called once per frame
	void Update () {
      
        tempTime = (int)Mathf.Round(timeLeft);
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        pausePc();
        pausePhone();
        setCanvas();
        checkGhost();
        playerMoveAcceleration();
       // playerMovePc();
        activateBonuses();
        setFinalEnviroment();
        isMoving = true;
        anim.enabled = true;
       
        transform.localEulerAngles = currentDirection;
        if (isMoving)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
            anim.enabled = false;
        transform.Rotate(new Vector3(0, 90, 0));
        var check = getPhonePosition();
        if (check.HasValue)
        {
            currentAcceleration = check.Value;
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    void finalText(int final)
    {
        if (final == 0)
        {
            winText.text = "You Win!";
            winText.color = Color.blue;
            mainMenu.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            
        }
        else
        {
            winText.text = "Game Over!";
            winText.color = Color.red;
            mainMenu.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            lifeCount = 0;
            packman.gameObject.SetActive(false);
        }
       
            
    }
   public void ghostStatus(bool status)
    {
        foreach (GameObject ghost in ghosts)
        {
            ghost.gameObject.SetActive(status);
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            audio.PlayOneShot(numnum);
            other.gameObject.SetActive(false);
            count++;
            
        }
        if (other.gameObject.CompareTag("ghost"))
        {
            int count=0;
            ghostColor = GameObject.FindGameObjectsWithTag("ghostColor");
            foreach (GameObject ghost in ghostColor)
            {
                if (ghost.GetComponent<Renderer>().material.color == Color.black )
                {
                    count++;
                }
            }
            if (count > 0)
            {
                bonusTimeLeft = 10;
                audio.PlayOneShot(eatGhost);
                foreach (GameObject ghost in ghosts)
                {
                    if (other.gameObject == ghost)
                        ghost.SetActive(false);
                }
            }
            else
            {
                lifeCount--;
                if (lifeCount > 0)
                    packman.transform.position = new Vector3(0, 7, 0);
                else
                {
                    packman.gameObject.SetActive(false);
                    finalText(1);
                }
              
            }
            
        }
        if (other.gameObject.CompareTag("ghostBusters"))
        {
            bonusTimeLeft = 10;
            ghostStatus(false);
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);

        }
        if (other.gameObject.CompareTag("ghostBuster"))
        {
            bonusTimeLeft = 10;
            other.gameObject.SetActive(false);
            changeGhostColor(Color.black);
            audio.PlayOneShot(eatBonus);
            
        }
        if (other.gameObject.CompareTag("timeBonus"))
        {
            bonusTimeLeft = 10;
            timeLeft += 50;
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);
        }
        if (other.gameObject.CompareTag("lifeBonus"))
        {
            bonusTimeLeft = 10;
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);
            lifeCount++;

        }
    }

    public void Die()
    {
        lifeCount--;
        if (lifeCount > 0)
            packman.transform.position = new Vector3(0, 7, 0);
        else
        {
            packman.gameObject.SetActive(false);
            finalText(1);
        }
           

    }
    void changeGhostColor(Color col)
    {
        ghostColor = GameObject.FindGameObjectsWithTag("ghostColor");
        foreach (GameObject ghost in ghostColor)
        {
            ghost.GetComponent<Renderer>().material.color = col;
        }
    }
    Vector3 randomPosition()
    {
        float x = Random.Range(-110, 110);
        float z = Random.Range(-110, 110);
        Vector3 randomPosition = new Vector3(x, 4, z);
        return randomPosition;
    }
 
    void playerMovePc()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentDirection = up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDirection = right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentDirection = down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDirection = left;
        }
         else
             isMoving = false;
    }
     void pausePc()
     {
         if (Input.GetKey(KeyCode.P))
         {
             paused = !paused;
         }
         if (paused)
         {
             Time.timeScale = 0;
             pause.text = "Paused";
             mainMenu.gameObject.SetActive(true);
         }
         else
         {
             Time.timeScale = 1;
             pause.text = "";
             mainMenu.gameObject.SetActive(false);
         }
     }
     void pausePhone()
     {
         if (Input.touchCount > 0)
         {
             paused = !paused;
         }
         if (paused)
         {
             Time.timeScale = 0;
             pause.text = "Paused";
             mainMenu.gameObject.SetActive(true);
         }
         else
         {
             Time.timeScale = 1;
             pause.text = "";
             mainMenu.gameObject.SetActive(false);
         }
     }
    void activateBonuses()
    {
        if (tempTime % 3 == 0 && ghostBusters.active == false && ghostBuster.active == false && bonusTime.active == false && bonusLife.active == false)
        {
            ghostBusters.transform.position = randomPosition();
            ghostBusters.gameObject.SetActive(true);

            ghostBuster.transform.position = randomPosition();
            ghostBuster.gameObject.SetActive(true);

            bonusTime.transform.position = randomPosition();
            bonusTime.gameObject.SetActive(true);

            bonusLife.transform.position = randomPosition();
            bonusLife.gameObject.SetActive(true);

            bonusTimeLeft = 10;
        }
        if (bonusTimeLeft > 0 && bonusTimeLeft <= 10)
        {
            bonusTimeLeft -= Time.deltaTime;
            
        }
        else
        {
            ghostBuster.gameObject.SetActive(false);
            ghostBusters.gameObject.SetActive(false);
            bonusTime.gameObject.SetActive(false);
            bonusLife.gameObject.SetActive(false);
            bonusTimeLeft = 0;
      
        }
    }
    void checkGhost()
    {
        foreach (GameObject ghost in ghosts)
        {
            if (ghost.active == false)
                if (bonusTimeLeft < 0)
                {
                    ghostStatus(true);
                    changeGhostColor(Color.green);
                    return;
                }
        }
    }
     void setFinalEnviroment()
    {
        if (lifeCount < 1 && winText.text != "You Win!")
        {
            finalText(1);
        }
        if (winText.text == "You Win!")
        {
            ghostStatus(false);
        }
        if (count == 254)
        {
            finalText(0);
        }
        if (timeLeft < 1 && winText.text != "You Win!")
        {
            finalText(1);
        }
    }
    void setCanvas()
    {
        tempTime = (int)Mathf.Round(timeLeft);
        time.text = "Time left: " + tempTime.ToString() + " seconds";
        lifes.text = "Lifes: " + lifeCount.ToString();
        countObjects.text = "Count " + count.ToString() + "/254";
    }

    float? getPhonePosition()
    {


        int tl = (int)Mathf.Round(timeLeft);
        float tmpcurrentAcceleration;
        if (tl % 2 == 0)
        {
            tmpcurrentAcceleration = Input.acceleration.y;
            return tmpcurrentAcceleration;
        }
        else
        {
            return null;
        }
        

    }

   void playerMoveAcceleration()
    {
        float x = Input.acceleration.x;
        //float z = Input.acceleration.z;
        float y = Input.acceleration.y;

        if (currentDirection != down)
        {

            if (y > currentAcceleration && (x < 0.04f || x > -0.04f))
                currentDirection = up;

            if (x > 0.06f)
            {
                currentDirection = right;
                y = currentAcceleration;
            }


            if (x < -0.06f)
            {
                currentDirection = left;
                y = currentAcceleration;
            }


            if (y < currentAcceleration - 0.02f && (x < 0.06f || x > -0.06f))
                currentDirection = down;


        }
        if (currentDirection != up)
        {
            if (y > currentAcceleration + 0.02f && (x < 0.06f || x > -0.06f))
                currentDirection = up;
            if (x > 0.06f)
            {
                currentDirection = right;
                y = currentAcceleration;
            }

            if (x < -0.06f)
            {
                currentDirection = left;
                y = currentAcceleration;
            }

            if (y < currentAcceleration && (x < 0.06f || x > -0.06f))
                currentDirection = down;
        }
    }
}


