using System; 
using System.Collections; 
using Server.Misc; 
using Server.Items; 
using Server.Mobiles; 

namespace Server.Mobiles 
{ 
	public class ArcherRanger : BaseRanger
	{ 


		[Constructable] 
		public ArcherRanger() : base( AIType.AI_Archer, FightMode.Weakest, 15, 5, 0.1, 0.2 ) 
		{ 
			Title = "the Ranger"; 

			AddItem( new Bow() );
			AddItem( new Boots() );
			AddItem( new Bandana(767) );
			AddItem( new Cloak(767) );
			AddItem( new LeatherGloves() );
			AddItem( new BodySash(767) );

            SetStr(70, 150);
            SetDex(60, 200);
            SetInt(125, 150);
            ActiveSpeed = 0.2;
            PassiveSpeed = 0.1;

            SetHits(100, 250);

            SetDamage(30, 40);

            SetSkill( SkillName.Anatomy, 120.0, 120.0 );
			SetSkill( SkillName.Archery, 120.0, 120.0 );
			SetSkill( SkillName.Tactics, 120.0, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0, 120.0 );

			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
				
				AddItem( new LeatherSkirt() );
				AddItem( new FemaleLeatherChest() );
				
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" );

				AddItem(new LeatherChest());
				AddItem(new LeatherLegs());
				AddItem(new LeatherArms());

			}
			
			Utility.AssignRandomHair( this );
		}

		public ArcherRanger( Serial serial ) : base( serial ) 
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