
////////////////////////////////////////
//                                    //
//   Generated by CEO's YAAAG - V1.2  //
// (Yet Another Arya Addon Generator) //
//                                    //
////////////////////////////////////////
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CoffinGlasstopOpenEastAddon : BaseAddon
	{
        private static int[,] m_AddOnSimpleComponents = new int[,] {
			  {3790, 1, 0, 2}, {3786, 1, 1, 2}, {3787, 1, 0, 2}// 32	33	34	
			, {3788, 1, 2, 2}, {3789, 1, 1, 2}, {3790, 1, 1, 2}// 35	36	37	
			, {3788, 1, 1, 2}, {3789, 1, 2, 2}// 38	39	
		};

 
            
		public override BaseAddonDeed Deed
		{
			get
			{
				return new CoffinGlasstopOpenEastAddonDeed();
			}
		}

		[ Constructable ]
		public CoffinGlasstopOpenEastAddon()
		{

            for (int i = 0; i < m_AddOnSimpleComponents.Length / 4; i++)
                AddComponent( new AddonComponent( m_AddOnSimpleComponents[i,0] ), m_AddOnSimpleComponents[i,1], m_AddOnSimpleComponents[i,2], m_AddOnSimpleComponents[i,3] );


			AddComplexComponent( (BaseAddon) this, 1981, 1, 2, 0, 1107, -1, "Coffin", 1);// 1
			AddComplexComponent( (BaseAddon) this, 1981, 1, 1, 0, 1107, -1, "Coffin", 1);// 2
			AddComplexComponent( (BaseAddon) this, 1981, 1, 0, 0, 1107, -1, "Coffin", 1);// 3
			AddComplexComponent( (BaseAddon) this, 11718, 0, 2, 2, 1107, -1, "Coffin", 1);// 4
			AddComplexComponent( (BaseAddon) this, 11718, 1, 2, 2, 1107, -1, "Coffin", 1);// 5
			AddComplexComponent( (BaseAddon) this, 11718, 0, -1, 2, 1107, -1, "Coffin", 1);// 6
			AddComplexComponent( (BaseAddon) this, 11718, 1, -1, 2, 1107, -1, "Coffin", 1);// 7
			AddComplexComponent( (BaseAddon) this, 2253, 1, 2, 2, 1102, -1, "Coffin", 1);// 8
			AddComplexComponent( (BaseAddon) this, 2254, 1, -1, 2, 1102, -1, "Coffin", 1);// 9
			AddComplexComponent( (BaseAddon) this, 2252, 0, 2, 2, 1102, -1, "Coffin", 1);// 10
			AddComplexComponent( (BaseAddon) this, 2252, 0, 1, 2, 1102, -1, "Coffin", 1);// 11
			AddComplexComponent( (BaseAddon) this, 2252, 1, 1, 2, 1102, -1, "Coffin", 1);// 12
			AddComplexComponent( (BaseAddon) this, 2252, 0, 0, 2, 1102, -1, "Coffin", 1);// 13
			AddComplexComponent( (BaseAddon) this, 2252, 1, 0, 2, 1102, -1, "Coffin", 1);// 14
			AddComplexComponent( (BaseAddon) this, 924, 1, 2, 7, 1107, -1, "Coffin", 1);// 15
			AddComplexComponent( (BaseAddon) this, 925, 1, -1, 7, 1107, -1, "Coffin", 1);// 16
			AddComplexComponent( (BaseAddon) this, 926, 1, 1, 7, 1107, -1, "Coffin", 1);// 17
			AddComplexComponent( (BaseAddon) this, 926, 1, 0, 7, 1107, -1, "Coffin", 1);// 18
			AddComplexComponent( (BaseAddon) this, 926, 0, 2, 7, 1107, -1, "Coffin", 1);// 19
			AddComplexComponent( (BaseAddon) this, 926, 0, 0, 7, 1107, -1, "Coffin", 1);// 20
			AddComplexComponent( (BaseAddon) this, 926, 0, 1, 7, 1107, -1, "Coffin", 1);// 21
			AddComplexComponent( (BaseAddon) this, 7388, 1, 0, 10, 1150, -1, "Coffin", 1);// 22
			AddComplexComponent( (BaseAddon) this, 7388, 1, 1, 10, 1150, -1, "Coffin", 1);// 23
			AddComplexComponent( (BaseAddon) this, 7388, 1, 2, 10, 1150, -1, "Coffin", 1);// 24
			AddComplexComponent( (BaseAddon) this, 2253, 1, 2, 9, 1102, -1, "Coffin", 1);// 25
			AddComplexComponent( (BaseAddon) this, 2254, 1, -1, 9, 1102, -1, "Coffin", 1);// 26
			AddComplexComponent( (BaseAddon) this, 2252, 0, 2, 9, 1102, -1, "Coffin", 1);// 27
			AddComplexComponent( (BaseAddon) this, 2252, 0, 1, 9, 1102, -1, "Coffin", 1);// 28
			AddComplexComponent( (BaseAddon) this, 2252, 1, 1, 9, 1102, -1, "Coffin", 1);// 29
			AddComplexComponent( (BaseAddon) this, 2252, 0, 0, 9, 1102, -1, "Coffin", 1);// 30
			AddComplexComponent( (BaseAddon) this, 2252, 1, 0, 9, 1102, -1, "Coffin", 1);// 31

		}

		public CoffinGlasstopOpenEastAddon( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
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

	public class CoffinGlasstopOpenEastAddonDeed : BaseAddonDeed
	{
		public override BaseAddon Addon
		{
			get
			{
				return new CoffinGlasstopOpenEastAddon();
			}
		}

		[Constructable]
		public CoffinGlasstopOpenEastAddonDeed()
		{
			Name = "CoffinGlasstopOpenEast";
		}

		public CoffinGlasstopOpenEastAddonDeed( Serial serial ) : base( serial )
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