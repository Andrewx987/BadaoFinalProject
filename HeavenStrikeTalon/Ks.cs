﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace HeavenStrikeTalon
{
    using static Program;
    using static Extension;
    class Ks
    {
        public static void UpdateKs()
        {
            foreach (var hero in HeroManager.Enemies.Where(x => x.IsValidTarget(W.Range) && !x.IsZombie))
            {
                if (WKs && W.IsReady() && W.GetDamage(hero)*2 >= hero.Health)
                {
                    W.Cast(hero);
                }
                if (RKs && R1IsReady() && R.GetDamage(hero) >= hero.Health)
                {
                    var pred = R.GetPrediction(hero);
                    if (!pred.CastPosition.IsWall() && pred.Hitchance >= HitChance.Medium)
                    {
                        R.Cast(pred.CastPosition);
                        LastUltPos = Player.Position.To2D();
                    }
                }
                if (BotrkKs && ItemData.Blade_of_the_Ruined_King.GetItem().IsReady()
                    && ItemData.Blade_of_the_Ruined_King.GetItem().IsInRange(hero) &&
                    (Player.CalcDamage(hero, Damage.DamageType.Physical, hero.MaxHealth * 0.1) >= hero.Health
                    || Player.CalcDamage(hero, Damage.DamageType.Physical, 100) >= hero.Health))
                {
                    ItemData.Blade_of_the_Ruined_King.GetItem().Cast(hero);
                }
                if (CutlassKs && ItemData.Bilgewater_Cutlass.GetItem().IsReady()
                    && ItemData.Bilgewater_Cutlass.GetItem().IsInRange(hero)
                    && (Player.CalcDamage(hero, Damage.DamageType.Magical, 100) >= hero.Health))
                {
                    ItemData.Bilgewater_Cutlass.GetItem().Cast(hero);
                }
                if (TiamatKs && (ItemData.Tiamat_Melee_Only.GetItem().IsReady()
                    || ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsReady()))

                {
                    var pred = Prediction.GetPrediction(hero, Player.AttackCastDelay).UnitPosition;
                    if ((ItemData.Tiamat_Melee_Only.GetItem().IsInRange(pred)
                        || ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsInRange(pred))
                        && Player.CalcDamage(hero, Damage.DamageType.Physical,
                        Player.TotalAttackDamage * (1 - (Player.Position.To2D().Distance(pred.To2D()) - hero.BoundingRadius) / 1000))
                        >= hero.Health)
                    {
                        if (ItemData.Tiamat_Melee_Only.GetItem().IsReady())
                        {
                            ItemData.Tiamat_Melee_Only.GetItem().Cast();
                        }
                        if (ItemData.Ravenous_Hydra_Melee_Only.GetItem().IsReady())
                        {
                            ItemData.Ravenous_Hydra_Melee_Only.GetItem().Cast();
                        }
                    }
                }
            }
            if (Player.HasBuff("talonshadowassaultbuff"))
            {
                foreach (var hero in HeroManager.Enemies.Where(x => x.IsValidTarget(R.Range, true, 
                    LastUltPos.IsValid() ? LastUltPos.To3D() : default(Vector3)) && !x.IsZombie))
                {
                    if (R.GetDamage(hero) >= hero.Health)
                    {
                        if (R2IsReady())
                            R.Cast();
                        else if (HasItem())
                            CastItem();
                    }
                }
            }
        }
    }
}
