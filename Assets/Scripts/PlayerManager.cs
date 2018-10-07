using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    public int lifeValue = 3;
    public int playerScore = 0;
    public bool isDead;
    public bool isDefeat;

    public GameObject born;
    public Text PlayerScoreText;
    public Text PlayerLifeValueText;
    public GameObject isDefeatUI;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }
        if (isDead)
        {
            Recover();
        }
        PlayerScoreText.text = playerScore.ToString();
        PlayerLifeValueText.text = lifeValue.ToString();

	}

    private void Recover()
    {
        if (lifeValue <= 0)
        {
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createpPlayer = true;
            isDead = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
