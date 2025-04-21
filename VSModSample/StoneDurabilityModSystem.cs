using Vintagestory.API.Common;

namespace StoneDurabilityMod
{
    public class StoneDurabilityModSystem : ModSystem
    {
        public override void Start(ICoreAPI api)
        {
            base.Start(api);

            // Enregistrement des classes nécessaires pour le mod avec noms uniques basés sur le ModID
            api.RegisterBlockEntityClass(Mod.Info.ModID + ".StoneBlock", typeof(Blocks.StoneBlock));
        }
    }
}