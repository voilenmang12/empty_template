using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TimerCachedData : ControllerCachedData
{
    public long nextDay;
    public long nextWeek;
    public long nextMonth;

    public override void OnNewData()
    {
    }

    public override void FirstTimeInit()
    {
    }
}
public class TimerController : SingletonController<TimerController,TimerCachedData>
{
    protected DateTime nextDay;
    protected DateTime nextWeek;
    protected DateTime nextMonth;

    protected override string KeyData()
    {
        return "timer_system";
    }
    protected override string KeyEvent()
    {
        return Constant.EVENT_TIMER_CHANGED;
    }

    protected override void OnInitSuccess()
    {
        base.OnInitSuccess();
        nextDay = Helper.ParseDateTime(cachedData.nextDay);
        nextWeek = Helper.ParseDateTime(cachedData.nextWeek);
        nextMonth = Helper.ParseDateTime(cachedData.nextMonth);
    }

    protected override void OnTick()
    {
        base.OnTick();
        if (IsNewDay())
            TigerForge.EventManager.EmitEvent(Constant.EVENT_TIMER_NEW_DAY);
        if (IsNewWeek())
            TigerForge.EventManager.EmitEvent(Constant.EVENT_TIMER_NEW_WEEK);
        if (IsNewMonth())
            TigerForge.EventManager.EmitEvent(Constant.EVENT_TIMER_NEW_MONTH);
    }
    protected override void OnNextDay()
    {
        base.OnNextDay();
        nextDay = Helper.NextDay(DateTime.UtcNow);
        cachedData.nextDay = nextDay.ToUnixTimestamp();
        OnValueChange();
    }
    protected override void OnNextWeek()
    {
        base.OnNextWeek();
        nextWeek = Helper.NextWeek(DateTime.UtcNow);
        cachedData.nextWeek = nextWeek.ToUnixTimestamp();
        OnValueChange();
    }
    protected override void OnNextMonth()
    {
        base.OnNextMonth();
        nextMonth = Helper.NextMonth(DateTime.UtcNow);
        cachedData.nextMonth = nextMonth.ToUnixTimestamp();
        OnValueChange();
    }
    public bool IsNewDay()
    {
        return DateTime.UtcNow > nextDay;
    }

    public bool IsNewWeek()
    {
        return DateTime.UtcNow > nextWeek;
    }

    public bool IsNewMonth()
    {
        return DateTime.UtcNow > nextMonth;
    }

    public DateTime GetNextDay()
    {
        return nextDay;
    }

    public DateTime GetNextWeek()
    {
        return nextWeek;
    }

    public DateTime GetNextMonth()
    {
        return nextMonth;
    }
}
