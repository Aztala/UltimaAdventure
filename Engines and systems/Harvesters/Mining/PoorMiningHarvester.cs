using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class PoorMiningHarvester : BaseHarvester
	{
		[Constructable]
		public PoorMiningHarvester() : base()
		{
			type = 1;
			quality = Utility.RandomMinMax(1, 3);
			NestSpawnType = "WeakSentry";
			Movable = true;
			
		}

		public PoorMiningHarvester( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}