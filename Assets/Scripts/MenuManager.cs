using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{


    public Camera cam;
    public static int menumode;
    public Text pressX;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void backbutton()
    {
        
            SceneManager.LoadScene("Menu");
    }
    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("salmon"))
        {

            cam.backgroundColor = new Color(255f / 255f, 114f / 255f, 114f / 255f);
        }
        else if (PlayerPrefs.GetString("backgroundColor").ToLower().Contains("sky"))
        {


            cam.backgroundColor = new Color(135f / 255f, 206f / 255f, 235f / 255f);
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

        if (Input.GetKeyDown("x"))
        {
            SceneManager.LoadScene("ModeScene");
        }
           

        if (Input.GetKeyDown("z"))
        {
            SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown("o"))
        {
            SceneManager.LoadScene("Options");
        }

        if (Input.GetKeyDown("c"))
        {
            SceneManager.LoadScene("Credits");
        }


        if (Input.GetKeyDown("h"))
        {
            SceneManager.LoadScene("HowToPlay");
        }

        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("GameScene");
            menumode = 1;

        }

        if (Input.GetKeyDown("2"))
        {
            SceneManager.LoadScene("GameScene");
            menumode = 2;
            
        }

        if (Input.GetKeyDown("3"))
        {
            SceneManager.LoadScene("GameScene");
            menumode = 3;
            
        }

    }

    IEnumerator displaytext()
    {

        const float waitTime = 0.5f;
        float counter = 0f;

        pressX.text = "";
        while (counter < waitTime)
        {
            counter += Time.deltaTime;
            yield return null;
        }
        pressX.text = "PRESS X TO START";
       

    }
}
