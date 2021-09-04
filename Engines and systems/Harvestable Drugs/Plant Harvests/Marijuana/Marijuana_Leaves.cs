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
    public class PipeWeed_Leaves : DrugSystem_Engine
    {
        [Constructable]
        public PipeWeed_Leaves() : this(null) { }

        [Constructable]
        public PipeWeed_Leaves(Mobile sower): base(0x18E5)
        {
            Name = "PipeWeed Leaves";
            Weight = 0.3;
            Hue = 167;
            Movable = true;
            Stackable = true;
        }

        public override void OnDoubleClick(Mobile from)
        {

            int emote = Utility.Random(0);
            switch (emote)
            {
                case 0:
                    {
                        from.Emote("*Sniffs The Freshly Gathered Leaves*");
                        from.SendMessage("You Sense These Leaves Are Of The Finest Quality");

                        break;
                    }
            }
        }
        public PipeWeed_Leaves(Serial serial): base(serial)
        {
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