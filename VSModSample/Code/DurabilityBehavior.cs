using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using System.Text;

namespace StoneDurabilityMod.Behaviors
{
    // Comportement attaché à un BlockEntity pour gérer la durabilité
    public class DurabilityBehavior : BlockEntityBehavior
    {
        public int CurrentDurability { get; private set; } // Stockage de la durabilité actuelle

        public DurabilityBehavior(BlockEntity blockentity) : base(blockentity) { }

        public override void Initialize(ICoreAPI api, JsonObject properties)
        {
            base.Initialize(api, properties);
            api.Logger.Notification("[DEBUG] DurabilityBehavior initialisé !");
        }

        // Initialise la durabilité
        public void SetDurability(int durability)
        {
            CurrentDurability = durability;
            Blockentity.MarkDirty(true);
            Blockentity.Api.Logger.Notification($"[DEBUG] Durabilité initialisée à {durability}");
        }

        // Réduit la durabilité du bloc
        public void ReduceDurability(int amount)
        {
            CurrentDurability -= amount;
            if (CurrentDurability < 0) CurrentDurability = 0;
            Blockentity.MarkDirty(true);
        }

        // Chargement depuis les attributs persistants
        public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldAccessForResolve)
        {
            base.FromTreeAttributes(tree, worldAccessForResolve);
            CurrentDurability = tree.GetInt("durability");
        }

        // Sauvegarde dans les attributs persistants
        public override void ToTreeAttributes(ITreeAttribute tree)
        {
            base.ToTreeAttributes(tree);
            tree.SetInt("durability", CurrentDurability);
        }

        // Affiche la durabilité quand le joueur regarde le bloc
        public override void GetBlockInfo(IPlayer forPlayer, StringBuilder sb)
        {
            base.GetBlockInfo(forPlayer, sb);
            sb.AppendLine($"Durabilité : {CurrentDurability} / 100");
        }
    }
}