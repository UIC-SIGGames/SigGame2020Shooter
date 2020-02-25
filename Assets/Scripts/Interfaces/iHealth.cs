using System;

public interface iHealth
{
    void TakeDamage(float amount);
    float PercentLeft();

    event Action OnTakeDamage;
}
