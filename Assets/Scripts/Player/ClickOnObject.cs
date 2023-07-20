using API;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using Zenject;

namespace Player
{
    public class ClickOnObject : MonoBehaviour
    {
        [Inject] private IGameInput _gameInput;
        [Inject] private Camera _camera;

        [SerializeField] private LayerMask _objectLayerMask;
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _floatingTextPrefab;
        
        private PlayerInputAction _inputAction;
        
        private void Awake()
        {
            _inputAction = new PlayerInputAction();
        }

        public void Start()
        {
            _camera = Camera.main;
            _gameInput.OnClick += Clicked;
        }

        private void OnEnable() => _inputAction.Enable();
        private void OnDisable() => _inputAction.Disable();

    
        private void Clicked()
        {
            var mousePosition = _gameInput.MousePosition;
            Ray ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit, 100, _objectLayerMask))
            {
                var fruitInstance = hit.collider.GetComponent<FruitItem>();

                if (fruitInstance == null) return;
                if (!fruitInstance.IsRequired || _player.IsPicking) return;
                
                _player.Anim(fruitInstance);
                ShowPlusPoint(fruitInstance.transform.position);
            }
        }

        private void ShowPlusPoint(Vector3 position)
        {
            var plusOneText = Instantiate(_floatingTextPrefab, position, Quaternion.identity, transform);

            var text = plusOneText.GetComponentInChildren<TextMeshPro>();

            plusOneText.transform.DOMoveY(position.y + 1f, 1f).OnComplete(() => Destroy(plusOneText.gameObject));
            text.DOFade(0f, 1f).SetDelay(0.5f);
        }
    }
}