using EasyTransition;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
//using UnityEngine.UIElements;

public class Game_1929 : MonoBehaviour
{
    private int CurrentTurn = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public EventUIManager eventUIManager;  // 事件UI管理器
    public Image AdditionTextPanel;
    public Image StockPanel;
    public Text AdditionText;
    public Text TaskText;
    public Text DataText;
    public Text MoneyText;
    public Text RadFavo;
    public Text ModFavo;
    void Start()
    {
        SetStocksValue();
        //TaskText.text = "123\r\n213213\r\n213\r\n12\r\n3\r\n213\r\n21\r\n3\r\n21\r\n312\r\n3\r\n2132\r\n3\r\n12\r\n312\r\n3\r\n12\r\n312\r\n12";
        //UnityEngine.SceneManagement.SceneManager.LoadScene("GlobalEvent");
        EventManager eventManager = FindFirstObjectByType<EventManager>();

        GameEvent event1ConferenceEvent = new GameEvent(
        "Conference:\n" +
        "In June 1929, at a discussion meeting, some people thought that they should seize the bull market tail and make a lot of money, and some senior executives of investment banks raised concerns that they should be stable, and the decision-making power was handed over to you.",
        Resources.Load<Sprite>("img/2008"),
        "Now is the best time!",
        () =>
        {
            //（激进派角色好感度获取+20%，稳健派角色好感度获取-20%）
            GameManager.Instance.UpdateCharacterFavorability("Radicals", 20);
            GameManager.Instance.UpdateCharacterFavorability("Moderates", -20);
            ShowAdditionText("Progressive characters get +20% favorability\n moderate characters get -20% favorability");
            ShowAdditionText("Received task: Capital accumulation - Complete the underwriting task of three premium new issues\nMission Bonus: Receive an additional $200 million in customer investment");
            //收到任务：资金积累——完成三个优质新股的承销 任务奖励：获得额外的2亿美金的客户投资
            TaskText.text += "Task: Capital accumulation - Complete the underwriting task of three premium new issues\n\nMission Bonus: Receive an additional $200 million in customer investment";
            //            Player.Instance.IncreasePoliticalDonation(1000000);
            // 触发相关任务
            //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        },
        "It's not easy being in business for years. Keep what you've got.",
        () =>
        {
            // （激进派角色好感度获取-20%，稳健派角色好感度获取+20%）
            GameManager.Instance.UpdateCharacterFavorability("Radicals", -20);
            GameManager.Instance.UpdateCharacterFavorability("Moderates", 20);
            // 触发相关任务
            //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        }
        );
        eventUIManager.ShowEventUI(event1ConferenceEvent);



        var a = Resources.Load<Sprite>("img/capitalist");
        var b = Resources.Load<Sprite>("img/card");
        //// 注册一个股市崩盘事件
        //eventManager.RegisterEvent(new Event1ConferenceEvent());

    }

    Coroutine coroutine;
    private Queue<string> messageQueue = new Queue<string>(); // 文本队列
    private bool isDisplaying = false; // 标记是否正在显示文本

    // 添加文本到队列
    public void ShowAdditionText(string message)
    {
        messageQueue.Enqueue(message); // 将文本加入队列
        if (!isDisplaying) // 如果没有正在显示文本，启动协程
        {
            StartCoroutine(DisplayMessages());
        }
    }

    // 协程：依次处理队列中的文本
    private IEnumerator DisplayMessages()
    {
        isDisplaying = true;

        while (messageQueue.Count > 0)
        {
            // 取出队列中的文本
            string message = messageQueue.Dequeue();
            AdditionText.text = message; // 显示文本
            AdditionTextPanel.gameObject.SetActive(true); // 确保文本框可见

            yield return new WaitForSeconds(2f); // 显示2秒

            AdditionTextPanel.gameObject.SetActive(false); // 隐藏文本框
        }

        isDisplaying = false; // 标记为未显示状态
    }

    public void ShowAdditionText2(string text)
    {
        //if (coroutine.)
        //{

        //}
        AdditionTextPanel.gameObject.SetActive(true);
        AdditionText.text = text;
        //Invoke("CloseAdditionTextPanel", 2f);

        coroutine = StartCoroutine(CloseAdditionTextP());

    }

    private IEnumerator CloseAdditionTextP()
    {
        yield return new WaitForSeconds(2f);
        AdditionTextPanel.gameObject.SetActive(false);
    }

    private void CloseAdditionTextPanel()
    {
        AdditionTextPanel.gameObject.SetActive(false);
    }

    public TransitionManager manager;
    public TransitionSettings transition;
    private bool event2 = false;
    private bool event3 = false;
    private bool event4 = false;
    private bool event5 = false;
    private bool event7 = false;
    private bool event6 = false;

    public void EndTurnBtnClicked()
    {
        var gameState = GameManager.Instance.gameState;
        var a = Resources.Load("img/1719");
        var ws = Resources.Load("../img/capitalist.png");
        var aa = Resources.Load("capitalist");
        var wws = Resources.Load<Sprite>("img/capitalist");

        //gameState.currentMonth++;



        manager = TransitionManager.Instance();
        manager.onTransitionCutPointReached += () =>
        {
            Debug.Log("Cut Point Reached");
            manager.onTransitionCutPointReached -= () => { };
        };
        manager.Transition(transition, 0);
        //StartCoroutine(WaitAndExecute());
        //WaitForSeconds wait = new WaitForSeconds(10f);
        Invoke("PerformAction", 1f);

    }

    private void PerformAction()
    {
        GameManager.Instance.AdvanceTime(1);
        var m = GameManager.Instance.CurrentMonth;
        DateTime date = new DateTime(1929 + (m + 5) / 12, (m + 5) % 12, 1);
        string formattedDate = date.ToString("MMMM ", new System.Globalization.CultureInfo("en-US"));
        formattedDate += "1st";
        formattedDate += ", " + date.Year;
        DataText.text = formattedDate;
        MoneyText.text = GameManager.Instance.Money.ToString() + "$";
        RadFavo.text = GameManager.Instance.GetFavorability("Radicals").ToString();
        ModFavo.text = GameManager.Instance.GetFavorability("Moderates").ToString();
        SimulatePriceChanges();


        SetStocksValue();
    }

    

    private void SimulatePriceChanges()
    {
        var s = GameManager.Instance.Stocks;
        foreach (Stock stock in s)
        {
            // 生成随机涨跌幅
            float randomChange = UnityEngine.Random.Range(-1.1f, 1.1f);

            // 根据市场趋势调整价格
            float marketImpact = stock.MarketTrendImpact;

            // 计算新的价格
            //stock.PriceChangeRate = randomChange * marketImpact * 100f; // 转换为百分比
            stock.PreviousPrice = stock.CurrentPrice;
            stock.CurrentPrice += stock.CurrentPrice * randomChange * marketImpact;

            // 确保价格不会低于0
            stock.CurrentPrice = Mathf.Max(stock.CurrentPrice, 0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoneyText.text = GameManager.Instance.Money.ToString() + "$";
        RadFavo.text = GameManager.Instance.GetFavorability("Radicals").ToString();
        ModFavo.text = GameManager.Instance.GetFavorability("Moderates").ToString();
        if (GameManager.Instance.CurrentMonth + 6 == 11 && !event2)
        {
            event2 = true;
            GameEvent event2ConferenceEvent = new GameEvent(//股市特殊变化：1929年10月到11月的股票全部下跌百分之40，卖出成功概率降至百分之20，买入成功概率提高至百分之95
            "Special changes in the stock market:\nIn October and November 1929, when all stocks fell 40 percent, the selling probability dropped to 20 percent and the buying probability increased to 95 percent.",
            Resources.Load<Sprite>("img/1719"),
            "Get it.",
            () => { }
            );
            eventUIManager.ShowEventUI(event2ConferenceEvent);
        }
        else if (GameManager.Instance.CurrentMonth + 6 == 12 && !event3)
        {
            event3 = true;
            GameEvent event3ConferenceEvent = new GameEvent(
            "Conference:\n" +
            "After Black Thursday: November 1929, the morning meeting was very serious, discussing the disastrous week before. The decision on the future direction of the company is handed to the players, some suggest taking advantage of this opportunity to aggressively short, while others suggest shrinking investment.",
            Resources.Load<Sprite>("img/1719"),
            "Short the stock! Become the richest man in America!",
            () =>
            {//收到任务：黑色星期四的机遇与挑战——如果选择1，则该任务内容为在1930年10月前将公司非贷款资产翻倍，任务成功则奖励激进派好感度10点。
                ShowAdditionText(
                "Received task: The opportunities and challenges of Black Thursday doubled the company's non-loan assets by October 1930.\n" +
                "Mission Bonus: Radical Favorability 10 points.");
                TaskText.text += "Received task: The opportunities and challenges of Black Thursday doubled the company's non-loan assets by October 1930.\n" +
                "Mission Bonus: Radical Favorability 10 points.";
            },
            "Shrink investment! Preserving assets is more important!",
            () =>
            {//收到任务：黑色星期四的机遇与挑战——如果选择1，则该任务内容为在1930年10月前将公司非贷款资产翻倍，任务成功则奖励激进派好感度10点。
                ShowAdditionText(
                "Received task: Completed 10 profitable investments by October 1930, and the company's non-loan assets declined by less than 10 percent. \n" +
                "Mission Bonus: Moderate Favorability 10 points.");
                TaskText.text += "Received task: Completed 10 profitable investments by October 1930, and the company's non-loan assets declined by less than 10 percent. \n" +
                "Mission Bonus: Moderate Favorability 10 points.";
            }
            );
            eventUIManager.ShowEventUI(event3ConferenceEvent);
        }
        else if (GameManager.Instance.CurrentMonth + 6 == 13 && !event4)
        {
            event4 = true;
            GameEvent event4ConferenceEvent = new GameEvent(//股市特殊变化：1929年11月到12月的股票全部下跌百分之20，卖出成功概率回升至百分之30，买入成功概率下降至百分之85。此后股市自然下跌概率设定为百分60，自然买入成功率设定为百分之80，自然卖出成功率设定为百分之40。
            "Special changes in the stock market:\r\n" +
            "In November and December 1929, when all stocks fell 20 percent, the probability of success in selling recovered to 30 percent, and the probability of success in buying fell to 85 percent. After that, the natural decline probability of the stock market is set at 60 percent, the natural buying success rate is set at 80 percent, and the natural selling success rate is set at 40 percent.",
            Resources.Load<Sprite>("img/1719"),
            "Get it.",
            () => { }
            );
            eventUIManager.ShowEventUI(event4ConferenceEvent);
        }
        else if (GameManager.Instance.CurrentMonth + 6 == 23 && !event5)
        {
            event5 = true;
            GameEvent event5ConferenceEvent = new GameEvent(
            "Conference:\n"
            + "In October 1930, President Hoover threatened to impose federal regulation on the New York Stock Exchange, forcing the exchange to \"voluntarily\" agree not to make loans for short selling, and total bank lending fell by 40 percent (soon to be corrected by setting parameter C at 0.8). The company meeting stirred up a discussion. This policy is undoubtedly a blow to the radicals, but they still do not give up, this view is countered by the moderates",
            Resources.Load<Sprite>("img/1719"),
            "Not good for our policy, but we still have a chance!", //1.不利好我们的政策，但我们仍有机会！（激进派好感度+10，稳健派好感度-10）
            () =>
            {
                GameManager.Instance.UpdateCharacterFavorability("Radicals", 10);
                GameManager.Instance.UpdateCharacterFavorability("Moderates", -10);
                ShowAdditionText("Progressive characters get +10% favorability\n moderate characters get -10% favorability");

                //收到任务：顺应政策的变革：完成十次低买高卖的做多收益行为，任务奖励：两派好感度都+10，政客好感度+10，银行关系+0.1。
                var t = "Received task: Comply with the change of policy: complete ten low buy high sell long income behavior.\n" +
                "Mission Bonus: Favorability ratings are both +10, politicians +10, banking relations +0.1";
                ShowAdditionText(t);
                TaskText.text += t;
            },
            "It's time to slow down.",      //2.是时候放慢脚步了。（激进派好感度-10，稳健派好感度+10）
            () =>
            {
                GameManager.Instance.UpdateCharacterFavorability("Radicals", -10);
                GameManager.Instance.UpdateCharacterFavorability("Moderates", 10);
                ShowAdditionText("Progressive characters get -10% favorability\n moderate characters get 10% favorability");
            }
            );
            eventUIManager.ShowEventUI(event5ConferenceEvent);
        }
        else if (GameManager.Instance.CurrentMonth + 6 >= 36 && GameManager.Instance.Money >= 50000000000 && !event6)
        {
            event6 = true;
            GameEvent event6ConferenceEvent = new GameEvent(
            "Roosevelt's contact:\r\n" +
            "Roosevelt came to the protagonist's company, claiming that he is participating in the presidential campaign, hoping that the player can help him save the American people, he told the player that he will concentrate the last strength of the United States to inject capital into the development of infrastructure industry to solve the employment problem of the American people (construction, public utilities, industrial stocks will rise, the success rate of buying 100 percent). It is hoped that players can assist him in his actions and continue to buy stocks in the three directions of construction, utilities, and industry.",
            Resources.Load<Sprite>("img/1719"),
            "The opportunity of a lifetime! The last of America's wealth!", //1.千载难逢的机会来了！搜刮尽美利坚最后一丝财富！（激进派好感度+10，稳健派好感度-10）
            () =>
            {
                GameManager.Instance.UpdateCharacterFavorability("Radicals", 10);
                GameManager.Instance.UpdateCharacterFavorability("Moderates", -10);
                ShowAdditionText("Progressive characters get +10% favorability\n moderate characters get -10% favorability");

                //收到任务：白头鹰的命运：
                //完成方式一：大量买入建筑、公共事业、工业行业股票后卖出，引发三大行业雪崩。触发结局："富豪"
                //完成方式二：听从罗斯福的意见，每月增买建筑、生活消费品、重工业三个方向的股票各自30亿美元连续十个月并在此后长期持有12个月，即可完成任务。触发结局：富豪
                var t = "Received task: The fate of the bald eagle.\n" +
                "Ending: \"Rich\"\r\nBuy a lot of construction, public utilities, industrial stocks and then sell, triggering an avalanche in the three major industries.\r\nEnding: Rich\r\nListen to Roosevelt's advice, each month to buy construction, consumer goods, heavy industry three directions of the stock of $3 billion for 10 consecutive months and then hold for 12 months, you can complete the task.";
                ShowAdditionText(t);
                TaskText.text += t;
            },
            "The future president's opinion is worth considering.",      //2.未来总统的意见值得考虑。（激进派好感度-10，稳健派好感度+10）
            () =>
            {
                GameManager.Instance.UpdateCharacterFavorability("Radicals", -10);
                GameManager.Instance.UpdateCharacterFavorability("Moderates", 10);
                ShowAdditionText("Progressive characters get -10% favorability\n moderate characters get 10% favorability");
            }
            );
            eventUIManager.ShowEventUI(event6ConferenceEvent);
        }

        //public class Event1ConferenceEvent : GameEvent
        //{
        //    public Event1ConferenceEvent() : base(
        //        "In June 1929, at a discussion meeting, some people thought that they should seize the bull market tail and make a lot of money, and some senior executives of investment banks raised concerns that they should be stable, and the decision-making power was handed over to you.",
        //    Resources.Load<Sprite>("img/1719"),
        //    "Now is the best time!",
        //    () => {
        //        //（激进派角色好感度获取+20%，稳健派角色好感度获取-20%）
        //        GameManager.Instance.UpdateCharacterFavorability("Radicals", 20);
        //        GameManager.Instance.UpdateCharacterFavorability("Moderates", -20);
        //        ShowAdditionText("Progressive characters get +20% favorability\n moderate characters get -20% favorability");
        //        ShowAdditionText("Received task: Capital accumulation - Complete the underwriting task of three premium new issues\nMission Bonus: Receive an additional $200 million in customer investment");
        //        //收到任务：资金积累——完成三个优质新股的承销 任务奖励：获得额外的2亿美金的客户投资
        //        TaskText.text += "Task: Capital accumulation - Complete the underwriting task of three premium new issues\n\nMission Bonus: Receive an additional $200 million in customer investment";
        //        //            Player.Instance.IncreasePoliticalDonation(1000000);
        //        // 触发相关任务
        //        //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        //    },
        //    "It's not easy being in business for years. Keep what you've got.",
        //    () => {
        //        // （激进派角色好感度获取-20%，稳健派角色好感度获取+20%）
        //        GameManager.Instance.UpdateCharacterFavorability("Radicals", -20);
        //        GameManager.Instance.UpdateCharacterFavorability("Moderates", 20);
        //        // 触发相关任务
        //        //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        //    }
        //        ) { }
    }

    public Text Text13;
    public Text Text23;
    public Text Text33;
    public Text Text43;
    public Text Text53;
    public Text Text63;
    public Text Text73;
    public Text Text83;

    public Text Text14;
    public Text Text24;
    public Text Text34;
    public Text Text44;
    public Text Text54;
    public Text Text64;
    public Text Text74;
    public Text Text84;

    public Text Text15;
    public Text Text25;
    public Text Text35;
    public Text Text45;
    public Text Text55;
    public Text Text65;
    public Text Text75;
    public Text Text85;

    public void BuyStockBtnClicked()
    {
        var s = GameManager.Instance.Stocks;
        float c = 0;
        c += (int.Parse(Text15.text) - s[0].StockHoldings) * s[0].CurrentPrice;
        c += (int.Parse(Text25.text) - s[1].StockHoldings) * s[1].CurrentPrice;
        c += (int.Parse(Text35.text) - s[2].StockHoldings) * s[2].CurrentPrice;
        c += (int.Parse(Text45.text) - s[3].StockHoldings) * s[3].CurrentPrice;
        c += (int.Parse(Text55.text) - s[4].StockHoldings) * s[4].CurrentPrice;
        c += (int.Parse(Text65.text) - s[5].StockHoldings) * s[5].CurrentPrice;
        c += (int.Parse(Text75.text) - s[6].StockHoldings) * s[6].CurrentPrice;
        c += (int.Parse(Text85.text) - s[7].StockHoldings) * s[7].CurrentPrice;
        if (c >= GameManager.Instance.Money)
        {
            ShowAdditionText("Insufficient funds!");
            return;
        }
        GameManager.Instance.Money -= c;

        s[0].StockHoldings = int.Parse(Text15.text);
        s[1].StockHoldings = int.Parse(Text25.text);
        s[2].StockHoldings = int.Parse(Text35.text);
        s[3].StockHoldings = int.Parse(Text45.text);
        s[4].StockHoldings = int.Parse(Text55.text);
        s[5].StockHoldings = int.Parse(Text65.text);
        s[6].StockHoldings = int.Parse(Text75.text);
        s[7].StockHoldings = int.Parse(Text85.text);
    }

    public void StockExitBtnClicked()
    {
        SetStocksValue();
    }

    private void SetStocksValue()
    {
        var s = GameManager.Instance.Stocks;

                Text13.text = s[0].CurrentPrice.ToString();
                Text14.text = s[0].PriceChangeRate.ToString() + "%";
                Text15.text = s[0].StockHoldings.ToString();

                Text23.text = s[1].CurrentPrice.ToString();
                Text24.text = s[1].PriceChangeRate.ToString() + "%";
                Text25.text = s[1].StockHoldings.ToString();

                Text33.text = s[2].CurrentPrice.ToString();
                Text34.text = s[2].PriceChangeRate.ToString() + "%";
                Text35.text = s[2].StockHoldings.ToString();

                Text43.text = s[3].CurrentPrice.ToString();
                Text44.text = s[3].PriceChangeRate.ToString() + "%";
                Text45.text = s[3].StockHoldings.ToString();

                Text53.text = s[4].CurrentPrice.ToString();
                Text54.text = s[4].PriceChangeRate.ToString() + "%";
                Text55.text = s[4].StockHoldings.ToString();

                Text63.text = s[5].CurrentPrice.ToString();
                Text64.text = s[5].PriceChangeRate.ToString() + "%";
                Text65.text = s[5].StockHoldings.ToString();

                Text73.text = s[6].CurrentPrice.ToString();
                Text74.text = s[6].PriceChangeRate.ToString() + "%";
                Text75.text = s[6].StockHoldings.ToString();

                Text83.text = s[7].CurrentPrice.ToString();
                Text84.text = s[7].PriceChangeRate.ToString() + "%";
                Text85.text = s[7].StockHoldings.ToString();

        
    }

    public void SmallBtnClicked(Button b)
    {
        var num = int.Parse(b.GetComponentInParent<Text>().text);
        num--;
        b.GetComponentInParent<Text>().text = num.ToString();
    }
    public void SmallBtnClicked2(Button b)
    {
        var num = int.Parse(b.GetComponentInParent<Text>().text);
        num++;
        b.GetComponentInParent<Text>().text = num.ToString();
    }
}