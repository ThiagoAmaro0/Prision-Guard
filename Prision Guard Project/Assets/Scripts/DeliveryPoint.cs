using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out StackManager stack))
        {
            int count = stack.Delivery();
            print($"Delivery {count} enemies");
        }
    }
}
