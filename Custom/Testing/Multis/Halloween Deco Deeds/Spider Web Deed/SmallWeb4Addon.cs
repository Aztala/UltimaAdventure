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
	public class SmallWeb4Addon : BaseAddon
	{
		public override BaseAddonDeed Deed
		{
			get
			{
				return new SmallWeb4AddonDeed();
			}
		}

		[ Constructable ]
		public SmallWeb4Addon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 4309 );
			AddComponent( ac, 0, 0, 0 );

		}

		public SmallWeb4Addon( Serial serial ) : base( serial )
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

	public class SmallWeb4AddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new SmallWeb4Addon();
			}
		}

		[Constructable]
		public SmallWeb4AddonDeed()
		{
			Name = "SmallWeb4";
		}

		public SmallWeb4AddonDeed( Serial serial ) : base( serial )
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