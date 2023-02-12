using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Test
{
    public class Test_Service_Locator : MonoBehaviour
    {
        private void Start()
        {
            ServiceLocator.Get<Test_Service_Behaviour>(true);
            ServiceLocator.Get<Test_Service_Object>(true);
        }
    }
}
