#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Threading;
using System.Collections;
using Server;
using Server.Network;
using Server.Scripts;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items.Crops
{
    [FlipableAttribute(0x1AA2, 0xF00)]
    public class PipeWeed_RollingPaper : DrugSystem_Effect
    {
        [Constructable]
        public PipeWeed_RollingPaper(): base(Utility.RandomList( 0x12AB, 0x12AC ))
        {
            Name = "Rolling Paper";
            this.Weight = 0.2;
            this.Hue = 1153;
        }

        public PipeWeed_RollingPaper(Serial serial): base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(PipeWeed_Leaves), 3) )

            {
                from.SendMessage("You Roll Up The PipeWeed Into A Fatty.");
                from.AddToBackpack(new PipeWeed_Joint());
            }
            else
            {
                from.SendMessage("Your Need More PipeWeed Leaves Bro!");
                return;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
 