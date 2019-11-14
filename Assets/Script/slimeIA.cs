using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeIA : MonoBehaviour
{
    int isWalkingParam = Animator.StringToHash("isWalking");
    int deadParam = Animator.StringToHash("dead");

    private GameController _GameController;
    private Rigidbody2D slimeRb;
    private Animator slimeAnimator;
    public GameObject hitPrefab;
    public bool isLookLeft;
    public float speed;
    public float timeWalk;
    public GameObject hitBox;
    private int horizontal;
    private int step = 0;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        slimeRb = GetComponent<Rigidbody2D>();
        slimeAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("slimeWalk");
        slimeWalk();
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal > 0 && isLookLeft == false)
        {
            flip();
        }
        else if (horizontal < 0 && isLookLeft == true)
        {
            flip();
        }
        slimeRb.velocity = new Vector2(horizontal * speed, slimeRb.velocity.y);
        if (horizontal != 0)
        {
            slimeAnimator.SetBool(isWalkingParam, true);
        }
        else
        {
            slimeAnimator.SetBool(isWalkingParam, false);
        }

        if (step >= timeWalk + 100)
        {
            slimeWalk();
            step = 0;
        }
        else
        {
            step++;
        }
    }

    // OnTriggerEnter2D é chamado quando outro Collider2D entra no gatilho (somente física de 2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SLIME: " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "hitBox":
                horizontal = 0;
                //StopCoroutine("slimeWalk");
                Destroy(hitBox);
                _GameController.playSFX(_GameController.sfxEnemyDie, 1f);
                slimeAnimator.SetTrigger(deadParam);
                _GameController.scorePoints += 1;
                _GameController.getCoin();
                break;
        }
    }

    void onDead()
    {
        Destroy(this.gameObject);
    }

    void slimeWalk()
    {
        if (horizontal > 0)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 1;
        }
        //yield return new WaitForSeconds(timeWalk);
        //StartCoroutine("slimeWalk");
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
