using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const
{
    //Tags
    public const string carbonMapTag = "CarbonMap";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Boss = "Boss";
    public const string EquipedGrid= "EquipedGrid";
    //背包格子标签
    public const string bagGrid = "Grid";
    public const string bagItem = "BagItem";
    //场景名称
    public const string SceneLogin = "SceneLogin";//登录场景
    public const string SceneMainCity = "SceneMainCity";//主城场景
    //音效名称加载路径
    public const string bgLoginAudio = "bgLogin";//登录场景背景音效
    public const string bgMainCity = "bgMainCity";//主场景背景音效
    public const string bgCarbon = "bgHuangYe";//副本背景音效
    public const string playerFootAudio = "Se_UI_Run";//角色移动行走音效
    public const string playerHitAudio = "assassin_Hit";//角色移动行走音效
    public const string playerDiedAudio = "AssassinDied";//角色死亡音效
    public const string npcDialogAudio = "sfx_nextDialog";//NPC对话音效

    public const string skill1Audio = "se_104_dagger_attack_3";//技能音效
    public const string skill2Audio = "skill_104_x_twinblade_skill_1";//技能音效
    public const string skill3Audio = "skill_104_x_dagger_skill_1";//技能音效
    public const string attackAudio = "skill_104_x_dagger_attack_";//普通攻击音效
    public const string enemyDieAudio = "MonsterDied2";//敌人死亡音效
    public const string enemyHitAudio = "MonsterHit23";//敌人死亡音效
    public const string enemyAttackAudio = "MonsterAttack";
    public const string bossAttackAudio = "BossAttack";
    public const string bossDiedAudio = "BossDied";
    public const string bossSoundAudio = "BossSound";
    public const string FallStoneAudio = "FallStone";//怪物登场音效
    //创建角色按钮音效名称
    public const string enterGameBtnAudio = "uiLoginBtn";
    //普通UI按钮点击音效
    public const string uiBtnClickAudio = "uiClickBtn";
    public const string uiOpenPage = "uiOpenPage";
    public const string uiCloseBtn = "uiCloseBtn";
    public const string uiExtenBtn = "uiExtenBtn";
    public const string useHPAudio = "buff";
    //副本失败胜利音效
    public const string uiLoseAudio = "fblose";
    public const string uiBalanceAudio = "fbwin";
    public const string uiVictoryAudio = "fbitem";
    //屏幕标准尺寸
    public const int standardScreenWidth = 1334;
    public const int standardScreenHeight = 750;
    //遥感拖拽标准距离
    public const int dragMoveStandardRange = 86;
    public const float playerMoveSpeed =6;
    public const float playerRotateDirSpeed = 3;
    public const float monsterMoveSpeed = 2;

    //玩家人物过渡动画 参数
    public const float animeTransitionTime = 0.3f;
    public const float idelAnimeValue = 0;
    public const float runAnimeValue = 1;
    public const float animeAccelerateValue = 5;
    public const float hpAccelerateValue = 0.1f;
    //怪物 过渡动画 切换参数
    public const int m_bornAnimeValue = 0;
    public const int m_dieAnimeValue = 100;//怪物和角色死亡动画切换参数
    public const int m_hitAnimeValue = 101;//怪物和角色受击动画切换参数
    public const int m_idelAnimeValue = -1;//怪物和角色静止动画切换参数
    //角色技能切换ID 
    public const int animeOnSkill1Value =1;
    public const int animeOnSkill2Value =2;
    public const int animeOnSkill3Value =3;
    public const int animeOffSkillValue =-1;
    //主城ID
    public const int mainCityMapID = 0;
    //自动引导 NPC  ID编号
    public const int npcWiseManID = 0;
    public const int npcGeneralID = 1;
    public const int npcArtisanID = 2;
    public const int npcTraderID = 3;
    //普通攻击连续时间间隔
    public const int attackTimeInterval =500;
}
