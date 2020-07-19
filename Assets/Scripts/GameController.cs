using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{

    public static int mode = 0;

    public GameObject asteroid;
    public GameObject alienship;
    

    private int score;
    private int hiscore;
    private int asteroidsRemaining;
    private int lives;
    private int wave;
    private int increaseEachWave = 2;
    public Camera cam;

    public GameObject ship;
    public GameObject bullet;

    public Text scoreText;
    public Text livesText;
    public Text waveText;
    public Text hiscoreText;
    public Text countdownText;
    public Text gameover;
    public Text pressx;
    public Text pressz;
    public Text gamemode;

    // Use this for initialization
    void Start()
    {
        //get mode choice
        mode = MenuManager.menumode;
        Debug.Log(mode);

        hiscore = PlayerPrefs.GetInt("hiscore", 0);
        StartCoroutine(countdown_text());
        StartCoroutine(startgame());
        if (mode == 1)
        {
            gamemode.text = "Classic Mode";
        }
        else if (mode == 2)
        {
            gamemode.text = "Hard Mode";
        }
        else if (mode == 3)
        {
            gamemode.text = "Infinite Mode";
        }

    }
    








    //waits 3 seconds, repositions ship, begins game
    IEnumerator startgame()
    {
        ship.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ship.GetComponent<Rigidbody2D>().angularVelocity = 0;
        yield return new WaitForSeconds(3);
        
        BeginGame();
    }
    //counts down from 3 to 1
    IEnumerator countdown_text()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "";
    }



    void Update()
    {

        if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("salmon"))
        {

            cam.backgroundColor = new Color(255f / 255f, 114f / 255f, 114f / 255f);
        }
        else if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("sky"))
        {


            cam.backgroundColor = new Color(135f / 255f, 206f / 255f, 235f / 255f); ;
        }
        else if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("teal"))
        {
            cam.backgroundColor = new Color(0f / 255f, 128f / 255f, 128f / 255f);
        }
        else if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("yellow"))
        {

            cam.backgroundColor = new Color(255f / 255f, 211f / 255f, 0f / 255f);
        }
        else
        {
            Debug.Log("none were chosen");
        }



        //go to main menu
        if (Input.GetKey("escape"))
            SceneManager.LoadScene("Menu");

        //restart mode
        if (Input.GetKeyDown("x"))
        {
            score = 0;
            lives = 3;
            wave = 0;
            gameover.text = "";
            pressx.text = "";
            pressz.text = "";
            waveText.text = "";
            scoreText.text = "";
            hiscoreText.text = "";
            livesText.text = "";

            repositionShip();
            DestroyAliens();
            DestroyExistingAsteroids();
            
            StartCoroutine(countdown_text());
            StartCoroutine(startgame());
        }

        //go to main menu
        if (Input.GetKeyDown("z"))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown("e"))
        {
            SceneManager.LoadScene("ModeScene");
        }


    }

    void BeginGame()
    {
        //classic mode
        if (mode == 1)
        {
            score = 0;
            lives = 3;
            wave = 1;

            scoreText.text = "Score:" + score;
            hiscoreText.text = "Highest: " + hiscore;
            livesText.text = "Lives: " + lives;
            waveText.text = "Wave #" + wave;
            gameover.text = "";
            pressx.text = "";
            pressz.text = "";

            repositionShip();
            SpawnAlien();
            SpawnAsteroids();

        } 

        //hard mode
        else if (mode == 2) {
            score = 0;
            lives = 3;
            wave = 1;

            scoreText.text = "Score:" + score;
            hiscoreText.text = "Highest: " + hiscore;
            livesText.text = "Lives: " + lives;
            waveText.text = "Wave #" + wave;
            gameover.text = "";
            pressx.text = "";
            pressz.text = "";

            repositionShip();
            SpawnAlien();
            SpawnAsteroids();
        }

        //infinite mode
        else if (mode == 3)
        {
            score = 0;
            lives = 3;
            wave = 1;

            scoreText.text = "Score:" + score;
            hiscoreText.text = "Highest: " + hiscore;
            livesText.text = "";
            waveText.text = "Wave #" + wave;
            gameover.text = "";
            pressx.text = "";
            pressz.text = "";

            repositionShip();
            SpawnAlien();
            SpawnAsteroids();
        }



    }

    //if (mode == 1)
    //    {

    //    } else if (mode == 2)
    //    {

    //    } else if (mode == 3)
    //    {

    //    }
    void SpawnAlien()
    {
        //Mode 1: regular
        if (mode == 1)
        {
            DestroyAliens();
            if (wave > 0)
            {
                int alienwave = wave;
                for (int i = 0; i < alienwave; i++)
                {
                    Invoke("randtimealien", Random.Range(1f, 3f));
                }
            }
        }
        //Mode 2: 1 extra alien
        else if (mode == 2)
        {
            DestroyAliens();
            if (wave > 0)
            {
                int alienwave = wave + 1;
                for (int i = 0; i < alienwave; i++)
                {
                    Invoke("randtimealien", Random.Range(1f, 3f));
                }
            }
        }
        //Mode 3: regular
        else if (mode == 3)
        {
            DestroyAliens();
            if (wave > 0)
            {
                int alienwave = wave;
                for (int i = 0; i < alienwave; i++)
                {
                    Invoke("randtimealien", Random.Range(1f, 3f));
                }
            }
        }
        
    }

    //is not affected by mode
    void randtimealien()
    {
        Vector3 pos;
        bool cleared = false;
        while (!cleared)
        {
            float x = Random.Range(-9.0f, 9.0f);
            float y = Random.Range(-5.0f, 5.0f);
            float z = 0f;
            pos = new Vector3(x, y, z);
            if (canSpawnalien(pos))
            {
                Instantiate(alienship, pos, Quaternion.Euler(0, 0, 0));
                cleared = true;
                break;
            }

        }
    }

    void SpawnAsteroids()
    {
        //Mode 1: regular
        if (mode == 1)
        {
            DestroyExistingAsteroids();
            asteroidsRemaining = (wave * increaseEachWave);
            for (int i = 0; i < asteroidsRemaining; i++)
            {
                Vector3 pos;
                bool cleared = false;
                // Spawn an asteroid
                while (!cleared)
                {
                    float x = Random.Range(-9.0f, 9.0f);
                    float y = Random.Range(-5.0f, 5.0f);
                    float z = 0f;
                    pos = new Vector3(x, y, z);
                    if (canSpawn(pos))
                    {
                        Instantiate(asteroid, pos, Quaternion.Euler(0, 0, 0));
                        cleared = true;
                        break;
                    }
                }
            }
            waveText.text = "Wave #" + wave;
        } 

        //Mode 2: asteroids = wave * increasefactor + 2
        else if (mode == 2)
        {
            DestroyExistingAsteroids();
            asteroidsRemaining = (wave * increaseEachWave + 2);
            for (int i = 0; i < asteroidsRemaining; i++)
            {
                Vector3 pos;
                bool cleared = false;
                // Spawn an asteroid
                while (!cleared)
                {
                    float x = Random.Range(-9.0f, 9.0f);
                    float y = Random.Range(-5.0f, 5.0f);
                    float z = 0f;
                    pos = new Vector3(x, y, z);
                    if (canSpawn(pos))
                    {
                        Instantiate(asteroid, pos, Quaternion.Euler(0, 0, 0));
                        cleared = true;
                        break;
                    }
                }
            }
            waveText.text = "Wave #" + wave;
        } 
        //Mode 3: regular
        else if (mode == 3)
        {
            DestroyExistingAsteroids();
            asteroidsRemaining = (wave * increaseEachWave);
            for (int i = 0; i < asteroidsRemaining; i++)
            {
                Vector3 pos;
                bool cleared = false;
                // Spawn an asteroid
                while (!cleared)
                {
                    float x = Random.Range(-9.0f, 9.0f);
                    float y = Random.Range(-5.0f, 5.0f);
                    float z = 0f;
                    pos = new Vector3(x, y, z);
                    if (canSpawn(pos))
                    {
                        Instantiate(asteroid, pos, Quaternion.Euler(0, 0, 0));
                        cleared = true;
                        break;
                    }
                }
            }
            waveText.text = "Wave #" + wave;
        }
        

    }

    //is not affected by mode
    public bool canSpawn(Vector3 pos)
    {
        if ((pos.x > 3) || (pos.x < -3))
        {

                return true;
            
        }
        return false;
    }
    //is not affected by mode
    public bool canSpawnalien(Vector3 pos)
    {
        if ((pos.x > 8.5) || (pos.x < -8.5))
        {

            
                return true;
            
        }
        return false;
    }
    //is not affected by mode
    public void repositionShip()
    {
        Vector3 zeroed = new Vector3(0, 0, 0);
        ship.transform.position = zeroed;
        ship.transform.rotation = Quaternion.Euler(0, 0, 0);
        ship.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ship.GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }
    //is not affected by mode
    IEnumerator waitbeforeSpawn()
    {
        repositionShip();
        DestroyExistingAsteroids();
        DestroyAliens();
        yield return new WaitForSeconds(1);
        SpawnAsteroids();
        SpawnAlien();
        
        
    }

    //is not affected by mode
    IEnumerator wavetitle()
    {
        waveText.text = "";
        countdownText.text = "Wave #" + wave;
        yield return new WaitForSeconds(1);
        waveText.text = "Wave #" + wave;
        countdownText.text = "";



    }

    //is not affected by mode
    public void IncrementScore(int num)
    {

        if (asteroidsRemaining < 1)
        {
            asteroidsRemaining = 0;
            wave++;
            waveText.text = "Wave #" + wave;
            StartCoroutine("waitbeforeSpawn");
            StartCoroutine("wavetitle");


        }
        
        if (num == 1)
        {   
            score += 50;
        } else if (num == 2)
        {
            score += 75;
        } else if (num == 3)
        {
            score += 100;

        } else if (num == 4) {
            score += 500;
        }

        

        scoreText.text = "Score: " + score;

        if (score > hiscore)
        {
            hiscore = score;
            hiscoreText.text = "Highest: " + hiscore;
            PlayerPrefs.SetInt("Highest", hiscore);
        }
        
    }

    public void DecrementLives()
    {
        //Mode 1: regular
        if (mode == 1)
        {
            lives--;
            livesText.text = "Lives: " + lives;


            if (lives < 1)
            {

                scoreText.text = "";
                livesText.text = "";
                waveText.text = "";
                DestroyExistingAsteroids();
                DestroyAliens();
                StartCoroutine(playagain());


            }
        }
        //Mode 2: regular
        else if (mode == 2)
        {
            lives--;
            livesText.text = "Lives: " + lives;


            if (lives < 1)
            {

                scoreText.text = "";
                livesText.text = "";
                waveText.text = "";
                DestroyExistingAsteroids();
                DestroyAliens();
                StartCoroutine(playagain());


            }
        }
        else if (mode == 3)
        {
            lives++;
        }

    }

    IEnumerator playagain()
    {
        
        yield return new WaitForSeconds(1);
        gameover.text = "GAME OVER";
        pressx.text = "PRESS X TO PLAY AGAIN";
        pressz.text = "PRESS Z TO EXIT";




    }

    public void DecrementAsteroids()
    {
        asteroidsRemaining--;
    }

    public void SplitAsteroid()
    {
        asteroidsRemaining += 1;

    }
    void DestroyAliens()
    {
        GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
        foreach (GameObject alien in aliens)
        {
            GameObject.Destroy(alien);
        }

        GameObject[] ab = GameObject.FindGameObjectsWithTag("Alien Bullet");
        foreach (GameObject alien in ab)
        {
            GameObject.Destroy(alien);
        }

        
    }
    void DestroyExistingAsteroids()
    {
        GameObject[] asteroids =
            GameObject.FindGameObjectsWithTag("Large Asteroid");

        foreach (GameObject current in asteroids)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids2 =
            GameObject.FindGameObjectsWithTag("Medium Asteroid");

        foreach (GameObject current in asteroids2)
        {
            GameObject.Destroy(current);
        }

        GameObject[] asteroids3 =
            GameObject.FindGameObjectsWithTag("Small Asteroid");

        foreach (GameObject current in asteroids3)
        {
            GameObject.Destroy(current);
        }
    }

}