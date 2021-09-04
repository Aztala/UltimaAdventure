#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using Server;
using Server.Items;
using Server.Items.Crops;

namespace Server.Items
{
    public class PipeWeed_StarterKit : Bag
    {
        [Constructable]
        public PipeWeed_StarterKit(): this(0x0E76)
        {
        }

        [Constructable]
        public PipeWeed_StarterKit(int amount)
        {

            Name = "PipeWeed Kit";
            Hue = 0;
    
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );
            DropItem(new PipeWeed_Seed(1) );

        }

        public PipeWeed_StarterKit(Serial serial): base(serial)
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
