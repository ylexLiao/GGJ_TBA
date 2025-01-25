using UnityEngine;
using UnityEngine.UI;
using TMPro; // 如果使用的是 TextMeshPro Dropdown

public class FinanceManager : MonoBehaviour
{
    public TMP_Dropdown dropdown; // 绑定 Dropdown

    private float familyWealth = 100000000f;
    private float selfOwnedCapital = 500000000f;
    private float clientFunds = 100000000f;
    private float bankLoans = 300000000f;


    void Start()
    {
        UpdateDropdownOptions();
    }

    public void UpdateDropdownOptions()
    {
        dropdown.ClearOptions();

        // 添加新的选项
        dropdown.options.Add(new TMP_Dropdown.OptionData($"Family Wealth: ${familyWealth:N0}"));
        dropdown.options.Add(new TMP_Dropdown.OptionData($"Self-Owned Capital: ${selfOwnedCapital:N0}"));
        dropdown.options.Add(new TMP_Dropdown.OptionData($"Client ManagesFunds: ${clientFunds:N0}"));
        dropdown.options.Add(new TMP_Dropdown.OptionData($"Bank Loans: ${bankLoans:N0}"));

        dropdown.RefreshShownValue();
    }

    // 示例方法，用于修改资金后刷新选项
    public void AdjustWealth(float amount)
    {
        familyWealth += amount;
        UpdateDropdownOptions();
    }
}
