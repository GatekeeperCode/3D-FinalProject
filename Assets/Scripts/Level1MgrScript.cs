using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1MgrScript : MonoBehaviour
{
    private TimerScript _timer;
    private bool paused;
    public GameObject pauseMenu;
    public GameObject _chaseEnemyPrefab;

    [SerializeField]
    private List<Transform> _spawnPoints;
    float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(holdLook());

        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = FindObjectOfType<TimerScript>();
        spawnTime = 5f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pause();
            }
            else
            {
                unpause();
            }
        }
        print(spawnTime);
        if (spawnTime < 0) {
            //StartCoroutine(SpawnEnemies());
            int i = Random.Range(0, _spawnPoints.Count - 1);
            Vector3 sourcePosition = _spawnPoints[i].transform.position;
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            /* UnityEngine.AI.NavMeshHit closestHit;
             if (UnityEngine.AI.NavMesh.SamplePosition(sourcePosition, out closestHit, 500, 1))
             {
                 chaseEnemy.transform.position = closestHit.position;
                 chaseEnemy.AddComponent<UnityEngine.AI.NavMeshAgent>();
                 //TODO
             }
             else
             {
                 Debug.Log("...");
             }*/
            spawnTime = 5f;
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }
    }
    /*
    IEnumerator SpawnEnemies()
    {
        if (_timer._elapsedTime < 15)
        {
           

        }
        else if (_timer._elapsedTime < 20)
        {
            int i = Random.Range(0, _spawnPoints.Count - 1);
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            spawnTime = 4;
        }
        else if (_timer._elapsedTime < 25)
        {
            int i = Random.Range(0, _spawnPoints.Count - 1);
            GameObject chaseEnemy = Instantiate(_chaseEnemyPrefab, _spawnPoints[i]);
            spawnTime = 3;
        }

    }*/

    void changeLevel()
    {
        PlayerPrefs.SetFloat("PlayerLevel1Time", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer.StopTimer();
        SceneManager.LoadScene(2);
    }

    //Pauses game and opens pause menu
    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        paused = true;
    }

    //Unpauses game and closes pause menu
    private void unpause()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void playerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator holdLook()
    {
        yield return new WaitForSeconds(8);
        Messenger.Broadcast("StartLook");
    }
}
