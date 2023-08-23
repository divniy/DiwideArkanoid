using UnityEngine;
using UnityEngine.SceneManagement;

namespace Diwide.Arkanoid.UI
{
    public class MainMenu : MonoBehaviour
    {
        public virtual void OnPlayGame()
        {
            SceneManager.LoadScene("MainScene");
        }
        
        public virtual void OnExitGame()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
            Debug.Log("Exit game");
            Application.Quit();
        }
    }
}