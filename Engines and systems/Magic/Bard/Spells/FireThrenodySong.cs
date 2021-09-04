using System;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.OneTime.Events;
using System.Collections;

namespace Server.Spells.Song
{
	public class FireThrenodySong : ModifierSong
	{
		
		private static SpellInfo m_Info = new SpellInfo(
				"Fire Threnody", "*plays a fire threnody*",
				//SpellCircle.First,
				//212,9041
				-1
			);
		
		public FireThrenodySong( Mobile caster, Item scroll) : base( caster, scroll, m_Info )
		{
			ModType = 2;
			ExpiryMessage = "The effect of the fire threnody wears off.";
			IsHkCheck = true;
		}

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 5 ); } }
		public override double RequiredSkill{ get{ return 65.0; } }
		public override int RequiredMana{ get{ return 40; } }

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			Spellbook book = Spellbook.Find(Caster, -1, SpellbookType.Song);		
			if (book != null) {
				SongBook = (SongBook)book;
				bool sings = false;
				Targets = new ArrayList(){ m };	
				if ( !Caster.CanSee( m ) )
				{
					Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
				} 
				else if (CheckHSequence( m ) && this.CanCast() && !base.SongIsAlreadyOnTarget(m))
				{ 
					sings = true;

					Mobile source = Caster;	;
					SpellHelper.Turn( source, m );

					int duration = this.CalculateDurationByFame(m, false);
					if (m is PlayerMobile) {
						duration = this.CalculateDurationByMagicResist(m);
					}
					Duration = duration;
				    int amount = (MusicSkill( Caster ) / 20);
					if ( base.CheckSlayer( SongBook.Instrument, m ))
					{
					    amount = amount * 2;
					}
					Modification =  new ResistanceMod( ResistanceType.Fire, (amount * -1) );
					m.AddResistanceMod( Modification );
					m.FixedParticles( 0x374A, 10, 30, 5013, 0x489, 2, EffectLayer.Waist );
					SongEffect songEffect = new SongEffect(m.Serial, this);
					base.AddSongEffect(songEffect);
					OneTimeSecEvent.SecTimerTick += SecondTimerTick;
				}
				BardFunctions.UseBardInstrument( SongBook.Instrument, sings, Caster );
				FinishSequence();
			}
		}
		private class InternalTarget : Target
		{
			private FireThrenodySong m_Owner;

			public InternalTarget( FireThrenodySong owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
