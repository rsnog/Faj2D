using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private Camera cam;
    public Transform playerTransform;
    public Transform LimitCamLeft, LimitCamRigth, LimitCamDown, LimitCamUp;
    public float speedCam;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioClip sfxJump;
    public AudioClip sfxAttack;
    public AudioClip sfxHited;
    public AudioClip sfxCoin;
    public AudioClip sfxEnemyDie;
    public AudioClip sfxDie;
    public AudioClip[] sfxStep;

    public int scorePoints;
    public int life;
    public Text coinsText;
    public Image[] hearts;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        cam = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        heartController();
        //PlayerPrefs.SetInt("scorePoints", 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // LateUpdate será chamada a cada quadro, se o Comportamento estiver habilitado
    private void LateUpdate()
    {
        camController();
    }

    void camController()
    {
        float positionCamX = playerTransform.position.x;
        float positionCamy = playerTransform.position.y;
        if (cam.transform.position.x < LimitCamLeft.position.x && playerTransform.position.x < LimitCamLeft.position.x)
        {
            positionCamX = LimitCamLeft.position.x;
        }
        else if (cam.transform.position.x > LimitCamRigth.position.x && playerTransform.position.x > LimitCamRigth.position.x)
        {
            positionCamX = LimitCamRigth.position.x;
        }

        if (cam.transform.position.y < LimitCamDown.position.y && playerTransform.position.y < LimitCamDown.position.y)
        {
            positionCamy = LimitCamDown.position.y;
        }
        else if (cam.transform.position.y > LimitCamUp.position.y && playerTransform.position.y > LimitCamUp.position.y)
        {
            positionCamy = LimitCamUp.position.y;
        }

        Vector3 posCam = new Vector3(positionCamX, positionCamy, cam.transform.position.z);
        cam.transform.position = Vector3.Lerp(cam.transform.position, posCam, speedCam * Time.deltaTime);
    }

    public void playSFX(AudioClip sfxClip, float volume)
    {
        sfxSource.PlayOneShot(sfxClip, volume);
    }

    public void heartController()
    {
        foreach (Image image in hearts)
        {
            image.enabled = false;
        }
        for (int i = 0; i < life; i++)
        {
            hearts[i].enabled = true;
        }
    }

    public void getCoin()
    {
        playSFX(sfxCoin, 1f);
        scorePoints += 1;
        coinsText.text = scorePoints.ToString();
    }

    public void getHit()
    {
        life -= 1;
        heartController();
        if (life <= 0)
        {
            PlayerPrefs.SetInt("scorePoints", scorePoints);
            if (scorePoints>PlayerPrefs.GetInt("Record"))
            {
                PlayerPrefs.SetInt("Record", scorePoints);
            }
            playSFX(sfxDie, 1f);
            playerTransform.gameObject.SetActive(false);
            //SceneManager.LoadScene("GameOver");
            GameManager.GetInstance().SetPlaying(false);
            GameManager.GetInstance().SetDeath(true);
        }
    }
}
