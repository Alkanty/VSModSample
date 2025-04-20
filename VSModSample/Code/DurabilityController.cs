using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace StoneDurabilityMod.Behaviors
{
    // Comportement de bloc qui applique la logique de dégradation dans le temps
    public class DurabilityController : BlockBehavior
    {
        private int maxDurability; // Valeur initiale de durabilité
        private string wornBlockCode; // Code du bloc usé qui remplacera le bloc courant (optionnel)
        private ICoreServerAPI sapi; // Référence à l'API serveur

        public DurabilityController(Block block) : base(block) { }

        public override void Initialize(JsonObject properties)
        {
            base.Initialize(properties);
            maxDurability = properties["maxDurability"].AsInt(100);
            wornBlockCode = properties["wornBlockCode"].AsString(null); // null si non défini
        }

        public override void OnLoaded(ICoreAPI api)
        {
            base.OnLoaded(api);
            sapi = api as ICoreServerAPI;

            // Enregistrement du tick toutes les 60 secondes uniquement côté serveur
            if (sapi != null)
            {
                sapi.Event.RegisterGameTickListener(OnGameTick, 60000);
            }
        }

        // Lorsqu’un bloc est placé, on initialise sa durabilité via le behavior
        public override void OnBlockPlaced(IWorldAccessor world, BlockPos pos, ref EnumHandling handling)
        {
            base.OnBlockPlaced(world, pos, ref handling);

            if (world.Side == EnumAppSide.Server)
            {
                sapi.World.RegisterCallback((dt) =>
                {
                    var be = world.BlockAccessor.GetBlockEntity(pos);
                    if (be != null)
                    {
                        be.Initialize(world.Api); // force l'initialisation des behaviors
                        be.GetBehavior<DurabilityBehavior>()?.SetDurability(maxDurability);
                    }
                }, 50); // 50ms de délai pour garantir que le behavior est initialisé
            }
        }

        // Tick serveur régulier pour réduire la durabilité des blocs
        private void OnGameTick(float dt)
        {
            if (sapi == null) return;

            foreach (var chunkPair in sapi.WorldManager.AllLoadedChunks)
            {
                var chunk = chunkPair.Value;
                foreach (var blockEntity in chunk.BlockEntities.Values)
                {
                    // On ne traite que les blocs de ce type précis
                    if (blockEntity.Block.Code == block.Code)
                    {
                        var durabilityBehavior = blockEntity.GetBehavior<DurabilityBehavior>();
                        if (durabilityBehavior != null)
                        {
                            durabilityBehavior.ReduceDurability(1); // Réduction de 1 point toutes les 60s

                            if (durabilityBehavior.CurrentDurability <= 0 && wornBlockCode != null)
                            {
                                // Remplace le bloc par sa version usée quand il est épuisé (si définie)
                                var wornBlock = sapi.World.GetBlock(new AssetLocation(wornBlockCode));
                                sapi.World.BlockAccessor.SetBlock(wornBlock.Id, blockEntity.Pos);
                            }
                        }
                    }
                }
            }
        }
    }
}