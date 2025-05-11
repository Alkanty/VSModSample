using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.GameContent;
using Vintagestory.API.Common;
using Vintagestory.Common;
using System.ComponentModel.DataAnnotations;



namespace stonedurabilitymod.src.blockentity
{
    public class SDBlockEntity : BlockEntity
    {

        public override void Initialize(ICoreAPI api)
        {
            base.Initialize(api);


        }
        
        public override void GetBlockInfo(IPlayer forPlayer, StringBuilder sb)
        {

            string hp = "100";
            string MaxHp = "100";
            sb.AppendLine($"HP: {hp}/{MaxHp}");
        }
    }
}
