using System;
using API;
using Factory;
using ScriptableObjects;
using UI;
using UnityEngine;
using Zenject;

namespace Basket
{
    public class BasketStorage : MonoBehaviour
    {
        [Inject] private IFruitsConfigsRepository _fruitsReposit;
        
        [SerializeField] private Transform[] _basketPositions;

        private BasketCell[] _basketCells;
        private int _currentIndex;
        private FruitTask _fruitTask;

        private void Start()
        {
            _basketCells = new BasketCell[_basketPositions.Length];
            _fruitTask = _fruitsReposit.FruitTask;
        }

        public Vector3 GetFreeCellPosition()
        {
            return _basketPositions[_currentIndex].position;
        }

        public bool TryAddProductToBasket(FruitItem data, out Transform parent)
        {
            if (_currentIndex < _fruitTask.FreeSlots && data.IsRequired)
            {
                _basketCells[_currentIndex] = new BasketCell(data, _basketPositions[_currentIndex]);
                parent = _basketPositions[_currentIndex];
                _currentIndex++;
                _fruitTask.AddToFruitTask();
                
                return true;
            }
            
            parent = null;
            return false;
        }
    }

    [Serializable]
    public class BasketCell
    { 
        public FruitItem Product;
        public Transform Position;

        public BasketCell(FruitItem product, Transform position)
        {
            Product = product;
            Position = position;
        }
    }
}