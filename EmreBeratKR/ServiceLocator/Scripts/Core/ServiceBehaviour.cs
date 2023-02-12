using UnityEngine;

namespace EmreBeratKR.ServiceLocator
{
    public abstract class ServiceBehaviour : MonoBehaviour, IService
    {
        [SerializeField] private bool dontDestroyOnLoad;
        
        
        protected void Awake()
        {
            TryMarkAsDontDestroyOnLoad();
        }

        protected virtual void Reset()
        {
            name = ServiceLocator.GetServiceName(GetType());
        }


        private bool TryMarkAsDontDestroyOnLoad()
        {
            if (!dontDestroyOnLoad) return false;
            
            DontDestroyOnLoad(gameObject);

            return true;
        }
    }
}