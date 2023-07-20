using Factory;
using UnityEngine;
using Zenject;

namespace Common
{
    public class FruitsSpawner : MonoBehaviour
    {
        [Inject] private IFruitsFactory _factory;
    
        [Header("SpawnPoints")]
        [SerializeField] private Transform _spawnPoint;


        [Header("ItemSettings")]
        [SerializeField] private float _queueTime = 3f;
        [SerializeField] private float _time = 0;
    
        public void Update()
        {
            var itemRotation = Quaternion.Euler(-90f, 90f, -90f);
            if (_time > _queueTime)
            {
                _factory.Create(_spawnPoint.position, itemRotation);
                _time = 0;
            }

            _time += Time.deltaTime;
        }

        public void SwitchOffSpawner()
        {
            gameObject.SetActive(false);
        }
    }
}