using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // 单例模式

    // 游戏状态
    public GameState gameState; // 全局游戏状态
    public List<Stock> Stocks { get; set; } = new List<Stock>(); // 股票列表


    // 存档路径
    private string savePath;

    private void Awake()
    {
        // 单例模式初始化
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 确保在切换场景时不会销毁
            savePath = Application.persistentDataPath + "/gameSave.json"; // 设置存档路径
            InitializeGameState(); // 初始化游戏状态
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 初始化游戏状态
    private void InitializeGameState()
    {
        //if (File.Exists(savePath))
        //{
        //    // 如果有存档，加载存档
        //    LoadGame();
        //}
        //else
        {
            // 没有存档则创建新游戏
            gameState = new GameState();
            gameState.money = 10000f; // 默认金钱
            gameState.currentMonth = 1; // 时间从1月开始
            gameState.characters = new List<Character>
            {
                new Character("Radicals", 50),
                new Character("Moderates", 50),
            };
            //        Construction,       // 建筑行业
            //Utilities,          // 公共事业
            //Manufacturing,      // 工业制造
            //Retail,             // 消费品零售
            //Entertainment,      // 娱乐行业
            //Automotive,         // 汽车行业
            //Military,           // 军工行业
            //Finance             // 金融行业
            //var a = new Stock("Construction", 98);
            //Stocks.Add(a);
            Stocks.Add(new Stock    { Name = "Construction", CurrentPrice = 100f, PreviousPrice = 98f, MarketTrendImpact = 1f });


            Stocks.Add(new Stock { Name = "Utilities", CurrentPrice = 100f, PreviousPrice = 101f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Retail", CurrentPrice = 100f, PreviousPrice = 93f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Entertainment", CurrentPrice = 100f, PreviousPrice = 94f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Automotive", CurrentPrice = 100f, PreviousPrice = 97f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Military", CurrentPrice = 100f, PreviousPrice = 103f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Finance", CurrentPrice = 100f, PreviousPrice = 90f, MarketTrendImpact = 1f });
            Stocks.Add(new Stock { Name = "Manufacturing", CurrentPrice = 100f, PreviousPrice = 99f, MarketTrendImpact = 1f });
        }
    }
    public int GetFavorability(string characterName)
    {
        Character character = gameState.characters.Find(c => c.name == characterName);
        if (character != null)
        {
            return character.favorability;
        }
        else
        {
            Debug.LogWarning($"未找到角色: {characterName}");
            return 0;
        }
    }

    public int CurrentMonth
    {
        get { return (int)gameState.currentMonth; }
        set { gameState.currentMonth = value; }
    }

    public float Money
    {
        get { return (float)gameState.money; }
        set { gameState.money = value; }
    }

    

    // 增加金钱
    public void AddMoney(float amount)
    {
        gameState.money += amount;
        Debug.Log($"金钱增加: {amount}，当前金钱: {gameState.money}");
    }

    // 减少金钱
    public void DeductMoney(float amount)
    {
        gameState.money -= amount;
        if (gameState.money < 0)
        {
            gameState.money = 0;
        }
        Debug.Log($"金钱减少: {amount}，当前金钱: {gameState.money}");
    }

    // 推进时间
    public void AdvanceTime(int months)
    {
        gameState.currentMonth += months;
        Debug.Log($"时间推进: {months}个月，当前时间: {gameState.currentMonth}月");
    }

    // 更新角色好感度
    public void UpdateCharacterFavorability(string characterName, int delta)
    {
        Character character = gameState.characters.Find(c => c.name == characterName);
        if (character != null)
        {
            character.favorability = Mathf.Clamp(character.favorability + delta, -100, 100); // 好感度范围限制在-100到100
            Debug.Log($"角色{characterName}好感度改变: {delta}，当前好感度: {character.favorability}");
        }
        else
        {
            Debug.LogWarning($"未找到角色: {characterName}");
        }
    }

    // 保存游戏
    public void SaveGame()
    {
        string json = JsonUtility.ToJson(gameState, true);
        File.WriteAllText(savePath, json);
        Debug.Log($"游戏已保存，存档路径: {savePath}");
    }

    // 加载游戏
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            gameState = JsonUtility.FromJson<GameState>(json);
            Debug.Log("游戏存档已加载");
        }
        else
        {
            Debug.LogWarning("没有找到存档文件");
        }
    }
}

// 游戏状态类
[System.Serializable]
public class GameState
{
    //public List<Stock> stocks;
    public float money;                // 金钱
    public int currentMonth;           // 当前月份
    public List<Character> characters; // 角色列表
}

// 角色类
[System.Serializable]
public class Character
{
    public string name;       // 角色名
    public int favorability;  // 好感度

    public Character(string name, int favorability)
    {
        this.name = name;
        this.favorability = favorability;
    }
}

[Serializable]
public class Stock
{
    //public Stock(string name,float previousPrice)
    //{
    //    Name = name;
    //    CurrentPrice = 100f;
    //    PreviousPrice = previousPrice;
    //    MarketTrendImpact = 1f;
    //    StockHoldings = 0;
    //}
    //public Stock(string name, float currentPrice, float previousPrice)
    //{
    //    Name = name;
    //    CurrentPrice = 100f;
    //    PreviousPrice = previousPrice;
    //    MarketTrendImpact = 1f;
    //    StockHoldings = 0;
    //}
    public string Name;              // 股票名称
    //public string Industry;          // 所属行业
    public float CurrentPrice =100;       // 当前价格
    public float PriceChangeRate => ((CurrentPrice - PreviousPrice) / PreviousPrice) * 100;    // 涨跌幅（百分比）
    //public float Volatility;         // 波动率，控制涨跌的幅度
    public float PreviousPrice;          // 初始价格（用于参考）
    public float MarketTrendImpact = 1;  // 市场趋势对股票的影响
    public int StockHoldings = 0;        // 持有数量
    public float StockValue => CurrentPrice * StockHoldings;  // 股票市值
}

public enum IndustryType
{
    Construction,       // 建筑行业
    Utilities,          // 公共事业
    Manufacturing,      // 工业制造
    Retail,             // 消费品零售
    Entertainment,      // 娱乐行业
    Automotive,         // 汽车行业
    Military,           // 军工行业
    Finance             // 金融行业
}