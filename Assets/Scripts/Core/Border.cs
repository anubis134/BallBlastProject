using Core.Ball.Interfaces;
using UnityEngine;

namespace Core
{
    public class Border : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out IReflectable reflectable))
            {
                reflectable.Reflect(collision.GetContact(0).normal);
                print(collision.GetContact(0).normal.normalized);
            }
        }
    }
}
