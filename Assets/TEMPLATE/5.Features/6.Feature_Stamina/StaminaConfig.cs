using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Template.Stamina
{
    [CreateAssetMenu(fileName = "StaminaConfig", menuName = "GAME/STAMINA/StaminaConfig")]
    public class StaminaConfig : ScriptableObject
    {
        [Separator("CONFIG")]
        public int MaxStamina = 5;
        [Separator("TIMES")]
        [Tooltip("Total second to recorver 1 stamina")]
        public int RecoverTime = 600;
        [Tooltip("Duration that user dont need to pay stamina when use")]
        public int UnlimitTime = 300;

        //optional
        [Separator("PRICE")]
        public int StaminaPriceCoin = 200;
        public int StaminaPriceDiamond = 5;
    }
}