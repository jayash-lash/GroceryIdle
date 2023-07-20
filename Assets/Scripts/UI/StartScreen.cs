using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
   public class StartScreen : MonoBehaviour
   {
      [SerializeField] private Button _startButton;

      private void Awake()
      {
         _startButton.onClick.AddListener(StartGame);
      }

      public void StartGame()
      {
         SceneManager.LoadScene("GameScene");
      }
   }
}
