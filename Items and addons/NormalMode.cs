using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class NormalModeDeed : Item
	{
		public override string DefaultName
		{
			get { return "Difficulty Setting - hover for details"; }
		}

		[Constructable]
		public NormalModeDeed() : base( 0x14F0 )
		{
			base.Weight = 1.0;
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );

			list.Add( "You are currently playing unrestricted, how the game was meant to be played.");
			list.Add( "This mode offers some benefits and your actions will have greater impact on the world.");
			list.Add( "Be warned that you may lose skill points in certian circumstances when unrestricted." ); 
			list.Add( "If you don't want to lose skills at all, double click this globe to restrict your account.");
			list.Add( "You will have lower total stats, and be restricted in how you affect the world.");
			list.Add( "This choice is permanent.  Choose Wisely." );
		
		}

		public NormalModeDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if (from is PlayerMobile)
			{
				if ( !((PlayerMobile)from).NormalMode )
				{
					((PlayerMobile)from).NormalMode = true;
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You choose to play unrestricted.  Good Choice."  ) );
					from.StatCap = 250;
				}
				else if (((PlayerMobile)from).NormalMode)
				{
					((PlayerMobile)from).NormalMode = false;
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You choose to restrict your affect on the balance."  ) );
					from.StatCap = 225;
				}
					
			}
				
		}
	}
}


