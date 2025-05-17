using stonedurabilitymod.src.block;
using stonedurabilitymod.src.blockentity;
using Vintagestory.API.Common;
using Vintagestory.Client.NoObf;

namespace stonedurabilitymod.src;


/// <summary>
/// ------------------------------------------------------------
/// MODSYSTEM CLASS >  INIT LINK BETWEEN MOD AND GAME
/// -----------------------------------------------------------
/// </summary>
public class SDModSystem : ModSystem
{
    //VARIABLE INIT SECTION
    private static string ModId;           

    //START METHOD : INIT ONCE AFTER JSON READ BEFORE WORLD START NEEDED TO RUN API 

    public override void Start(ICoreAPI api)
    {
        ModId = Mod.Info.ModID;         //create variable for storing  domain of mod
        base.Start(api);
        RegisterBlock(api);             //link to  the register block method launch on start 
        RegisterBlockEntity(api);       //link to the register blockentity method launch on start 

    }
    //METHOD :  REGISTER CLASS & BEHAVIORS , CONFIG BEFORE WORLD START
    private static void RegisterBlock(ICoreAPI api)
    {
        api.RegisterBlockClass($"{ModId}:SDBlock", typeof(SDBlock));  
    }
    private static void RegisterBlockEntity(ICoreAPI api)
    {
        api.RegisterBlockEntityClass($"{ModId}:SDBlockEntity", typeof(SDBlockEntity));

    }
    //SHOULDLOAD METHOD : SHOULD YOUR MOD BE LOADER ?
    //utility : register class & behaviors, config before world start

    public override bool ShouldLoad(EnumAppSide forSide)
    {
        return true;
    }




}