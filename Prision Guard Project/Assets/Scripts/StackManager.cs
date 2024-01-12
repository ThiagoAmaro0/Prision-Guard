using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [SerializeField] private StackNode _nodePrefab;
    [SerializeField] private List<StackNode> _stack;
    [SerializeField] private float _minDamping = 0.3f;
    [SerializeField] private float _strengthLoss = 0.2f;
    [SerializeField] private float _dampingLoss = 0.2f;
    [SerializeField] private Transform _stackStart;
    [SerializeField] private Transform _stackParent;
    [SerializeField] private LayerMask _pickupLayer;

    void Start()
    {
        _stack = new List<StackNode>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_pickupLayer == (_pickupLayer | (1 << other.gameObject.layer)))
        {
            RagdollManager ragdoll = other.gameObject.GetComponentInParent<RagdollManager>();
            if (ragdoll)
            {
                if (ragdoll.PickUp())
                {
                    Add(ragdoll);
                }
            }
        }
    }

    private void Add(RagdollManager ragdoll)
    {
        StackNode node = Instantiate(_nodePrefab, new Vector3(0, _stack.Count, 0) + _stackStart.position,
                                    Quaternion.identity, _stackParent);
        if (_stack.Count > 1)
        {
            float strength = _stack[_stack.Count - 1].Strength - _stack[_stack.Count - 1].Strength * _strengthLoss;
            float damping = _stack[_stack.Count - 1].Damping - _stack[_stack.Count - 1].Damping * _dampingLoss;

            node.Strength = strength;
            node.Damping = damping < _minDamping ? _minDamping : damping;
        }
        node.StackStart = _stackStart;
        _stack.Add(node);

        ragdoll.ResetHips(_stack[_stack.Count - 1].transform);
    }
}
