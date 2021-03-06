
////////////////////////////////////////
//                                    //
//      Generated by CEO's YAAAG      //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarketStandBeeSouthAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {2540, 0, 0, 3}, {5171, 0, -1, 3}, {5167, 0, 0, 0}// 9	10	11	
			, {5167, 0, -1, 2}, {5159, 0, 1, 3}, {5175, 0, -1, 3}// 12	13	14	
			, {5162, 0, 2, 0}, {5160, 0, 2, 8}// 15	16	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new MarketStandBeeSouthAddonDeed();
			}
		}

		[ Constructable ]
		public MarketStandBeeSouthAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( 2938, 0, 0, 1, 542, -1, "market stand" );// 1
			AddComplexComponent( 1445, 0, -1, 5, 542, -1, "market stand" );// 2
			AddComplexComponent( 6787, 0, -1, 0, 542, -1, "market stand" );// 3
			AddComplexComponent( 2938, 0, -1, 1, 542, -1, "market stand" );// 4
			AddComplexComponent( 1445, 0, 0, 5, 542, -1, "market stand" );// 5
			AddComplexComponent( 1445, 0, 1, 5, 542, -1, "market stand" );// 6
			AddComplexComponent( 6787, 0, 1, 0, 542, -1, "market stand" );// 7
			AddComplexComponent( 2938, 0, 1, 1, 542, -1, "market stand" );// 8

		}

		public MarketStandBeeSouthAddon( Serial serial ) : base( serial )
		{
		}

        public void AddComplexComponent(int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(item, xoffset, yoffset, zoffset, hue, lightsource, null);
        }

        public void AddComplexComponent(int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class MarketStandBeeSouthAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new MarketStandBeeSouthAddon();
			}
		}

		[Constructable]
		public MarketStandBeeSouthAddonDeed()
		{
			Name = "MarketStandBeeSouth";
		}

		public MarketStandBeeSouthAddonDeed( Serial serial ) : base( serial )
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