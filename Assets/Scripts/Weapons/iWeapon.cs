using System;

public interface iWeapon
{
    event Action OnFire;
    void CommandFire();
}
