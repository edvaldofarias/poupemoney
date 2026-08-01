namespace PoupeMoney.Core.Commons;

public static class CustomFakerExtensions
{
    public static Faker<T> UsePrivateConstructor<T>(this Faker<T> faker) where T : class =>
        faker.CustomInstantiator(_ => (T)Activator.CreateInstance(typeof(T), nonPublic: true)!);
}