using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public GameObject playerPrefab;

    public GameObject[] enemyPrefabList;

    public bool createpPlayer;

	// Use this for initialization
	void Start () {
        //延时调用
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void BornTank()
    {
        if (createpPlayer)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            int num = Random.Range(0, 2);
            Instantiate(enemyPrefabList[num], transform.position, Quaternion.identity);
        }
    }
}
