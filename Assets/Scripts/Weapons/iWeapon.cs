using System;

public interface iWeapon
{
    float GetConsumption();
    event Action OnFire;
    void CommandFire();
}
