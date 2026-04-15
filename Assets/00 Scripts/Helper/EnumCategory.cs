using System;

public enum EGameState
{
    Loading,
    Home,
    Gameplay,
}

public enum EGamePlayState
{
    Cinematic,
    Running,
    GameOver,
    Pause
}
public enum EGameType
{
    Campaign,
    Endless,
}
public enum EAnimationEffectType
{
    None = 0,
}

public enum EItemIngame
{
}

public enum EAchievementType
{
    Login,
    LevelPlay,
    LevelFailed,
    LevelCompleted,
    SpinLuckyWheel,
    UseBooster,
}

public enum ERarity
{
    Common,
    Great,
    Rare,
    Epic,
    Legend,
    Mythical,
}
public enum ECommonResource
{
    Coin,
    Gem,
    Energy,
}
public enum EVirtualResource
{

}
public enum EExpireableResource
{

}
public enum EContentActiveResource
{

}
public enum ERewardState
{
    Progress,
    CanClaim,
    Claimed,
}
public enum EIAPPackType
{
    Coin_pack_1,
    Coin_pack_2,
    Coin_pack_3,
    Coin_pack_4,
    Coin_pack_5,
    Coin_pack_6,
}
public enum EResourceFrom
{
    Hack,
    GameDrop,
    LuckySpin,
    TimeReward,
    AdsReward,
    UpgradeGun,
    NormalChest,
    DailyLogin,
    IAP,
    SpendIngame,
    ReviveIngame,
    SpendOpenChest,
    SpendTeam,
    MergeEquipment,
    ServerGenerate,
    MintInInVentory,
    Shop,
    DailyQuest,
    InviteFriend,
    PartnerReferal,
    Leaderboard,
    Tutorial,
}
public enum EButtonType
{
    Common,
    ResourceConsume,
    ActionConsume,
}
public enum EButtonColor
{
    Green,
    Yellow,
    Blue,
    Gray,
    Purple,
    Red,
    Orange,
}
public enum EDiscount
{
    E0,
    E10,
    E20,
    E30,
    E50,
    E75,
}
public enum EShopPurchaseType
{
    Free,
    Ads,
    Coin,
    Gem,
    IAP,
}
public enum EStatusState
{
    None,
    disconnected,
    connected,
    connecting,
    reconnecting,
}
public enum EHoleType
{
    None,
    Empty,
    Locked,
    HasPin,
}
public enum EBarMaterial
{
    Bar1, Bar2, Bar3, Bar4, Bar5, Bar6, Bar7
}
public enum EBackBoardMaterial
{
    Board1, Board2, Board3, Board4, Board5, Board6, Board7
}
public enum ESliderShape
{
    Rectangle,
    Circle,
    Right_Triangle,
    O_Shape,
    L_Shape,
    T_Shape,
    U_Shape,
    Plus_Shape,
    Holed_Rectangle,
    H_Shape,
}
public enum ERandomRotation
{
    E0 = 0,
    E27 = 27,
    E45 = 45,
    E63 = 63,
    E90 = 90,
    E117 = 117,
    E135 = 135,
    E152 = 152,
    E180 = 180,
    E225 = 225,
    E270 = 270,
    E315 = 315,
}
public enum ENodeType
{
    Empty,
    Wall,
    Start,
    End,
    Monster,
    Treasure,
}
public enum EGameSetting
{
    Music,
    Sound,
    Vibration,
}
public enum EShopType
{
    IAP,
    Coin,
    Gem,
    Booster,
}
public enum EShopCoin
{
    ShopCoin1,
    ShopCoin2,
    ShopCoin3,
}
public enum EShopEnergy
{
    ShopEnergy1,
    ShopEnergy2,
    ShopEnergy3,
}

public enum EAdsRewardPlacement
{
    Booster,
    X2GameCoin,
}

public enum EAdsInterPlacement
{
    
}