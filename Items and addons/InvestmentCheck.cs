using System;
using System.Globalization;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class InvestmentCheck : Item
	{
		private int m_Worth;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Worth
		{
			get{ return m_Worth; }
			set{ m_Worth = value; InvalidateProperties(); }
		}


		[Constructable]
		public InvestmentCheck( int worth ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 0x224;
			Movable = false;
			Name = "Investment Certificate";
			m_Worth = worth;
		}

		public override bool DisplayLootType{ get{ return Core.AOS; } }

		public override int LabelNumber{ get{ return 1041361; } }

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string worth;

			if ( Core.ML )
				worth = m_Worth.ToString( "N0", CultureInfo.GetCultureInfo( "en-US" ) ); // what is this?? from bankcheck.cs
			else
				worth = m_Worth.ToString();

			list.Add( 1060738, worth ); // value: ~1_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			from.Send( new MessageLocalizedAffix( Serial, ItemID, MessageType.Label, 0x3B2, 3, 1041361, "", AffixType.Append, String.Concat( " ", m_Worth.ToString() ), "" ) ); //an investment certificate (and amount)
		}

		public override void OnDoubleClick( Mobile from )		
		{
			BankBox box = from.FindBankNoCreate();
			//Item bag = ((Item)this).Layer;
			if( box != null && IsChildOf( box ) ) // bag.Layer = InterestBag && bag.Layer = BankBox not working:  check if the investmentcheck is in an interestbag, and if the interestbag is in a bankbox
			{
				int payback = m_Worth;
				
				//Container box = from.BankBox; //assign player's bankbox
				box.DropItem(new BankCheck( payback )); //drop a check of the investment amount in the bankbox
				from.SendMessage( "You withdraw " + payback + " gold from your investment" );				
				this.Delete();
			}
			else
			{
				from.SendLocalizedMessage( 1047026 ); // That must be in your bank box to use it.
			}
		}
		
		public InvestmentCheck( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Worth );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Worth = reader.ReadInt();
					break;
				}
			}
		}
	}
}