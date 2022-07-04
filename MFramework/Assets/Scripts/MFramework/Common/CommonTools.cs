/********************************************************************
    created: 2021/09/09
    filename: CommonTools.cs
    author:	 Mashiro Shiina
	e_mail address:1967407707@qq.com 
	date: 9:9:2021   20:36
	purpose: 扩展定义常用的方法函数
*********************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework
{
    public class MathTools
    {
        #region Math Method
        /// <summary>
        /// 自定义工具函数，根据最大值和最小值，获取非负随机值
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static int GetRandomValue(int minValue, int maxValue, System.Random random = null)
        {
            if (random == null)
            {
                random = new System.Random();
            }
            int resValue = random.Next(minValue, maxValue);
            return resValue;
        }
        #endregion
    }
    //颜色工具
    public enum ColorType
    {
        Red,
        Scarlet,//猩红色
        Yellow,
        Khaki,//土黄色
        Green,
        Kelly,//黄绿色
        Blue,
        CobaltBlue,
        Orange
    }
    public class ColorTools
    {
        //颜色代码定义常量
        private const string redColor = "<color=red>";
        private const string scarletColor = "<color=#BC1717>";
        private const string yellowColor = "<color=FFEA04>";
        private const string orangeColor = "<color=orange>";
        private const string khakiColor = "<color=#9F9F5F>";
        private const string greenColor = "<color=#3afd06>";
        private const string kellyColor = "<color=#99CC32>";
        private const string blueColor = "<color=#3299CC>";
        private const string cobaltBlueColor = "<color=#5959AB>";
        private const string endColor = "</color>";
        public static string ClorFonts(string content, ColorType colorType)
        {
            string result = "";
            switch (colorType)
            {
                case ColorType.Blue:
                    result = blueColor + content + endColor;
                    break;
                case ColorType.CobaltBlue:
                    result = cobaltBlueColor + content + endColor;
                    break;
                case ColorType.Green:
                    result = greenColor + content + endColor;
                    break;
                case ColorType.Kelly:
                    result = kellyColor + content + endColor;
                    break;
                case ColorType.Red:
                    result = redColor + content + endColor;
                    break;
                case ColorType.Scarlet:
                    result = scarletColor + content + endColor;
                    break;
                case ColorType.Yellow:
                    result = yellowColor + content + endColor;
                    break;
                case ColorType.Khaki:
                    result = khakiColor + content + endColor;
                    break;
                case ColorType.Orange:
                    result = orangeColor + content + endColor;
                    break;
            }
            return result;
        }
    }
}
