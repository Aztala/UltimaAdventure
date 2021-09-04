using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x14EF, 0x14F0 )]
	public class MonsterContract : Item
	{
		private int m_monster;
		private int reward;
		private int m_amount;
		private int m_tamed;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Monster
		{
			get{ return m_monster; }
			set{ m_monster = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Reward
		{
			get{ return reward; }
			set{ reward = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountToTame
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountTamed
		{
			get{ return m_tamed; }
			set{ m_tamed = value; }
		}
		
		[Constructable]
		public MonsterContract() : base( 0x14EF )
		{
			Weight = 1;
			Movable = true;
			Monster = MonsterContractType.Random();
			int price = MonsterContractType.Get[Monster].Rarety ;
			if (price <= 25)
				AmountToTame = Utility.RandomMinMax( 15, 30 );
			else if (price <= 50)
				AmountToTame = Utility.RandomMinMax( 10, 25 );	
			else if (price <= 75)
				AmountToTame = Utility.RandomMinMax( 7, 20 );	
			else if (price <= 75)
				AmountToTame = Utility.RandomMinMax( 5, 15 );	
			else if (price <= 100)
				AmountToTame = Utility.RandomMinMax( 4, 10 );		
			else if (price >= 100)
				AmountToTame = Utility.RandomMinMax( 2, 8 );			
			//double postprice = ((double)price / 125) * 100;
			//double scalar = 0.50 + ( (double)AetherGlobe.DoomCurse / 200000);
			//Reward = (int)((price * (price/1.25)) * scalar) * AmountToTame; old value method
			Reward = GetValue( price ) * AmountToTame ;
			Name = "Contract: " + AmountToTame + " " + MonsterContractType.Get[Monster].Name;
			AmountTamed = 0;
		}
		
		[Constructable]
		public MonsterContract( int monster, int atk, int gpreward ) : base( 0x14F0 )
		{
			Weight = 1;
			Movable = true;
			Monster = monster;
			AmountToTame = atk;
			Reward = gpreward;
			Name = "Contract: " + AmountToTame + " " + MonsterContractType.Get[Monster].Name;
			AmountTamed = 0;
		}
		
		[Constructable]
		public MonsterContract( int monster, int ak, int atk, int gpreward ) : this( monster,atk,gpreward )
		{
			AmountTamed = ak;
		}
		
		public override void OnDoubleClick( Mobile from )
		{
			if( IsChildOf( from.Backpack ) )
			{
				from.SendGump( new MonsterContractGump( from, this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
			}
		}

		public int GetValue( int value)
		{

			double basevalue = value;
			if (basevalue >= 125)
			{
				basevalue = 124;
			}

			double final = 0;
			double step = 10;
			double factorial = 1/ ((125-basevalue)/(basevalue*15));

			if (basevalue < step)
				final = basevalue * factorial;				
			else 
			{	
				while ( basevalue > 0 )
				{
					if (basevalue > step)
					{
						basevalue -= step;
						final += step * factorial;
								
					}
					else
					{
						final += basevalue * factorial;
						basevalue = 0;
					}
				}
			}		

			double petprice = final;
			petprice *= ((double)Misc.MyServerSettings.GetGoldCutRate(null, this)/100);// tie it to the doomcurse

			petprice *= 1 + ( Utility.RandomMinMax(15, 35) /100 ); // premium for doing bods over animal broker
			return (int)petprice;

		}

		public MonsterContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( m_monster );
			writer.Write( reward );
			writer.Write( m_amount );
			writer.Write( m_tamed );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_monster = reader.ReadInt();
			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_tamed = reader.ReadInt();
		}
	}
}
