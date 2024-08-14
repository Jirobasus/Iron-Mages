using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public SceneController sceneController;
    public Canvas deathMenu;

    public Image image;
    public bool coolDown;
    public bool isDeathScreenLoaded = false;
    public AudioSource arenaSoundtrack;
    public AudioClip[] dieAudioClips;
    public TextMeshProUGUI survivedText;

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int wave);
    void Start()
    {
        coolDown = false;
        image = GetComponent<Image>();
        sceneController = FindObjectOfType<SceneController>();
    }

    private void Update()
    {
        if (image.fillAmount < 0.2f && !isDeathScreenLoaded)
        {
            Death();
        }
    }

    public void ChangeHealth(float damage)
    {
        if (!coolDown)
        {
            StartCoroutine(ReduceHealthBar(damage));
        }
    }

    public IEnumerator ReduceHealthBar(float damage)
    {
        coolDown = true;
        image.fillAmount -= damage;
        yield return new WaitForSeconds(1);
        coolDown = false;
    }

    private void Death()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(dieAudioClips, transform, 0.2f);
        sceneController.isPaused = true;
        arenaSoundtrack.Pause();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        deathMenu.gameObject.SetActive(true);
        survivedText.text = "You survived\r\n" + sceneController.currentWave + " Waves";
        Time.timeScale = 0;
        isDeathScreenLoaded = true;
        sceneController.isInGame = false;
    }

    public void ResetCooldown()
    {
        StopCoroutine(ReduceHealthBar(0.2f));
        coolDown = false;
    }
}
