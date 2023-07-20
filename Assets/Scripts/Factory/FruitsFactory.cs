using API;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Factory
{
    public class FruitsFactory : MonoBehaviour, IFruitsFactory, IFruitsConfigsRepository
    {
        public FruitTask FruitTask { get; private set; }
        
        [SerializeField] private ItemData[] _fruitsConfigs;
        
        private void Awake()
        {
            var requiredFruit = _fruitsConfigs[Random.Range(0, _fruitsConfigs.Length)];
            var freeSlots = Random.Range(1, 5);
            
            FruitTask = new FruitTask(requiredFruit,freeSlots);
        }

        public void Create(Vector3 position, Quaternion rotation)
        {
            var fruitConfig = _fruitsConfigs[Random.Range(0, _fruitsConfigs.Length)];
            var fruitInstance = Instantiate(fruitConfig.Prefab, position, rotation);
            fruitInstance.Initialize(fruitConfig, ReferenceEquals(fruitConfig, FruitTask.FruitItem));
        }
    }
    
    public interface IFruitsFactory
    {
        void Create(Vector3 position, Quaternion rotation);
    }

    public interface IFruitsConfigsRepository
    {
        FruitTask FruitTask { get; }
    }
}