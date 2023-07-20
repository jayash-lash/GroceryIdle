using API;
using Factory;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UITaskView : MonoBehaviour
    {
        [Inject] private IFruitsConfigsRepository _repository;

        [SerializeField] private TextMeshProUGUI _fruitCountText;
        [SerializeField] private TextMeshProUGUI _requiredFruitText;

        private FruitTask _task;
    
        private void Start()
        {
            _task = _repository.FruitTask;
            _requiredFruitText.text = "Required Fruit: " + _task.FruitItem.FruitName;
            _fruitCountText.text = "Collected: " + _task.CollectedCount + "/" + _task.FreeSlots;
        
            _task.OnUpdateTask += UpdateFruitCount;
        }

        private void UpdateFruitCount()
        {
            _fruitCountText.text = "Collected: " + _task.CollectedCount + "/" + _task.FreeSlots;
        }
    }
}