using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Test
{
    public class Test_Service_Behaviour : ServiceBehaviour
    {
        [SerializeField] private string someString;
        [SerializeField] private int someInt;
        [SerializeField] private float someFloat;
    }
    
    public class Test_Service_Object : Service
    {
        public string SomeString { get; set; }
        public int SomeInt { get; set; }
        public float SomeFloat { get; set; }
    }
}