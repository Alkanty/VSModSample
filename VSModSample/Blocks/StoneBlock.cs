using Vintagestory.API.Common;

namespace StoneDurabilityMod.Blocks
{
    // Entité de bloc personnalisée qui peut accueillir des behaviors
    public class StoneBlock : BlockEntity
    {
        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);
            api.Logger.Notification("[DEBUG] BlockEntityStoneDurability initialisée !");
        }
    }
}