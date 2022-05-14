using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace NoBossScaling
{
    [Label("Multiplayer Scaling Config")]
    public class ScalingConfig : ModConfig
    {
        public static ScalingConfig Get => ModContent.GetInstance<ScalingConfig>();
        
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [Header("Global")]
        [Label("Disable All HP Scaling")]
        [Tooltip("Disables health scaling for all NPCs.")]
        [DefaultValue(true)]
        public bool DisableAllHealth { get; set; } = true;

        [Label("Disable All Dmg. Scaling")]
        [Tooltip("Disables damage scaling for all NPCs.")]
        [DefaultValue(true)]
        public bool DisableAllDamage { get; set; } = true;

        [Label("Disable All Def. Scaling")]
        [Tooltip("Disables defense scaling for all NPCs.")]
        [DefaultValue(true)]
        public bool DisableAllDefense { get; set; } = true;
        
        [Label("Disable All Size Scaling")]
        [Tooltip("Disables size scaling for all NPCs.")]
        [DefaultValue(true)]
        public bool DisableAllSize { get; set; } = true;
        
        [Label("Disable All KB Res. Scaling")]
        [Tooltip("Disables knockback resistance scaling for all NPCs.")]
        [DefaultValue(true)]
        public bool DisableAllKb { get; set; } = true;

        [Header("Whitelists")]
        [Label("Global Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve all their scaling.")]
        public List<NPCDefinition> GlobalWhitelist { get; set; } = new();

        [Label("HP Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve their health scaling.")]
        public List<NPCDefinition> HealthWhitelist { get; set; } = new();
        
        [Label("Dmg. Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve their damage scaling.")]
        public List<NPCDefinition> DamageWhitelist { get; set; } = new();
        
        [Label("Def. Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve their defense scaling.")]
        public List<NPCDefinition> DefenseWhitelist { get; set; } = new();
        
        [Label("Size Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve their size scaling.")]
        public List<NPCDefinition> SizeWhitelist { get; set; } = new();
        
        [Label("KB Res. Scaling Whitelist")]
        [Tooltip("Whitelist NPCs that should preserve their knockback resistance scaling.")]
        public List<NPCDefinition> KbWhitelist { get; set; } = new();

        public bool IsWhitelisted(int type, List<NPCDefinition> whitelist) => whitelist.Any(x => x.Type == type) || GlobalWhitelist.Any(x => x.Type == type);

        public bool ShouldScaleHealth(int type) => !DisableAllHealth && !IsWhitelisted(type, HealthWhitelist);
        
        public bool ShouldScaleDamage(int type) => !DisableAllDamage && !IsWhitelisted(type, DamageWhitelist);
        
        public bool ShouldScaleDefense(int type) => !DisableAllDefense && !IsWhitelisted(type, DefenseWhitelist);
        
        public bool ShouldScaleSize(int type) => !DisableAllSize && !IsWhitelisted(type, SizeWhitelist);
        
        public bool ShouldScaleKb(int type) => !DisableAllKb && !IsWhitelisted(type, KbWhitelist);
    }
}