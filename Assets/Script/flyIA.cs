using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyIA : MonoBehaviour
{
    int isWalkingParam = Animator.StringToHash("isWalking");
    int deadParam = Animator.StringToHash("dead");

    private GameController _GameController;
    private Rigidbody2D flyRb;
    private Animator flyAnimator;
    public Transform flyTransform;
    public Transform LimitCamRigth;
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
        flyRb = GetComponent<Rigidbody2D>();
        flyAnimator = GetComponent<Animator>();
        flyTransform = GetComponent<Transform>();
        LimitCamRigth = _GameController.LimitCamRigth;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("flyWalk");
        flyWalk();
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
        flyRb.velocity = new Vector2(horizontal * speed, flyRb.velocity.y);
        if (horizontal != 0)
        {
            flyAnimator.SetBool(isWalkingParam, true);
        }
        else
        {
            flyAnimator.SetBool(isWalkingParam, false);
        }

        if (step >= timeWalk + 100)
        {
            flyWalk();
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
        Debug.Log("fly: " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "hitBox":
                horizontal = 0;
                //StopCoroutine("flyWalk");
                Destroy(hitBox);
                _GameController.playSFX(_GameController.sfxEnemyDie, 1f);
                flyAnimator.SetTrigger(deadParam);
                _GameController.scorePoints += 1;
                _GameController.getCoin();
                break;
        }
    }

    void onDead()
    {
        GameObject temp = Instantiate(hitPrefab, transform.position, transform.localRotation);
        Destroy(temp.gameObject, 0.5f);
        Destroy(this.gameObject);
    }

    void flyWalk()
    {
        int rand = Random.Range(0, 100);
        if (rand < 33)
        {
            horizontal = -1;
        }
        else if (rand < 66)
        {
            horizontal = 1;
        }
        else if (rand < 100)
        {
            horizontal = 1;
        }

        if (flyTransform.position.x + 5 > LimitCamRigth.transform.position.x - 5)
        {
            horizontal = -1;
        }
        if (flyTransform.position.x - 5 < 0)
        {
            horizontal = 1;
        }
        //yield return new WaitForSeconds(timeWalk);
        //StartCoroutine("flyWalk");
    }

    void flip()
    {
        isLookLeft = !isLookLeft;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }
}
