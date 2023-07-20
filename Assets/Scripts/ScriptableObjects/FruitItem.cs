using UnityEngine;

namespace ScriptableObjects
{
    public class FruitItem : MonoBehaviour
    { 
        public ItemData Config { get; private set; }
        public bool IsRequired { get; private set; }
        public bool IsPickedUp { get; private set; } 

        [SerializeField] private Vector3 _directionOfMovement;
        [SerializeField] private float _speed;
        [SerializeField] private Collider _collider;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider>(); //i lazzy ass
        }

        private void Update()
        {
            if(!IsPickedUp)
                _transform.position += _directionOfMovement * (_speed * Time.deltaTime);
        } 
        public void Initialize(ItemData config, bool isRequired)
        {
            Config = config;
            IsRequired = isRequired;
        }

        public void PickUp()
        {
            IsPickedUp = true;
            _collider.enabled = false;
        }
        
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}