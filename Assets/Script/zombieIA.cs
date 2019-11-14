using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieIA : MonoBehaviour
{
    int isWalkingParam = Animator.StringToHash("isWalking");
    int deadParam = Animator.StringToHash("dead");

    private GameController _GameController;
    private Rigidbody2D zombieRb;
    private Animator zombieAnimator;
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
        zombieRb = GetComponent<Rigidbody2D>();
        zombieAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("zombieWalk");
        zombieWalk();
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal > 0 && isLookLeft == true)
        {
            flip();
        }
        else if (horizontal < 0 && isLookLeft == false)
        {
            flip();
        }
        zombieRb.velocity = new Vector2(horizontal * speed, zombieRb.velocity.y);
        if (horizontal != 0)
        {
            zombieAnimator.SetBool(isWalkingParam, true);
        }
        else
        {
            zombieAnimator.SetBool(isWalkingParam, false);
        }

        if (step >= timeWalk + 100)
        {
            zombieWalk();
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
        switch (collision.gameObject.tag)
        {
            case "hitBox":
                horizontal = 0;
                //StopCoroutine("zombieWalk");
                Destroy(hitBox);
                _GameController.playSFX(_GameController.sfxEnemyDie, 1f);
                zombieAnimator.SetTrigger(deadParam);
                _GameController.scorePoints += 1;
                _GameController.getCoin();
                break;
            case "Finish":
                if (horizontal > 0)
                {
                    horizontal = -1;
                }
                else
                {
                    horizontal = 1;
                }
                break;
        }
    }

    void onDead()
    {
        GameObject temp = Instantiate(hitPrefab, transform.position, transform.localRotation);
        Destroy(temp.gameObject, 0.5f);
        Destroy(this.gameObject);
    }

    void zombieWalk()
    {
        //int rand = Random.Range(0, 100);
        //if (rand < 33)
        //{
        //    horizontal = -1;
        //}
        //else if (rand < 66)
        //{
        //    horizontal = 1;
        //}
        //else if (rand < 100)
        //{
        //    horizontal = 1;
        //}
        if (horizontal > 0)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 1;
        }
        //yield return new WaitForSeconds(timeWalk);
        //StartCoroutine("zombieWalk");
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
