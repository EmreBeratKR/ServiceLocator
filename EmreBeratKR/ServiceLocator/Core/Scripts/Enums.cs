namespace EmreBeratKR.ServiceLocator
{
    public enum ServiceRegistrationMode
    {
        UseGlobal,
        AutoRegister,
        DoNotAutoRegister
    }

    public enum ServiceSceneLoadMode
    {
        UseGlobal, 
        Destroy,
        DoNotDestroy
    }
}