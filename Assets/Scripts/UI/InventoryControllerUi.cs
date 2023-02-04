using TMPro;
using UnityEngine;

public class InventoryControllerUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private PlayerInventory _playerInventory;
    
    private void Update() {
        _coinText.text = $"Coins: {_playerInventory.Coins}";
    }
}
