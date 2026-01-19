public interface ITowerAttack
{
    void Execute(Monster target);
}

public interface ITickableAttack
{
    void Tick(float deltaTime);
}