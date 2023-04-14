using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1MgrScript : MonoBehaviour
{
    private TimerScript _timer;
    public GameObject _chaseEnemyPrefab;

    [SerializeField]
    private List<Transform> _spawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = GameObject.FindObjectOfType<TimerScript>();
        StartCoroutine(SpawnEnemies());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    IEnumerator SpawnEnemies()
    {
        if (_timer._elapsedTime < 30)
        {
            int i = Random.Range(0, _spawnPoints.Count - 1);
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            yield return new WaitForSeconds(10);
        }
        else if (_timer._elapsedTime < 60)
        {
            int i = Random.Range(0, _spawnPoints.Count - 1);
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            yield return new WaitForSeconds(8);
        }
        else if (_timer._elapsedTime < 90)
        {
            int i = Random.Range(0, _spawnPoints.Count - 1);
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            yield return new WaitForSeconds(5);
        }
    }

    void changeLevel()
    {
        PlayerPrefs.SetFloat("PlayerLevel1Time", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene(2);
    }

    public void playerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }
}
