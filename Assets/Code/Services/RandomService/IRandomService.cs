namespace Code.Services.RandomService
{
    public interface IRandomService : IService
    {
        int RandomByNumber(int minValue, int maxValue);
    }
}