using UnityEngine;

public class StockManager : MonoBehaviour
{
    public float[] industryGrowth = new float[8]; // 各行业增长率

    void Start()
    {
        InitializeStocks();
    }

    void InitializeStocks()
    {
        for (int i = 0; i < industryGrowth.Length; i++)
        {
            industryGrowth[i] = Random.Range(-0.5f, 0.5f); // 初始波动
        }
    }

    public void UpdateStockPrices()
    {
        for (int i = 0; i < industryGrowth.Length; i++)
        {
            industryGrowth[i] += Random.Range(-0.1f, 0.1f); // 每回合波动
        }
    }
}
