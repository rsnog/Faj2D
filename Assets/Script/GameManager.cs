using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager uniqueIntance = null;
    public static GameManager GetInstance() { return uniqueIntance; }
    private FGameState gameState = new FGameState();

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        if (uniqueIntance == null)
        {
            uniqueIntance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void SetMenu(bool value)
    {
        gameState.bOnMenu = value;
    }

    public void SetDeath(bool value)
    {
        gameState.bIsDeath = value;
    }

    public void SetPlaying(bool value)
    {
        gameState.bIsPlaying = value;
    }

    public void SetExit(bool value)
    {
        gameState.bIsExiting = value;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetButtonDown("Jump"))
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "GameOver")
            {
                gameState.bOnMenu = true;
            }
            else
            {
                gameState.bIsPlaying = true;
            }
        }
        gameState.OnUpdate();
    }
}
