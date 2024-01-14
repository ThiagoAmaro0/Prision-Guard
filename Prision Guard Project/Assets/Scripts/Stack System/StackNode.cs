using UnityEngine;
namespace PrisionGuard
{
    public class StackNode : MonoBehaviour
    {
        [SerializeField] private float _strength = 100;
        [SerializeField] private float _damping = 10f;
        [SerializeField] private Rigidbody _rb;
        private Transform _stackStart;
        private Vector3 _target;
        private Vector3 _force;

        public float Strength { get => _strength; set => _strength = value; }
        public float Damping { get => _damping; set => _damping = value; }
        public Transform StackStart { get => _stackStart; set => _stackStart = value; }
        void Update()
        {
            if (_stackStart)
            {
                _target = new Vector3(0, transform.GetSiblingIndex(), 0) + _stackStart.position;
                _force = _target - transform.position;
                _rb.AddForce((_force * _strength) - (_rb.velocity * _damping));
            }
        }

    }
}
