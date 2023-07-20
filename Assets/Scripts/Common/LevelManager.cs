using API;
using Factory;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Common
{
    public class LevelManager : MonoBehaviour
    {
        [Inject] private IFruitsConfigsRepository _repository;
        [Inject] private FruitsSpawner _fruitsSpawner;
        [Inject] private Camera _camera;
        [Inject] private GameInput _gameInput;
    
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _task;

        [SerializeField] private GameObject _player;
        [SerializeField] private Rig _rig;
        [SerializeField] private GameObject _basket;
        [SerializeField] private GameObject _conveyour;

        private Animator _playerAnimator;
        private bool _isLevelComplete;
    
        private void Start()
        {
            _exitButton.onClick.AddListener(Exit);
            _restartButton.onClick.AddListener(NextLevel);
        
            _playerAnimator = _player.GetComponent<Animator>();
        }

        private void Update()
        {
            if (!_isLevelComplete && _repository.FruitTask.CollectedCount == _repository.FruitTask.FreeSlots)
            {
                _isLevelComplete = true;
                _gameInput.DisableInput();
                
                _rig.weight = 0f;
                _playerAnimator.SetBool("PlayDance", true);
                _fruitsSpawner.SwitchOffSpawner();
                _task.SetActive(false);
                _winScreen.SetActive(true);
                _basket.SetActive(false);
            }
            
            if (_isLevelComplete)
            {
                _conveyour.transform.position = Vector3.Lerp(_conveyour.transform.position, new Vector3(0f, -1f, 0f), Time.deltaTime * 1f);
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, new Vector3(-0.15f, 2.3f, 2f), Time.deltaTime * 1.2f);
            }
        }
        
        public void NextLevel()
        {
            SceneManager.LoadScene("GameScene");
        }

        private void Exit() => Application.Quit();
    }
}