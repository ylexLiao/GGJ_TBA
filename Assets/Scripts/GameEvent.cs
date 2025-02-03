using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;


public class GameEvent
{
    public string description;  // 事件描述
    public Sprite eventImage;  // 事件图片（图标或背景）

    public string optionAButtonText;  // 按钮A的文本
    public Action optionAAction;  // 按钮A的事件行为

    public string optionBButtonText;  // 按钮B的文本
    public Action optionBAction;  // 按钮B的事件行为
    public bool isTriggered = false;
    public float triggerProbability;  // 事件的触发概率

    // 构造函数来初始化事件
    public GameEvent(string desc, Sprite image, string optionAText, Action optionA, string optionBText = null, Action optionB = null)
    {
        description = desc;
        eventImage = image;
        optionAButtonText = optionAText;
        optionAAction = optionA;
        optionBButtonText = optionBText;
        optionBAction = optionB;
        triggerProbability = 1;
    }

    // 触发事件方法
    public void TriggerEvent()
    {
        if (!isTriggered && Random.Range(0f, 1f) <= triggerProbability)
        {
            //eventAction.Invoke();
            isTriggered = true;
        }
    }
}

/*// 股市崩盘事件
public class MarketCrashEvent : GameEvent
{
    public MarketCrashEvent() : base(
        "Market Crash",
        "The stock market crashes, leading to a significant drop in stock prices.",
        null,
        () => {
            // 假设股市下跌40%
            StockMarket.Instance.CrashMarket(0.4f);
        }
    )
    { }
}

// 政治献金事件
public class PoliticalDonationEvent : GameEvent
{
    public PoliticalDonationEvent() : base(
        "Political Donation",
        "Make a political donation to influence the next major policy.",
        null,
        () => {
            // 增加政治献金并触发新的政策
            Player.Instance.IncreasePoliticalDonation(1000000);
            // 触发相关任务
            MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        }
    )
    { }
}*/

// 政治献金事件
public class Event1ConferenceEvent : GameEvent
{
    public Event1ConferenceEvent() : base(
        "In June 1929, at a discussion meeting, some people thought that they should seize the bull market tail and make a lot of money, and some senior executives of investment banks raised concerns that they should be stable, and the decision-making power was handed over to you.",
        null,
        "Now is the best time!",
        () => {
            //（激进派角色好感度获取+20%，稳健派角色好感度获取-20%）
            //收到任务：资金积累——完成三个优质新股的承销 任务奖励：获得额外的2亿美金的客户投资


//            Player.Instance.IncreasePoliticalDonation(1000000);
            // 触发相关任务
            //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        },
        "It's not easy being in business for years. Keep what you've got.",
        () => {
            // （激进派角色好感度获取-20%，稳健派角色好感度获取+20%）
//            Player.Instance.IncreasePoliticalDonation(1000000);
            // 触发相关任务
            //MissionManager.Instance.AddMission("Increase donation to improve relations with politicians.");
        }
    )
    { }
}