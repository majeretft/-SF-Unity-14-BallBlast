using UnityEngine;

public class GameData
{
    public int? Damage { get; set; }
    public int? FireAmount { get; set; }
    public float? FireRate { get; set; }
    public int? Level { get; set; }
    public int? Coins { get; set; }
}

public class PersistantData : MonoBehaviour
{
    private string _keyDamage = "app:dmg";
    private string _keyFireAmount = "app:fireamount";
    private string _keyFireRate = "app:firerate";
    private string _keyLevel = "app:level";
    private string _keyCoins = "app:coins";

    public void Save(GameData data)
    {
        if (data.Damage.HasValue)
            PlayerPrefs.SetInt(_keyDamage, data.Damage.Value);

        if (data.FireAmount.HasValue)
            PlayerPrefs.SetInt(_keyFireAmount, data.FireAmount.Value);

        if (data.Level.HasValue)
            PlayerPrefs.SetInt(_keyLevel, data.Level.Value);

        if (data.Coins.HasValue)
            PlayerPrefs.SetInt(_keyCoins, data.Coins.Value);

        if (data.FireRate.HasValue)
            PlayerPrefs.SetFloat(_keyFireRate, data.FireRate.Value);
    }

    public GameData Read()
    {
        return new GameData
        {
            Coins = PlayerPrefs.GetInt(_keyCoins),
            Damage = PlayerPrefs.GetInt(_keyDamage),
            FireAmount = PlayerPrefs.GetInt(_keyFireAmount),
            FireRate = PlayerPrefs.GetFloat(_keyFireRate),
            Level = PlayerPrefs.GetInt(_keyLevel),
        };
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
}
