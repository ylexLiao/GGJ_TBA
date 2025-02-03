using UnityEngine;

public class LineChartManager : MonoBehaviour
{
    public LineRenderer lineRenderer; // 绑定 LineRenderer
    public int pointCount = 10; // 数据点数量
    private float[] stockPrices; // 股票价格数据
    private float xSpacing = 1.0f; // 点之间的水平间隔

    void Start()
    {
        // 创建一个新的宽度曲线
        AnimationCurve widthCurve = new AnimationCurve();
        widthCurve.AddKey(0.0f, 0.1f); // 起点宽度 0.1
        widthCurve.AddKey(1.0f, 0.1f); // 终点宽度 0.1

        // 将曲线应用到 LineRenderer
        lineRenderer.widthCurve = widthCurve;

        // 设置宽度倍数
        lineRenderer.widthMultiplier = 1.0f; // 全局宽度倍数
        stockPrices = new float[pointCount];
        InitializePrices();
        DrawChart();
    }

    void InitializePrices()
    {
        // 初始化股票价格为随机值
        for (int i = 0; i < pointCount; i++)
        {
            stockPrices[i] = Random.Range(100f, 500f); // 随机价格范围
        }
    }

    public void UpdatePrices()
    {
        // 模拟价格波动
        for (int i = 0; i < pointCount; i++)
        {
            stockPrices[i] += Random.Range(-20f, 20f); // 波动范围
        }

        // 重新绘制图表
        DrawChart();
    }

    void DrawChart()
    {
        // 设置点数量
        lineRenderer.positionCount = pointCount;

        // 更新每个点的坐标
        for (int i = 0; i < pointCount; i++)
        {
            float x = i * xSpacing; // X 轴间距
            float y = stockPrices[i] / 100f; // Y 轴缩放比例
            lineRenderer.SetPosition(i, new Vector3(x, y, 0)); // 设置点位置
        }
    }
}
