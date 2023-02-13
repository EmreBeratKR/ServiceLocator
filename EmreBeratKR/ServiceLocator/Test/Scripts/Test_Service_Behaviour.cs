using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Test
{
    [ServiceSceneLoad(ServiceSceneLoadMode.DoNotDestroy)]
    [ServiceRegistration(ServiceRegistrationMode.DoNotAutoRegister)]
    public class Test_Service_Behaviour : ServiceBehaviour
    {
        [SerializeField] private string someString;
        [SerializeField] private int someInt;
        [SerializeField] private float someFloat;
    }
    
    [ServiceRegistration(ServiceRegistrationMode.DoNotAutoRegister)]
    public class Test_Service_Object : Service
    {
        public string SomeString { get; set; }
        public int SomeInt { get; set; }
        public float SomeFloat { get; set; }
    }
}