/********************************************************************
    created: 2021/09/09
    filename: PathConfig .cs
    author:	 Mashiro Shiina
	e_mail address:1967407707@qq.com 
	date: 9:9:2021   16:36
	purpose: 存储配置路径名称
*********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MFramework
{
    public class ResPathConfig
    {
        //玩家登录界面 自动生成名字 配置文件路径
        public const string spawnNameConfigPath = "ResCofig/rdname";
        //主城配置文件路径
        public const string mainCityConfigPath = "ResCofig/map";
        //怪物属性配置文件路径
        public const string monsterConfigPath = "ResCofig/monster";
        //引导任务配置文件路径
        public const string guideTaskConfigPath = "ResCofig/guide";
        //强化配置数据文件路径
        public const string strengthenConfigPath = "ResCofig/strong";
        //任务奖励配置文件路径
        public const string taskRewardConfigPath = "ResCofig/taskreward";
        //技能配置文件 路径
        public const string skillXMlPath = "ResCofig/skill";
        //技能移动距离配置文件 路径
        public const string skillMoveXMlPath = "ResCofig/skillmove";
        //技能伤害配置文件 路径
        public const string skillActionXMlPath = "ResCofig/skillaction";
        //商店商品配置文件 路径
        public const string itemXMlPath = "ResCofig/storegoods";
        //任务奖励预制体 路径
        public const string taskRewardUIPrefabPath = "UIPrefabs/RewardItem";

        //引导任务不同NPC 图片资源路径
        public const string taskHead = "ResImages/task";
        public const string wiseManHead = "ResImages/wiseman";
        public const string generalHead = "ResImages/general";
        public const string artisanHead = "ResImages/artisan";
        public const string traderHead = "ResImages/trader";
        //引导任务不同NPC icon资源路径
        public const string wiseManIcon = "ResImages/npc0";
        public const string generalIcon = "ResImages/npc1";
        public const string artisanIcon = "ResImages/npc2";
        public const string traderIcon = "ResImages/npc3";
        public const string guideIcon = "ResImages/npcguide";
        public const string selfIcon = "ResImages/assassin";
        //角色预制体路径
        public const string prefabPlayerPath = "PrefabPlayer/AssassinCharacter";
        public const string prefabCarbonPlayerPath = "PrefabPlayer/CarbonAssassinCharacter";
        //血条预制体加载路径
        public const string prefabHPItemPath = "UIPrefabs/ItemEntityHP";
        //角色技能特效加载路径
        public const string skillPath = "PrefabFX/";
        public const string skill1Path = "PrefabFX/dagger_skill1";
        public const string skill2Path = "PrefabFX/dagger_skill2";
        public const string skill3Path = "PrefabFX/dagger_skill3";
        public const string skillAttack1Path = "PrefabFX/dagger_atk1";
        public const string skillAttack2Path = "PrefabFX/dagger_atk2";
        public const string skillAttack3Path = "PrefabFX/dagger_atk3";
        public const string skillAttack4Path = "PrefabFX/dagger_atk4";
        public const string skillAttack5Path = "PrefabFX/dagger_atk5";
        //信息面板相机目标图片路径
        public const string renderTexturePath = "UIPrefabs/CharacterInfoRenderTexture";
        //强化资源路径
        public const string headIcon = "ResImages/helmet";
        public const string bodyIcon = "ResImages/body";
        public const string waistIcon = "ResImages/waist";
        public const string armIcon = "ResImages/hand";
        public const string legIcon = "ResImages/leg";
        public const string footIcon = "ResImages/foot";

        public const string star1Icon = "ResImages/star1";
        public const string star2Icon = "ResImages/star2";
        //聊天按钮图片的配置路径
        public const string chat1Icon = "ResImages/btntype1";
        public const string chat2Icon = "ResImages/btntype2";
        //血条图片配置路径
        public const string hpGreen = "ResImages/fgGreen";
        public const string hpRed = "ResImages/fgRed";
        //结算评级 图片资源路径
        public const string balanceGrade = "ResImages/fb-d";
        //商店物品预制体路径
        public const string itemPrefab = "UIPrefabs/ItemGoods";
        //背包物品预制体路径
        public const string bagItemPrefab = "UIPrefabs/BagItem";
    }
}