﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

namespace BadaoKingdom
{
    static class Program
    {
        public static readonly List<string> SupportedChampion = new List<string>()
        {
            "MissFortune","Poppy","Jhin","Shen",/*"Elise","AurelionSol",*/"Gangplank", /*"Graves" ,*/ /*"Veigar", */"Yasuo"
        };
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            if (!SupportedChampion.Contains(ObjectManager.Player.ChampionName))
            {
                Game.PrintChat("<font color=\"#24ff24\">BadaoKingdom </font>" + "<font color=\"#ff8d1a\">" +
                    ObjectManager.Player.ChampionName + "</font>" + "<font color=\"#24ff24\"> not supported!</font>");
                return;
            }
            Game.PrintChat("<font color=\"#24ff24\">BadaoKingdom </font>" + "<font color=\"#ff8d1a\">" + 
                ObjectManager.Player.ChampionName + "</font>" + "<font color=\"#24ff24\"> loaded!</font>");
            BadaoChampionActivate();
            BadaoUtility.BadaoActivator.BadaoActivator.BadaoActivate();

            // summoner spells

            BadaoMainVariables.Ignite = ObjectManager.Player.GetSpellSlot("SummonerDot");
            BadaoMainVariables.Flash  = ObjectManager.Player.GetSpellSlot("SummonerFlash");
            foreach (var spells in ObjectManager.Player.Spellbook.Spells.Where(
                x =>
                (x.Slot == SpellSlot.Summoner1 || x.Slot == SpellSlot.Summoner2)
                && x.Name.ToLower().Contains("smite")))
            {
                BadaoMainVariables.Smite = spells.Slot;
                break;
            }


        }

        private static void BadaoChampionActivate()
        {
            var ChampionName = ObjectManager.Player.ChampionName;
            if (ChampionName == "MissFortune")
                BadaoChampion.BadaoMissFortune.BadaoMissFortune.BadaoActivate();
            else if (ChampionName == "Poppy")
                BadaoChampion.BadaoPoppy.BadaoPoppy.BadaoActivate();
            else if (ChampionName == "Jhin")
                BadaoChampion.BadaoJhin.BadaoJhin.BadaoActivate();
            else if (ChampionName == "Shen")
                BadaoChampion.BadaoShen.BadaoShen.BadaoActivate();
            //else if (ChampionName == "Elise")
            //    BadaoChampion.BadaoElise.BadaoElise.BadaoActivate();
            else if (ChampionName == "Gangplank")
                BadaoChampion.BadaoGangplank.BadaoGangplank.BadaoActivate();
            //else if (ChampionName == "Graves")
            //    BadaoChampion.BadaoGraves.BadaoGraves.BadaoActivate();
            //else if (ChampionName == "Veigar")
            //    BadaoChampion.BadaoVeigar.BadaoVeigar.BadaoActivate();
            else if (ChampionName == "Yasuo")
                BadaoChampion.BadaoYasuo.BadaoYasuo.BadaoActivate();
            ;
        }
    }
}
