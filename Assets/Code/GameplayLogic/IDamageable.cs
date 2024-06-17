
namespace Code.GameplayLogic
{
    public interface IDamageable
    {
        void TakeDamage(float damage);
        void Heal(float heal);
    }
}