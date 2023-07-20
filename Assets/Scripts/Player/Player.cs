using Basket;
using DG.Tweening;
using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public bool IsPicking => _isPicking;

        [Header("HumanBonesTransform")] 
        [SerializeField] private Transform _hipTarget;
        [SerializeField] private Transform  _spineTarget;
        [SerializeField] private Transform _spineTip;
        [SerializeField] private Transform _rightHandTarget;
        [SerializeField] private Transform _leftHandTarget;
        [SerializeField] private Transform _headTarget;

        [SerializeField] private BasketStorage _basketStorage;
        
        private bool _isPicking;
        private Vector3 _startPosRightHand;
        private Quaternion _startRot;
        private Vector3 _middlePosRightHand;

        private void Awake()
        {
            _startPosRightHand = _rightHandTarget.position;
        }

        private void Start()
        {
            _rightHandTarget.Rotate(0f,0f,-60f);   
            _leftHandTarget.Rotate(0f,50f, 30f);
        }

        public void Anim(FruitItem fruitItem)
        {
            if (_isPicking)
                return;
            
            fruitItem.PickUp();

            _isPicking = true;
            
            var rotateWristToTarget = new Vector3(0f, - 120f, 0);
            var pickUpSequence = DOTween.Sequence();
            
            //Start pick up
            pickUpSequence.Join(_spineTarget.DORotate(new Vector3(70f, 0f, 0f), 0.3f));
            pickUpSequence.Join(_rightHandTarget.DOMove(fruitItem.transform.position, 0.4f).OnComplete(() =>
            {
                _spineTarget.DORotate(new Vector3(40f, 0f, 0f), 0.3f).OnComplete(() =>
                {
                    _spineTarget.DORotate(new Vector3(0f, -70f, 0f), 0.4f);
                    _spineTip.DORotate(new Vector3(0f, -100f, 0f), 0.4f);
                    _hipTarget.DORotate(new Vector3(0f, -60f, 0f), 0.4f);
                });
                _rightHandTarget.DORotate(rotateWristToTarget, 0.5f);
                var fruitTransform = fruitItem.transform;
                fruitTransform.SetParent(_rightHandTarget, true);
                fruitTransform.localPosition = Vector3.zero;

                _middlePosRightHand = _rightHandTarget.position;
            }));
            
            //Rotate spine
            pickUpSequence.Join(_rightHandTarget.DORotate(rotateWristToTarget, 0.3f).OnComplete(() =>
            {
                _hipTarget.DORotate(new Vector3(0f, 50f, 0f), 0.3f);
                _spineTarget.DORotate(new Vector3(0f, 50f, 0f), 0.3f);
            }));
            
            //Put in basket
            var cellPosition = _basketStorage.GetFreeCellPosition();
            pickUpSequence.Append(_rightHandTarget.DOMove(cellPosition, 0.41f).OnComplete( () =>
            {
                AddToBasket(fruitItem);
            }));
            
            //Back to start pos
            pickUpSequence.Append(_spineTip.DORotate(new Vector3(0f, 0f, 0f), 0.4f));
            pickUpSequence.Join(_spineTarget.DORotate(new Vector3(0f, 0f, 0f), 0.5f));
            pickUpSequence.Join(_hipTarget.DORotate(new Vector3(0f, 0f, 0f), 0.5f).OnComplete(() =>
            {
                _rightHandTarget.DOMove(_middlePosRightHand, 0.2f);
                _rightHandTarget.DOMove(_startPosRightHand, 0.4f);
            }));
            
            pickUpSequence.Join( _rightHandTarget.DOMove(_startPosRightHand, 0.4f));
            pickUpSequence.Join(_rightHandTarget.DORotate(new Vector3(0f, 0f, -60f), 0.5f));

            pickUpSequence.OnComplete(() => _isPicking = false);
        }

        private void AddToBasket(FruitItem fruitItem)
        {
            var addedToBasket = _basketStorage.TryAddProductToBasket(fruitItem, out var parent);

            if (!addedToBasket) return;
            fruitItem.transform.SetParent(parent, true);
            fruitItem.transform.localPosition = Vector3.zero;
        }


    }
}