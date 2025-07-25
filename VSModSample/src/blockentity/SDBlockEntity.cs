using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.GameContent;
using Vintagestory.API.Common;
using Vintagestory.Common;
using System.ComponentModel.DataAnnotations;
using static Vintagestory.Server.Timer;
using Vintagestory.API.Datastructures;
using System.Net.Http.Headers;
using Vintagestory.API.Server;



namespace stonedurabilitymod.src.blockentity
{
    public class SDBlockEntity : BlockEntity
    {
        double lastday;
        const float maxhp = 100f;
        float blockhp = maxhp;
        double dayelapsed;

        //INIT METHOD FOR EACH NEW BLOCK
        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);
            this.lastday = api.World.Calendar.TotalDays;  
            if (api.Side == EnumAppSide.Server)         //make sure its on server side only, it creates a ticklistener
            {
                RegisterGameTickListener(DurabilityTime, 5000);

                if (!(Api as ICoreServerAPI).World.IsFullyLoadedChunk(Pos)) return;

            }
        }

        //CUSTOM METHOD FOR KEEP TRACK OF DURABILITY
        void DurabilityTime(float _)
        {
            double nowday = Api.World.Calendar.TotalDays;
            Api.Logger.Debug(Convert.ToString(nowday));       //debug
            this.dayelapsed = nowday - this.lastday;
            Api.Logger.Debug(Convert.ToString(dayelapsed));   //debug

            if (dayelapsed < 1.0)  return;  // prevent change if no day has passed
            while (dayelapsed >= 1)         // reduce by 1 HP and reset the daylapsed : add and hook back to the time of day
            {
                this.blockhp = Math.Max(0, this.blockhp - 1);   
                dayelapsed = 0;                                
                this.lastday = Api.World.Calendar.TotalDays;
            }
            MarkDirty();
        }

        //INGAME METHOD OF SERIALIZATION (SEND OUT ONCE WORLD CLODE)
        public override void ToTreeAttributes(ITreeAttribute tree)
        {
            base.ToTreeAttributes(tree);
            tree.SetFloat("blockhp", this.blockhp);
            tree.SetDouble("lastday", this.lastday);
            tree.SetDouble("dayelapsed", this.dayelapsed);
        }

        //INGAME METHOD OF SERIALIZATION (READ IN WHEN WORLD OPEN)
        public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldForResolving)
        {
            base.FromTreeAttributes(tree, worldForResolving);
            this.blockhp = tree.GetFloat("blockhp");
            this.lastday = tree.GetDouble("lastday");
            this.dayelapsed = tree.GetDouble("dayelapsed");
        }

        //INGAME METHOD FOR INFO TAB TO KEEP TRACK OF BLOCK HP
        public override void GetBlockInfo(IPlayer forPlayer, StringBuilder sb)
        {

            sb.AppendLine($"HP: {blockhp}/{maxhp}");
        }
    }
}
