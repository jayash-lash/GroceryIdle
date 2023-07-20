using UnityEngine;

namespace Conveyour
{
    public class BeltScroll : MonoBehaviour
    {
        [SerializeField] private Vector2 _movementVector = Vector2.zero;
        
        [SerializeField] private Material[] _material;
        [SerializeField] private float _scrollSpeed;
    
        private float _offset;
    
        private void Update()
        {
            ScrollByOffset();
        }

        private void ScrollByOffset()
        {
            var normalizedMovement = _movementVector.normalized;

            _offset += Time.deltaTime * _scrollSpeed * normalizedMovement.sqrMagnitude;
            var offsetVector = new Vector2(0, _offset);

            foreach (var material in _material)
                material.mainTextureOffset = offsetVector;
        }
    }
}