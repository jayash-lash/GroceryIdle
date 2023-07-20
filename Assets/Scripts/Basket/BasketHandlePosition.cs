using UnityEngine;

namespace Basket
{
    public class BasketHandlePosition : MonoBehaviour
    {
        [SerializeField] private Vector3 _handlePosition;
        
        [SerializeField] private GameObject _basket;
        [SerializeField] private GameObject _leftHandBound;
        
        private void LateUpdate()
        {
            _basket.transform.position = _leftHandBound.transform.position + _handlePosition;
        }
    }
}
