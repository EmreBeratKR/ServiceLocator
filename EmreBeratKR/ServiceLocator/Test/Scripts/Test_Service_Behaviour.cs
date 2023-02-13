using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Test
{
    [DoNotAutoRegister]
    [DoNotDestroyOnLoad]
    public class Test_Service_Behaviour : ServiceBehaviour
    {
        [SerializeField] private string someString;
        [SerializeField] private int someInt;
        [SerializeField] private float someFloat;
    }
    
    [DoNotAutoRegister]
    public class Test_Service_Object : Service
    {
        public string SomeString { get; set; }
        public int SomeInt { get; set; }
        public float SomeFloat { get; set; }
    }
}