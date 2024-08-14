using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void PlayButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Arena");
    }
}
