// Created by Script Creator

using System;
using Server.Items;

namespace Server.Mobiles

              {

				  		

              [CorpseName( " corpse of the Caveman" )]
              public class Caveman : BaseCreature
              {
				  public override bool CanAngerOnTame { get { return true; } }
				private Timer m_Timer;
                                 [Constructable]
                                    public Caveman() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
                            {
                                               
					                           Body = 400;
						                       Female = false; 
						                       Hue = 0;
						                       Title = "The Caveman";
                                               //Body = 149; // Uncomment these lines and input values
                                               //BaseSoundID = 0x4B0; // To use your own custom body and sound.
                                               SetStr( 300, 600 );
                                               SetDex( 200, 400 );
                                               SetInt( 100, 200 );
                                               SetHits( 1000, 3000 );
                                               SetDamage( 20, 25 );
                                               SetDamageType( ResistanceType.Cold, 70 );
                                               SetDamageType( ResistanceType.Fire, 70 );
                                               SetDamageType( ResistanceType.Energy, 70 );
                                               SetDamageType( ResistanceType.Poison, 70 );

                                               SetResistance( ResistanceType.Physical, 60 );
                                               SetResistance( ResistanceType.Cold, 90 );
                                               SetResistance( ResistanceType.Fire, 90 );
                                               SetResistance( ResistanceType.Energy, 90 );
                                               SetResistance( ResistanceType.Poison, 90 );

			SetSkill( SkillName.Fencing, 60.0, 80.0 );
			SetSkill( SkillName.Macing, 60.0, 80.0 );
			SetSkill( SkillName.MagicResist, 60.0, 80.0 );
			SetSkill( SkillName.Swords, 60.0, 80.0 );
			SetSkill( SkillName.Tactics, 60.0, 80.0 );
			SetSkill( SkillName.Wrestling, 60.0, 80.0 );

             ControlSlots = 2;
             MinTameSkill = 20;
             Tamable = true;

			//m_Timer = new TeleportTimer( this );
			//m_Timer.Start();



            Fame = 15000;
            Karma = 15000;
            VirtualArmor = 65;

			PackGold( 5120, 5130 );


			

			Item Shirt = new Shirt(); 
			Shirt.Movable = false;
			Shirt.Hue = 351; 
			AddItem( Shirt );

			Item FurSarong = new FurSarong(); 
			FurSarong.Movable = false;
			FurSarong.Hue = 351; 
			AddItem( FurSarong );

			Item Club = new Club(); 
			Club.Movable = false;
			Club.Hue = 0; 
			AddItem( Club );

			


           



                            }
                                 public override void GenerateLoot()
		{
			switch ( Utility.Random( 350 ))
			{
				case 0: PackItem( new Flyswatter() ); break;
				case 1: PackItem( new CavemanClub() ); break;
				case 2: PackItem( new CavemanShirt() ); break;
				case 3: PackItem( new CavemanNeck() ); break;
				case 4: PackItem( new CavemanLoincloth() ); break;
				
				
				
		 }
		}
		
		        //public override bool HasBreath{ get{ return true ; } }
				//public override int BreathFireDamage{ get{ return 11; } }
				//public override int BreathColdDamage{ get{ return 11; } }
//                public override bool IsScaryToPets{ get{ return true; } }
				//public override bool AutoDispel{ get{ return true; } }
                public override bool BardImmune{ get{ return true; } }
                public override bool Unprovokable{ get{ return true; } }
               // public override Poison HitPoison{ get{ return Poison. Lethal ; } }
                //public override bool AlwaysMurderer{ get{ return true; } }
//				public override bool IsScaredOfScaryThings{ get{ return false; } }






		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( from is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)from;

				if ( bc.Controlled || bc.BardTarget == this )
					damage = 0; // Immune to pets and provoked creatures
			}
		}
		
		/*
		private class TeleportTimer : Timer
		{
			private Mobile m_Owner;

			private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};

			public TeleportTimer( Mobile owner ) : base( TimeSpan.FromSeconds( 1.0 ), TimeSpan.FromSeconds( 1.1 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				Map map = m_Owner.Map;

				if ( map == null )
					return;

				if ( 0.5 < Utility.RandomDouble() )
					return;

				Mobile toTeleport = null;

				foreach ( Mobile m in m_Owner.GetMobilesInRange( 16 ) )
				{
					if ( m != m_Owner && m.Player && m_Owner.CanBeHarmful( m ) && m_Owner.CanSee( m ) )
					{
						toTeleport = m;
						break;
					}
				}

				if ( toTeleport != null )
				{
					int offset = Utility.Random( 8 ) * 2;

					Point3D to = m_Owner.Location;

					for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = m_Owner.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = m_Owner.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, m_Owner.Z ) )
						{
							to = new Point3D( x, y, m_Owner.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Mobile m = toTeleport;

					Point3D from = m.Location;

					m.Location = to;

					Server.Spells.SpellHelper.Turn( m_Owner, toTeleport );
					Server.Spells.SpellHelper.Turn( toTeleport, m_Owner );

					m.ProcessDelta();

					Effects.SendLocationParticles( EffectItem.Create( from, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, m.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					m.PlaySound( 0x1FE );

					m_Owner.Combatant = toTeleport;
				}
			}
		}*/


public Caveman( Serial serial ) : base( serial )
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
    }
}
