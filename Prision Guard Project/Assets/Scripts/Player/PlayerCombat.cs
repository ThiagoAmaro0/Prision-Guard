using UnityEngine;
namespace PrisionGuard
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private float _punchForce;
        [SerializeField] private Animator _anim;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out RagdollManager ragdoll))
            {
                Vector3 direction = other.transform.position + Vector3.up - transform.position;
                ragdoll.AddForce(direction.normalized * _punchForce);
                _anim.SetTrigger("Attack");
            }
        }
    }
}