using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class SkaraBraeMage : BaseRanger 
	{ 

		[Constructable] 
		public SkaraBraeMage() : base( AIType.AI_Mage, FightMode.Weakest, 10, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Mage"; 

			AddItem( new Boots() );
			AddItem( new WizardsHat(692) );
			AddItem( new Cloak(692) );
			AddItem( new StuddedGloves() );
			AddItem( new Robe(667) );

			SetStr( 1000, 1000 );
			SetDex( 250, 250 );
			SetInt( 250, 250 );

			SetSkill( SkillName.MagicResist, 120.0,120.0 );
			SetSkill( SkillName.Magery, 120.0, 120.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 120.0,120.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new LeatherSkirt() );
				AddItem( new FemaleStuddedChest() );
				


			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
				
				AddItem( new StuddedChest());
				AddItem( new StuddedLegs());
				AddItem( new StuddedArms());
				


			}

			Utility.AssignRandomHair( this );
		}


		public SkaraBraeMage( Serial serial ) : base( serial ) 
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
	} 
}   