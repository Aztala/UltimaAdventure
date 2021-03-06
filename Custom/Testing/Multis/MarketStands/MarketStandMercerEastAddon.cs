/////////////////////////////////////////////////
//                                             //
// Automatically generated by the              //
// AddonGenerator script by Arya               //
//                                             //
/////////////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarketStandMercerEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MarketStandMercerEastAddonDeed();
			}
		}

		[ Constructable ]
		public MarketStandMercerEastAddon()
		{
            AddonComponent ac;

            ac = new AddonComponent(5991);
            ac.Hue = 3;
            //ac.Name = "market stand";
            AddComponent(ac, -1, 0, 7);

            ac = new AddonComponent(5991);
            ac.Hue = 23;
            //ac.Name = "market stand";
            AddComponent(ac, -1, 0, 6);

            ac = new AddonComponent(4000);
            ac.Hue = 44;
            //ac.Name = "market stand";
            AddComponent(ac, -1, 0, 8);

            ac = new AddonComponent(5991);
            ac.Hue = 55;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 7);

            ac = new AddonComponent(5991);
            ac.Hue = 66;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 8);

            ac = new AddonComponent(5991);
            ac.Hue = 77;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 9);

            ac = new AddonComponent(5991);
            ac.Hue = 88;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 10);

            ac = new AddonComponent(5991);
            ac.Hue = 99;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 11);

            ac = new AddonComponent(4000);
            ac.Hue = 122;
            //ac.Name = "market stand";
            AddComponent(ac, 0, 0, 12);

            ac = new AddonComponent(3996);
            ac.Hue = 53;
            ac.Name = "bolt of silk";
            AddComponent(ac, 0, 1, 1);

            ac = new AddonComponent(4000);
            ac.Hue = 248;
            ac.Name = "bolt of linen";
            AddComponent(ac, 1, 0, 7);

            ac = new AddonComponent(5991);
            ac.Hue = 178;
            AddComponent(ac, 1, 0, 6);

            ac = new AddonComponent(3999);
            AddComponent(ac, 1, 0, 8);

            ac = new AddonComponent(4000);
            ac.Hue = 210;
            AddComponent(ac, 1, 0, 9);

            ac = new AddonComponent(3989);
            ac.Hue = 38;
            AddComponent(ac, 2, 0, 0);

            ac = new AddonComponent(3992);
            ac.Hue = 254;
            AddComponent(ac, 2, 0, 3);

            ac = new AddonComponent(3989);
            ac.Hue = 14;
            AddComponent(ac, 2, 1, 1);

            ac = new AddonComponent(3995);
            ac.Hue = 276;
            AddComponent(ac, 1, 1, 1);

            ac = new AddonComponent( 3991);
            ac.Hue = 287;
            AddComponent(ac, -1, 1, 1);

            ac = new AddonComponent(6792);
            ac.Name = "market stand";
            AddComponent(ac, -1, 0, 0);

            ac = new AddonComponent(2958);
            ac.Name = "market stand";
            AddComponent(ac, -1, 0, 1);

            ac = new AddonComponent(6792);
            ac.Name = "market stand";
            AddComponent(ac, 1, 0, 0);
            ac = new AddonComponent(2958);
            ac.Name = "market stand";
            AddComponent(ac, 1, 0, 1);
            ac = new AddonComponent(2958);
            ac.Name = "market stand";
            AddComponent(ac, 0, 0, 1);
            ac = new AddonComponent(6791);
            ac.Name = "market stand";
            AddComponent(ac, 2, 0, 0);
		}

		public MarketStandMercerEastAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class MarketStandMercerEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MarketStandMercerEastAddon();
			}
		}

		[Constructable]
		public MarketStandMercerEastAddonDeed()
		{
			Name = "MarketStandMercerEast";
		}

		public MarketStandMercerEastAddonDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}