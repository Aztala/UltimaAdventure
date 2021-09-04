using System;
using Server;


namespace Server.Items
{
	public class OneRing : GoldRing
	{
		//public override int LabelNumber{ get{ return 1061103; } } // Ring of Health


		[Constructable]
		public OneRing()
		{
			Name = "The One Ring";
			Hue = 0x21;
			ItemID = 0x4CF8;
			Attributes.BonusHits = 30;
			Attributes.RegenHits = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Unique Artifact");
        }


		public OneRing( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();
		}
	}
}