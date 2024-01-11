using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private bool _picked;
    private Rigidbody[] _rbs;

    private Vector3 _rootStartPos;
    private void Start()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        _rootStartPos = _rbs[0].transform.localPosition;
    }
    public bool PickUp()
    {
        if (_picked)
            return false;
        _picked = true;
        foreach (Rigidbody rb in _rbs)
        {
            rb.gameObject.layer = LayerMask.NameToLayer("Stack");
        }
        _rbs[0].transform.localPosition = _rootStartPos;
        _rbs[0].isKinematic = true;

        return true;
    }
}