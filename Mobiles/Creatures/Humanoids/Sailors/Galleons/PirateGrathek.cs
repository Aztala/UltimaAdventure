using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Multis;

namespace Server.Mobiles 
{
	public class PirateGrathek : BasePirate 
	{
		[Constructable] 
		public PirateGrathek()
		{
			Name = NameList.RandomName( "lizardman" );
			Title = "the grathek pirate";
			Body = 534;
			BaseSoundID = 417;

			AI = AIType.AI_Melee;
			FightMode = FightMode.Closest;
			if ( Utility.RandomBool() ){ ship = new GalleonExotic(); } else { ship = new GalleonBarbarian(); }
			ship.Hue = ShipColor( "reptile" );

			SetStr( 536, 585 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 322, 351 );
			SetMana( 0 );

			SetDamage( 16, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 48;
			healme = "Slee heal me slak scurvs!";
		}

		public PirateGrathek( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public override int BreathPhysicalDamage{ get{ return 0; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 100; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x3F; } }
		public override int BreathEffectSound{ get{ return 0x658; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 18 ); }
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }
	}
}