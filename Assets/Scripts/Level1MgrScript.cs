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
    float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = FindObjectOfType<TimerScript>();
        spawnTime = 5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
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
        SceneManager.LoadScene(2);
    }

    public void playerDeath()
    {
        SceneManager.LoadScene("EndScene");
    }
}
