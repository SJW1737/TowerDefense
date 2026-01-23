using UnityEngine;
using UnityEngine.Events;

public class GoldManager : MonoSingleton<GoldManager>
{
    [Header("Gold")]
    [SerializeField] private int currentGold = 100;

    public int CurrentGold => currentGold;

    public UnityEvent<int> OnGoldChanged;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Init()
    {
        if (OnGoldChanged == null)
            OnGoldChanged = new UnityEvent<int>();
    }

    public bool CanSpend(int amount)
    {
        return currentGold >= amount;
    }

    public bool Spend(int amount)
    {
        if (!CanSpend(amount))
            return false;

        currentGold -= amount;
        OnGoldChanged.Invoke(currentGold);
        return true;
    }

    public void Add(int amount)
    {
        currentGold += amount;
        OnGoldChanged.Invoke(currentGold);
    }

    public void ApplyStartGoldRelic()
    {
        if (!RelicManager.IsReady)
        {
            return;
        }

        int bonus = (int)RelicManager.Instance.GetValue(RelicEffectType.StartGold);

        if (bonus <= 0)
        {
            return;
        }

        currentGold += bonus;
        OnGoldChanged.Invoke(currentGold);
    }
}
