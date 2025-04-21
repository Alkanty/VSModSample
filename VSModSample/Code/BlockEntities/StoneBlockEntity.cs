using Vintagestory.API.Common;

namespace StoneDurabilityMod.Code.BlockEntities.StoneBlockEntities
{
    // Entité de bloc personnalisée qui peut accueillir des behaviors
    public class StoneBlockEntity : BlockEntity
    {
        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);
            api.Logger.Notification("[DEBUG] BlockEntityStoneDurability initialisée !");
        }
    }
}