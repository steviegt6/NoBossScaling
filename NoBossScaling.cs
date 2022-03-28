using Terraria;
using Terraria.ModLoader;

namespace NoBossScaling
{
	public class NoBossScaling : Mod
	{
		public override void Load()
		{
			base.Load();

			On.Terraria.NPC.ScaleStats_ApplyMultiplayerStats += HandleScaling;
		}

		public override void Unload()
		{
			base.Unload();
		}

		private static void HandleScaling(
			On.Terraria.NPC.orig_ScaleStats_ApplyMultiplayerStats orig,
			NPC self,
			int numPlayers,
			float balance,
			float boost,
			float bossAdjustment
		)
		{
			static void SetCache<T>(ref T value, T cache, bool set) => value = set ? cache : value;
			
			int lifeMaxCache = self.lifeMax;
			int damageCache = self.damage;
			int defenseCache = self.defense;
			float scaleCache = self.scale;
			float knockBackResistCache = self.knockBackResist;
			
			orig(self, numPlayers, balance, boost, bossAdjustment);
			
			ScalingConfig cfg = ScalingConfig.Get;
			int type = self.type;
			
			SetCache(ref self.lifeMax, lifeMaxCache, cfg.ShouldScaleHealth(type));
			SetCache(ref self.life, lifeMaxCache, cfg.ShouldScaleHealth(type));
			SetCache(ref self.damage, damageCache, cfg.ShouldScaleDamage(type));
			SetCache(ref self.defDamage, damageCache, cfg.ShouldScaleDamage(type));
			SetCache(ref self.defense, defenseCache, cfg.ShouldScaleDefense(type));
			SetCache(ref self.defDefense, defenseCache, cfg.ShouldScaleDefense(type));
			SetCache(ref self.scale, scaleCache, cfg.ShouldScaleSize(type));
			SetCache(ref self.knockBackResist, knockBackResistCache, cfg.ShouldScaleKb(type));
		}
	}
}