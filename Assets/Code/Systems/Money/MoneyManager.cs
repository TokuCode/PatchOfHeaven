using UnityEngine;

public class MoneyManager : Singleton<MoneyManager>, IBind<MoneyData>
{
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    private MoneyData _moneyData;
    [SerializeField] private int _initialMoney;

    public void Bind(MoneyData moneyData)
    {
        _moneyData = moneyData;
        bool isNew = _moneyData.money <= 0;
        if(isNew) moneyData.money = _initialMoney;
    }
    
    public void SpendMoney(int amount, out bool success)
    {
        if (amount < 0)
        {
            Debug.LogError("Amount must be positive");
            success = false;
            return;
        }
        
        if (_moneyData.money >= amount)
        {
            _moneyData.money -= amount;
            success = true;
            return;
        }

        success = false;
    }

    public void CollectMoney(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Amount must be positive");
            return;
        }
        
        _moneyData.money += amount;
    }
}