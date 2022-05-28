public interface IDamageable
{
    float MaxHealth { get; set; }

    float Health { get; set; }

    void Damage( float damage );
}
