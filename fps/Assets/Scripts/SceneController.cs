using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healthPowerUpPrefab;
    [SerializeField] private GameObject shieldPowerUpPrefab;
    [SerializeField] private GameObject ammoPowerUpPrefab;

    public PlatformSwitcher platformSwitcher;
    public TextMeshProUGUI waveCountTextMeshPro;
    public TextMeshProUGUI breakTextMeshPro;
    public TextMeshProUGUI breakCountdownTextMeshPro;
    public TextMeshProUGUI mobileWaveCountTextMeshPro;
    public TextMeshProUGUI mobileBreakTextMeshPro;
    public TextMeshProUGUI mobileBreakCountdownTextMeshPro;
    public GameObject _enemy;
    public GameObject _healthPowerup;
    public GameObject _shiedlPowerup;
    public GameObject _ammoPowerup;
    public Material[] materials;
    public ParticleSystem hitExplosion;
    public List<GameObject> enemies;
    public List<GameObject> healthPowerUps;
    public List <GameObject> ammoPowerUps;
    public GameObject enemySpawnPointsParent;
    public GameObject healthSpawnPointsParent;
    public GameObject shieldSpawnPointsParent;
    public GameObject ammoSpawnPointsParent;
    public int currentMaxEnemies;
    public int currentWave;
    public float waveTextResizeSpeed = 1f;
    public float maxTextScale = 2.5f;
    public float initialScale = 1.5f;
    public float mobileMaxTextScale = 2f;
    public float mobileInitialScale = 1f;
    public Canvas pauseMenu;
    public Canvas UI;
    public Canvas mobileUI;
    public Canvas joystickCanvas;
    public bool isPaused;
    public AudioSource arenaSoundtrack;
    public Camera _camera;
    public bool isInGame;
    public Health health;
    public Shield shield;

    private GameObject[] enemySpawnPoints;
    private GameObject[] healthSpawnPoints;
    private GameObject[] shieldSpawnPoints;
    private GameObject[] ammoSpawnPoints;
    private int firstPreviousEnemySpawnPoint;
    private int secondPreviousEnemySpawnPoint;
    private int previousHealthSpawnPoint;
    private int previousShieldSpawnPoint;
    private int previousAmmoSpawnPoint;
    private int currentEnemySpawnPoint;
    private int currentHealthSpawnPoint;
    private int currentShieldSpawnPoint;
    private int currentAmmoSpawnPoint;
    private bool waveFinished;
    private bool isBreak;
    private int countdown;
    //private bool waveChanged;

    private void Start()
    {
        platformSwitcher = FindObjectOfType<PlatformSwitcher>();
        isPaused = false;
        isBreak = false;
        waveFinished = true;
        isInGame = true;
        //waveChanged = false;
        countdown = 30;
        currentWave = 1;
        StartCoroutine(WaveChange());
        firstPreviousEnemySpawnPoint = 0;
        secondPreviousEnemySpawnPoint = 1;
        previousHealthSpawnPoint = 0;
        previousShieldSpawnPoint = 0;
        previousAmmoSpawnPoint = 0;
        materials = new Material[7];
        enemySpawnPoints = new GameObject[24];
        healthSpawnPoints = new GameObject[19];
        shieldSpawnPoints = new GameObject[4];
        ammoSpawnPoints = new GameObject[10];
        for (int i = 1; i < 8; i++)
        {
            string materialName = "Skin" + i.ToString();
            //string materialName = materialsFolderPath + "Skin" + i.ToString() + ".mat";
            materials[i - 1] = Resources.Load<Material>(materialName);
        }
        for (int i = 0; i < 24; i++)
        {
            enemySpawnPoints[i] = enemySpawnPointsParent.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < 19; i++)
        {
            healthSpawnPoints[i] = healthSpawnPointsParent.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < 4; i++)
        {
            shieldSpawnPoints[i] = shieldSpawnPointsParent.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < 10; i++)
        {
            ammoSpawnPoints[i] = ammoSpawnPointsParent.transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        if (currentWave > 5 && currentWave % 5 == 1 && !isBreak)
        {
            waveFinished = false;
            isBreak = true;
            StartCoroutine(Break());
        }

        if (enemies.Count < currentMaxEnemies && waveFinished && !isBreak)
        {
            do
            {
                currentEnemySpawnPoint = Random.Range(0, 24);
            } while (currentEnemySpawnPoint == firstPreviousEnemySpawnPoint || currentEnemySpawnPoint == secondPreviousEnemySpawnPoint);

            _enemy = Instantiate(enemyPrefab) as GameObject;
            enemies.Add(_enemy);
            int skinVariant = Random.Range(0, 7);
            _enemy.GetComponent<Renderer>().material = materials[skinVariant];
            _enemy.transform.position = enemySpawnPoints[currentEnemySpawnPoint].transform.position;
            firstPreviousEnemySpawnPoint = secondPreviousEnemySpawnPoint;
            secondPreviousEnemySpawnPoint = currentEnemySpawnPoint;
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
        }

        if (!isBreak && enemies.Count == 0)
        {
            currentWave++;
            waveFinished = true;
            currentMaxEnemies++;
            StartCoroutine(WaveChange());

        }
        else if (enemies.Count == currentMaxEnemies)
        {
            waveFinished = false;
        }

        if (healthPowerUps.Count < 2)
        {
            do
            {
                currentHealthSpawnPoint = Random.Range(0, 19);
            } while (currentHealthSpawnPoint == previousHealthSpawnPoint);
            previousShieldSpawnPoint = currentHealthSpawnPoint;
            _healthPowerup = Instantiate(healthPowerUpPrefab) as GameObject;
            healthPowerUps.Add(_healthPowerup);
            _healthPowerup.transform.position = healthSpawnPoints[currentHealthSpawnPoint].transform.position;
        }

        if (ammoPowerUps.Count < 2)
        {
            do
            {
                currentAmmoSpawnPoint = Random.Range(0, 10);
            } while (currentAmmoSpawnPoint == previousAmmoSpawnPoint);
            previousAmmoSpawnPoint = currentAmmoSpawnPoint;
            _ammoPowerup = Instantiate(ammoPowerUpPrefab) as GameObject;
            ammoPowerUps.Add(_ammoPowerup);
            _ammoPowerup.transform.position = ammoSpawnPoints[currentAmmoSpawnPoint].transform.position;
        }

        if (_shiedlPowerup == null)
        {
            do
            {
                currentShieldSpawnPoint = Random.Range(0, 4);
            } while (currentShieldSpawnPoint == previousShieldSpawnPoint);
            previousShieldSpawnPoint = currentShieldSpawnPoint;
            _shiedlPowerup = Instantiate(shieldPowerUpPrefab) as GameObject;
            _shiedlPowerup.transform.position = shieldSpawnPoints[currentShieldSpawnPoint].transform.position;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private IEnumerator WaveChange()
    {
        waveCountTextMeshPro.gameObject.SetActive(true);
        if (currentWave < 5 || currentWave % 5 != 1)
        {
            waveCountTextMeshPro.text = "Wave " + currentWave;
        }
        else
        {
            waveCountTextMeshPro.text = "Break!";
        }
        while (waveCountTextMeshPro.transform.localScale.x < maxTextScale)
        {
            waveCountTextMeshPro.transform.localScale += Vector3.one * waveTextResizeSpeed * Time.deltaTime;
            yield return null;
        }
        while (waveCountTextMeshPro.transform.localScale.x > initialScale)
        {
            waveCountTextMeshPro.transform.localScale -= Vector3.one * waveTextResizeSpeed * Time.deltaTime;
            yield return null;
        }
        waveCountTextMeshPro.transform.localScale = Vector3.one * initialScale;
        waveCountTextMeshPro.gameObject.SetActive(false);
        /*
        if (platformSwitcher.isDesktop)
        {
        }
        else
        {
            mobileWaveCountTextMeshPro.gameObject.SetActive(true);
            if (currentWave < 5 || currentWave % 5 != 1)
            {
                mobileWaveCountTextMeshPro.text = "Волна" + currentWave;
            }
            else
            {
                mobileWaveCountTextMeshPro.text = "Перерыв!";
            }
            while (mobileWaveCountTextMeshPro.transform.localScale.x < mobileMaxTextScale)
            {
                mobileWaveCountTextMeshPro.transform.localScale += Vector3.one * waveTextResizeSpeed * Time.deltaTime;
                yield return null;
            }
            while (mobileWaveCountTextMeshPro.transform.localScale.x > mobileInitialScale)
            {
                mobileWaveCountTextMeshPro.transform.localScale -= Vector3.one * waveTextResizeSpeed * Time.deltaTime;
                yield return null;
            }
            mobileWaveCountTextMeshPro.transform.localScale = Vector3.one * mobileInitialScale;
            mobileWaveCountTextMeshPro.gameObject.SetActive(false);
        }
        */
    }

    public void DeleteHitMark(GameObject hitMarkInstance, Vector3 pos, Quaternion rotation)
    {
        StartCoroutine(DeleteHitMarkCoroutine(hitMarkInstance, pos, rotation));
    }

    public IEnumerator DeleteHitMarkCoroutine(GameObject hitMarkInstance, Vector3 pos, Quaternion rotation)
    {
        hitExplosion.transform.position = pos;
        hitExplosion.transform.rotation = rotation;
        hitExplosion.Play();
        yield return new WaitForSeconds(2f);
        Destroy(hitMarkInstance);
    }

    private IEnumerator Break()
    {
        breakTextMeshPro.gameObject.SetActive(true);
        breakCountdownTextMeshPro.gameObject.SetActive(true);
        while (countdown > 0)
        {
            if (countdown > 9)
            {
                breakCountdownTextMeshPro.text = "0:" + countdown;
            }
            else
            {
                breakCountdownTextMeshPro.text = "0:0" + countdown;
            }
            countdown--;
            yield return new WaitForSeconds(1);
        }
        countdown = 30;
        breakTextMeshPro.gameObject.SetActive(false);
        breakCountdownTextMeshPro.gameObject.SetActive(false);
        /*
        if (platformSwitcher.isDesktop)
        {
            
        }
        else
        {
            mobileBreakTextMeshPro.gameObject.SetActive(true);
            mobileBreakCountdownTextMeshPro.gameObject.SetActive(true);
            while (countdown > 0)
            {
                if (countdown > 9)
                {
                    mobileBreakCountdownTextMeshPro.text = "0:" + countdown;
                }
                else
                {
                    mobileBreakCountdownTextMeshPro.text = "0:0" + countdown;
                }
                countdown--;
                yield return new WaitForSeconds(1);
            }
            mobileBreakTextMeshPro.gameObject.SetActive(false);
            mobileBreakCountdownTextMeshPro.gameObject.SetActive(false);
        }
        */
        waveFinished = true;
        currentWave++;
        isBreak = false;
        StartCoroutine(WaveChange());
    }

    public void PauseGame()
    {
        if (isInGame)
        {
            arenaSoundtrack.Pause();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            isPaused = true;
            pauseMenu.gameObject.SetActive(true);
            UI.gameObject.SetActive(false);
            mobileUI.gameObject.SetActive(false);
            joystickCanvas.gameObject.SetActive(false);
            Time.timeScale = 0;
            health.ResetCooldown();
            shield.ResetCooldown();
        }  
    }
}
