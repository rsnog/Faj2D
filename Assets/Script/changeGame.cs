using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class changeGame : MonoBehaviour
{
    public Text coinsText;
    public Text recordText;
    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        coinsText.text = PlayerPrefs.GetInt("scorePoints").ToString();
        recordText.text = "Record - "+PlayerPrefs.GetInt("Record").ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "GameOver")
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                SceneManager.LoadScene("Game");
            }
        }
    }
}
