using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class MinocArcher : BaseRanger
	{ 


		[Constructable] 
		public MinocArcher() : base( AIType.AI_Archer, FightMode.Weakest, 15, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Archer"; 

			AddItem( new Crossbow() );
			AddItem( new Boots() );
			AddItem( new Bandana(248) );
			AddItem( new Cloak(248) );
			StuddedGloves gloves = new StuddedGloves();
			gloves.Hue = 1175;
			AddItem(gloves);
			AddItem( new BodySash(248) );

			SetStr( 1200, 1200 );
			SetDex( 250, 250 );
			SetInt( 61, 75 );

			SetSkill( SkillName.Anatomy, 120.0, 120.0 );
			SetSkill( SkillName.Archery, 120.0, 120.0 );
			SetSkill( SkillName.Tactics, 120.0, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0, 120.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				LeatherSkirt legs = new LeatherSkirt();
				legs.Hue = 1175;
				AddItem(legs);
				FemaleStuddedChest chest = new FemaleStuddedChest();
				chest.Hue = 1175;
				AddItem(chest);
				
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" );

				StuddedChest chest = new StuddedChest();
				chest.Hue = 1175;
				AddItem(chest);
				StuddedLegs legs = new StuddedLegs();
				legs.Hue = 1175;
				AddItem(legs);
				StuddedArms arms = new StuddedArms();
				arms.Hue = 1175;
				AddItem(arms);

			}
			
			Utility.AssignRandomHair( this );
		}

		public MinocArcher( Serial serial ) : base( serial ) 
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