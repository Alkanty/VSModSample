using stonedurabilitymod.src.blockentity;
using stonedurabilitymod.src.blocks;
using Vintagestory.API.Common;

namespace stonedurabilitymod.src
{
    
    /// <summary>
    /// ------------------------------------------------------------
    /// MODSYSTEM CLASS >  INIT LINK BETWEEN MOD AND GAME
    /// -----------------------------------------------------------
    /// </summary>
    public class SDModSystem : ModSystem
    {

        //--------------------------------------------------------------
        //START METHOD : INIT ONCE AFTER JSON READ BEFORE WORLD START NEEDED TO RUN API 

        //--------------------------------------------------------------
        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            RegisterBlockBehavior(api);             //link to  the register method launch on start 
            RegisterBlockEntity(api);

        }

        //METHOD :  REGISTER CLASS & BEHAVIORS , CONFIG BEFORE WORLD START
        private static void RegisterBlockBehavior(ICoreAPI api)
        {
            api.RegisterBlockClass("stonedurabilitymod.blocks.SDBlock", typeof(SDBlock));

        }
        private static void RegisterBlockEntity(ICoreAPI api)
        {
            api.RegisterBlockEntityClass("stonedurabilitymod.blockentity.SDBlockEntity", typeof(SDBlockEntity));

        }
        //--------------------------------------------------------------
        //SHOULDLOAD METHOD : SHOULD YOUR MOD BE LOADER ?
        //utility : register class & behaviors, config before world start
        //--------------------------------------------------------------
        public override bool ShouldLoad(EnumAppSide forSide)
        {
            return true;
        }




    }

}