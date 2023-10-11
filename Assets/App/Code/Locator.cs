namespace App.Code
{
    public static class Locator
    {
        private static class Service<T>
        {
            public static T Instance;
        }

        public static T Get<T>() =>
            Service<T>.Instance;

        public static void Set<T>(T instance) =>
            Service<T>.Instance = instance;
    }
}