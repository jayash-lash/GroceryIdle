using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New FruitData", menuName = "Fruit Data", order = 51)]
    public class ItemData : ScriptableObject
    {
        public string FruitName;
        public FruitItem Prefab;
    }
}