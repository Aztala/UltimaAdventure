//Created By Milva

using System;
using Server;

namespace Server.Items
{	


    public class  VikingWoodAxe : Item
                               
	             {
		[Constructable]
		public VikingWoodAxe () : base( 0x9E81)
		{                
			
                              Weight = 2;
                             Name = "Viking Wood Axe Sculpture";
                                
                                                
		}

        public VikingWoodAxe(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}