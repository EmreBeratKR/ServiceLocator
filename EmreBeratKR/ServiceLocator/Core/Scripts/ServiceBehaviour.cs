using UnityEngine;

namespace EmreBeratKR.ServiceLocator
{
    public abstract class ServiceBehaviour : MonoBehaviour, IService
    {
        protected virtual void Reset()
        {
            name = ServiceLocator.GetServiceName(GetType());
        }
    }
}