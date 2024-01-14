using UnityEngine;

namespace PrisionGuard
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10;
        [SerializeField] private Animator _animator;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Rigidbody _rb;

        void FixedUpdate()
        {
            if (_rb)
            {
                if (_joystick)
                {
                    _rb.velocity = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y) * _moveSpeed * Time.deltaTime;
                    _animator.SetBool("Walking", _joystick.Direction != Vector2.zero);
                    if (_joystick.Direction != Vector2.zero)
                    {
                        _animator.transform.forward = _rb.velocity.normalized;
                    }
                }
                else
                {
                    Debug.LogError("Player's joystick not serialized!", this);
                }
            }
            else
            {
                Debug.LogError("Player's Rigidbody not serialized!", this);
            }
        }
    }
}
