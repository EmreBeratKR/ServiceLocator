using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Test
{
    public class Test_Service_Locator : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Register(new GameObject()
                .AddComponent<Test_Service_Behaviour>());
            ServiceLocator.Register(new Test_Service_Object());
            
            ServiceLocator.Get<Test_Service_Behaviour>();
            ServiceLocator.Get<Test_Service_Object>();
        }
    }
}
