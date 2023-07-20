using System;
using ScriptableObjects;

namespace API
{
    public class FruitTask
    {
        public event Action OnUpdateTask; 
    
        public readonly ItemData FruitItem;
        public readonly int FreeSlots;
        public int CollectedCount;

        public FruitTask(ItemData fruitItem, int freeSlots)
        {
            FreeSlots = freeSlots;
            FruitItem = fruitItem;
        }
   
        public void AddToFruitTask()
        {
            CollectedCount++;
            OnUpdateTask?.Invoke();
        }
    }
}