using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrMMButtons : MonoBehaviour {

    public string GameSceneName = "MainGameplay";

    public void BtnStartGame()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}
