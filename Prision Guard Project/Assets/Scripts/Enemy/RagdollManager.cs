using System;
using System.Collections;
using UnityEngine;
namespace PrisionGuard
{
    public class RagdollManager : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody _mainRb;
        [SerializeField] private Rigidbody _hipsRb;
        [SerializeField] private Collider _mainCollider;
        private bool _canPickup;
        private Rigidbody[] _rbs;
        private Collider[] _colliders;
        private Vector3 _rootStartPos;
        public Action<RagdollManager> OnDisableAction;
        public Action<RagdollManager> OnPunchAction;

        private void OnEnable()
        {
            if (_colliders != null && _rbs != null)
            {
                ActiveRagdoll(false);
            }
        }

        private void OnDisable()
        {
            OnDisableAction?.Invoke(this);
        }
        private void Start()
        {
            _colliders = GetComponentsInChildren<Collider>();
            _rbs = GetComponentsInChildren<Rigidbody>();
            _rootStartPos = _hipsRb.transform.localPosition;
            ActiveRagdoll(false);
        }

        private void ActiveRagdoll(bool value)
        {
            foreach (Rigidbody rb in _rbs)
            {
                if (!value)
                    rb.gameObject.layer = LayerMask.NameToLayer("Body");
                rb.isKinematic = !value;
            }
            foreach (Collider collider in _colliders)
            {
                collider.enabled = value;
            }
            _mainCollider.enabled = !value;
            _mainRb.isKinematic = value;
            _animator.enabled = !value;
        }

        public void AddForce(Vector3 force)
        {
            ActiveRagdoll(true);
            _rbs[UnityEngine.Random.Range(0, _rbs.Length)].AddForce(force);
            StartCoroutine(DelayPickup());
        }

        private IEnumerator DelayPickup()
        {
            yield return new WaitForSeconds(1);
            _canPickup = true;
            OnPunchAction?.Invoke(this);
        }

        public bool PickUp()
        {
            if (!_canPickup)
                return false;
            _canPickup = false;
            foreach (Rigidbody rb in _rbs)
            {
                rb.gameObject.layer = LayerMask.NameToLayer("Stack");
            }
            _hipsRb.transform.localPosition = _rootStartPos;
            _hipsRb.isKinematic = true;

            return true;
        }

        public void ResetHips(Transform parent)
        {
            transform.parent = parent;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}