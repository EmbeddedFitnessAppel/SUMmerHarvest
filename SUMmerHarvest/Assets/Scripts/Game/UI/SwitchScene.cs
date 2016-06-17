using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SwitchScene : MonoBehaviour {

    public void SwitchSceneToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
