using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    private GameObject tempCoin;
    public float rateSpawn;
    public float currentTime;
    public bool spawned;

    // Despertado é chamado quando a instância do script for carregada
    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (tempCoin == null)
        {
            spawned = false;
        }
        if (currentTime >= rateSpawn && spawned == false)
        {
            currentTime = 0;
            tempCoin = Instantiate(coinPrefab) as GameObject;
            tempCoin.transform.position = new Vector3(transform.position.x, transform.position.y, tempCoin.transform.position.z);
            spawned = true;
            rateSpawn = Random.Range(30, 120);
        }
    }
}
