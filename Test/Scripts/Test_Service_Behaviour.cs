namespace EmreBeratKR.ServiceLocator.Test
{
    [ServiceSceneLoad(ServiceSceneLoadMode.DoNotDestroy)]
    [ServiceRegistration(ServiceRegistrationMode.DoNotAutoRegister)]
    public class Test_Service_Behaviour : ServiceBehaviour
    {
        public string someString;
        public int someInt;
        public float someFloat;
    }
    
    [ServiceRegistration(ServiceRegistrationMode.DoNotAutoRegister)]
    public class Test_Service_Object : Service
    {
        public string SomeString { get; set; }
        public int SomeInt { get; set; }
        public float SomeFloat { get; set; }
    }
}