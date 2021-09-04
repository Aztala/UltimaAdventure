using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.Targeting;

namespace Server.Items
{
    public class AetherGlobe : Item
    {
		public static int DoomCurse; // This is a global property of the game
		public static List<AetherGlobe> WorldGlobes = new List<AetherGlobe>();

		[CommandProperty(AccessLevel.GameMaster)]
		public static int Multiplier { get; set; }

		private static DateTime lastchanged;
		public static double delta1;
		public static double delta2;
		public static double delta3;
		public static double rateofchange;
		public static int changeint;
		public static int VendorCurse;
		
		[CommandProperty(AccessLevel.GameMaster)]
		public static int olddoomcurse { get; set; }

		public static bool killbonus = false;
		public static double rateofreturn;
		public static Mobile EvilChamp;
		public static Mobile GoodChamp;

		// infected invasion variables
		[CommandProperty(AccessLevel.GameMaster)]
		public static double intensity { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static int invasionstage { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static String carrier { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public static String general { get; set; }

		[CommandProperty( AccessLevel.GameMaster )]
		public int DoomCurseLevel
		{
			get
			{
			    return DoomCurse; 
			}
			set
			{ 
			    DoomCurse = value; 
			    AetherGlobe.UpdateColor(); 
			    InvalidateProperties(); 
			}
		}

		[Constructable]
		public AetherGlobe () : base( 0x115F )
		{
			Movable = false;
			Name = "AEther Globe";
			Visible = true;
			AetherGlobe.DoomCurse = 0;
			Multiplier = 0;
			lastchanged = DateTime.Now;
			olddoomcurse = 0;
			rateofchange = 0;
			delta1 = 0;
			delta2 = 0;
			delta3 = 0;
			rateofreturn = 0;
			EvilChamp = null;
			GoodChamp = null;
			VendorCurse = DoomCurse;

			carrier = null;
			general = null;
			invasionstage = 0;
			intensity = 0;

			WorldGlobes.Add( this );
			AetherGlobe.UpdateColor();
		}
		
		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
		
			if (DoomCurse > 90000)
				list.Add( "Evil has taken over the lands" ); 
			else if (DoomCurse > 50000)
				list.Add( "Evil is everpresent" ); 						
			else if (DoomCurse > 20000)
				list.Add( "Evil is taking hold" ); 	
			else 
				list.Add( "Evil here is weak" );
			
			if (AetherGlobe.changeint < 0)
				list.Add( "and the forces of good have pushed it back." );
			else if (AetherGlobe.changeint > 0)
				list.Add( "and the forces of good have lost some ground." );
			else if (AetherGlobe.changeint == 0)
				list.Add( "and there was a stalemate." );			
		}
		
		public override void OnDoubleClick( Mobile from )		
		{

			if (from is PlayerMobile && from != null)
			{
				if (((PlayerMobile)from).BalanceEffect == 0)
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You are neutral to the balance."  ) );

				else if (((PlayerMobile)from).BalanceEffect > 0)
				{
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You've tipped the balance towards Evil by " + ((PlayerMobile)from).BalanceEffect + " points."  ) );
					if ( AetherGlobe.EvilChamp == from)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Your ruthless aura casts a shadow over these lands."  ) );
					else if (AetherGlobe.EvilChamp != null)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You feel " + AetherGlobe.EvilChamp.Name + " " + AetherGlobe.EvilChamp.Title + " casts a shadow over you." ) );
				}
				else if (((PlayerMobile)from).BalanceEffect < 0)
				{
					from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You've tipped the balance towards Good by " + Math.Abs(((PlayerMobile)from).BalanceEffect) + " points."  ) );
					if ( AetherGlobe.GoodChamp == from)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "You are leading the charge against evil and corruption!"  ) );
					else if (AetherGlobe.GoodChamp != null)
						from.PublicOverheadMessage( MessageType.Regular, 0, false, string.Format ( "Your champion is " + AetherGlobe.GoodChamp.Name + " " + AetherGlobe.GoodChamp.Title  ) );
				}
					
			}

		}

		public AetherGlobe(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{			

			base.Serialize( writer );
			writer.Write( (int) 3 ); // version
			
			writer.Write( (int) AetherGlobe.DoomCurse);
			writer.Write( (int) Multiplier);
			writer.Write( (DateTime)lastchanged );
			writer.Write( (double) AetherGlobe.delta1 );
			writer.Write( (double) AetherGlobe.delta2 );
			writer.Write( (double) AetherGlobe.delta3 );
			writer.Write( (double) AetherGlobe.rateofchange );
			writer.Write( (int) AetherGlobe.olddoomcurse );
			writer.Write( (int) AetherGlobe.changeint );
			
			writer.Write( (double) AetherGlobe.rateofreturn );
			writer.Write( (Mobile) AetherGlobe.EvilChamp );
			writer.Write( (Mobile) AetherGlobe.GoodChamp );

			writer.Write( (int) invasionstage );
			writer.Write( (double) intensity );
			writer.Write( (String) carrier );
			writer.Write( (String) general );

		}

		public override void Deserialize( GenericReader reader )
		{
						
			base.Deserialize( reader );
			int version = reader.ReadInt();
			
			AetherGlobe.DoomCurse = reader.ReadInt();
			AetherGlobe.Multiplier = reader.ReadInt();
			lastchanged = reader.ReadDateTime();
			AetherGlobe.delta1 = reader.ReadDouble();
			AetherGlobe.delta2 = reader.ReadDouble();	
			AetherGlobe.delta3 = reader.ReadDouble();
			AetherGlobe.rateofchange = reader.ReadDouble();	
			AetherGlobe.olddoomcurse = reader.ReadInt();		
			AetherGlobe.changeint = reader.ReadInt();
			
			rateofreturn = reader.ReadDouble();
			EvilChamp = reader.ReadMobile();
			GoodChamp = reader.ReadMobile();

			if (!WorldGlobes.Contains( this ))
			    WorldGlobes.Add( this );

			AetherGlobe.UpdateColor();

			if (version >= 2)
			{
				invasionstage = reader.ReadInt();
				intensity = reader.ReadDouble();
				carrier = reader.ReadString();
			}
			if (version >= 3)
			{
				general = reader.ReadString();
			}
		}

		public override void OnAfterSpawn()
		{
			if (!WorldGlobes.Contains( this ))
			    WorldGlobes.Add( this );

			base.OnAfterSpawn();
		}

		public override void OnDelete()
		{
			if (WorldGlobes.Contains( this ))
			    WorldGlobes.Remove( this );

			base.OnDelete();
		}


		public static void UpdateColor() // updates color of all aetherglobes based on AetherGlobe.DoomCurse level
		{
			foreach( AetherGlobe globe in WorldGlobes )
			{
			    if (AetherGlobe.DoomCurse <= 10000) // green colors will be changed later, placeholder
				globe.Hue = 75; 
			    else if (AetherGlobe.DoomCurse <= 20000)
				globe.Hue = 66;				
			    else if (AetherGlobe.DoomCurse <= 30000)
				globe.Hue = 59;	
			    else if (AetherGlobe.DoomCurse <= 40000)
				globe.Hue = 56;	
			    else if (AetherGlobe.DoomCurse <= 50000)
				globe.Hue = 54;	
			    else if (AetherGlobe.DoomCurse <= 60000)
				globe.Hue = 52;	
			    else if (AetherGlobe.DoomCurse <= 70000)
				globe.Hue = 45;	
			    else if (AetherGlobe.DoomCurse <= 80000)
				globe.Hue = 43;	
			    else if (AetherGlobe.DoomCurse <= 90000)
				globe.Hue = 38;			
			    else if (AetherGlobe.DoomCurse <= 100000) // red
				globe.Hue = 1793;		

			    globe.InvalidateProperties();
			}
		}

		public static void ApplyCurse(Mobile m, Map frommap, Map tomap, int type)
		{

			if ( !(m is PlayerMobile) || m == null )
				return;

			if (((PlayerMobile)m).sbmaster)
				return;
				
			double cursechance;
			int balance = 0;
			
			if ( m.Karma >=0)
				balance = AetherGlobe.DoomCurse;
			else if ( m.Karma < 0 )
				balance = 100000 - AetherGlobe.DoomCurse;
			
			if ( ((double)balance / 125000.0) <= ((double)MyServerSettings.curseincrease() / 100000) )
				balance = MyServerSettings.curseincrease();

			cursechance = (( (double)balance / 100000 ) * ( (double)m.Karma / 15000)) /1.5   ;

			if (frommap != tomap) // recalling/gating to another facet is dangerous
			cursechance *= 5;

			if (type == 1) // recall
			cursechance *= 0.90;

			if (type == 2) // gate 
			cursechance /= 2;

			if (type == 3) // chivalry sacred journey
			cursechance *= 0.75;

			cursechance *= (1+ Utility.RandomDouble()); // randomize it a bit

			if (cursechance <= 0)
			cursechance = 0;

			if (cursechance >= 0.91)
			cursechance = 0.91;

			if (Utility.RandomDouble() <= cursechance) // check if curse is applied
			{
				
				
				if ( AetherGlobe.GoodChamp == m || AetherGlobe.EvilChamp == m )
					return;
				
					double loosing = 0;
					
					if (m.Karma >= 0)
						loosing = ( ( (double)AetherGlobe.DoomCurse / 200000.0 ) * ( ((double)m.Karma / 15000) ) ) ;
					else 
						loosing = ( ( (double)(100000-AetherGlobe.DoomCurse) / 200000.0) * ( ((double)Math.Abs(m.Karma) / 15000) ) ) ;
	
					if ( m.Fame > 0 ) // FAME LOSS
					{
						int amount = (int)((double)m.Fame * loosing );
						if ( m.Fame - amount < 0 ){ amount = m.Fame; }
						if ( m.Fame < 1 ){ m.Fame = 0; }
						Misc.Titles.AwardFame( m, -amount, true );
					}

					//karma loss
						int amounts = (int)((double)m.Karma * loosing);
						if ( m.Karma - amounts < 0 ){ amounts = m.Karma; }
						if ( m.Karma +1 == 0 || m.Karma -1 == 0){ m.Karma = 0; }
						Misc.Titles.AwardKarma( m, -amounts, true );
										
				
					if ( !(((PlayerMobile)m).NormalMode) )
					{
						m.SendMessage( "You reappear, but shaken." );
						return;
					}				
				
				if (Utility.RandomBool())
					m.SendMessage( "Crossing the voids of the worlds has weakened you...  " );					
				else
					m.SendMessage( "The Aether's curse corrupts your being...  " );	

				for( int s = 0; s < m.Skills.Length; s++ )
				{
					if ( Utility.RandomDouble() <= (cursechance ) &&  ( m.Skills[s].Base * ( (100 - (cursechance/0.5) ) / 100) )  > 35 )
						m.Skills[s].Base *= ( (100 - (cursechance/0.5) ) / 100);
				}
			}
		}
		
		public static void QuestEffect( int gold, Mobile from, bool Good)
		{
			if (from == null || gold == null || gold == 0 || !(from is PlayerMobile))
				return;
			
			PlayerMobile pm = (PlayerMobile)from;
			
			if ( !pm.NormalMode )
				return;
			
			double effect = (double)gold / 1000;
			
			if ( pm.BalanceStatus == 0 )
				effect /= 2;
			
			if (effect <1 && Utility.RandomDouble()> 0.66) // for smaller gold rewards, 33% chance of 1 point 
				effect = 1;
			else if (effect <1)
				return;
			
			if (Good)
				effect = -(effect); // good quests reduce the balance		
							
			from.SendMessage("you have done a quest and should get this many balance pts:  " + ((int)effect).ToString() );

			AetherGlobe.ChangeCurse( (int)effect);
			pm.BalanceEffect += (int)effect;
			
		}
				
				
		public static void ChangeCurse( int change ) // will be called on a daily basis in taskmanager
		{

			if (change == 0 ) // change==0 means it is the daily task manager (running every 24 hours)
			{
				if ( DateTime.Now > ( lastchanged + TimeSpan.FromHours( 23.0 )) ) 
				{		
					if (AetherGlobe.DoomCurse <= 0) // doomcurse can be 0, divide by 0 check
						AetherGlobe.DoomCurse = 1;
					
					AetherGlobe.UpdateRateOfReturn(); // updating the rate of return before making changes
					
					if (AetherGlobe.olddoomcurse == 0 && AetherGlobe.DoomCurse == 1 && AetherGlobe.rateofchange == 0) //system starting out / first time being run
					{
						AetherGlobe.DoomCurse += Utility.RandomMinMax( (MyServerSettings.curseincrease() / 2), MyServerSettings.curseincrease() );						
						AetherGlobe.rateofchange = AetherGlobe.DoomCurse;
						AetherGlobe.olddoomcurse = AetherGlobe.DoomCurse;
					}
					else 
					{
						
						rateofchange = 0; //resets rateofchange from previous calculation.
						//set variables
						double olddelta = (delta1 + delta2 + delta3) / 3; // claculate average previous 3 days of rate of changes
						int oldchangeint = AetherGlobe.changeint; // set previous amount the curse went up/down by
						AetherGlobe.changeint = AetherGlobe.DoomCurse - AetherGlobe.olddoomcurse; // the amount the curse changed by (neg or positive)

						if (AetherGlobe.changeint == 0) // divide by 0 check
							AetherGlobe.changeint = 1;
						
						AetherGlobe.delta3 = delta2; // changing the deltas 
						AetherGlobe.delta2 = delta1;
						
						Console.WriteLine( "step 1 rateofchange " + rateofchange); // debug console writeline
						AetherGlobe.delta1 = (double)AetherGlobe.changeint / 100000; // to determine new delta		
						//end

						if ( AetherGlobe.DoomCurse == 1 && AetherGlobe.changeint <= 0) //curse was brought to 0, difficulty needs to be ramped a bit
						{
							AetherGlobe.Multiplier += 2; //this variable remembers the previous changes and multiplies or reduces based on activity
							AetherGlobe.rateofchange += (Math.Abs(AetherGlobe.changeint) *  AetherGlobe.Multiplier) + MyServerSettings.curseincrease();	// sets the initial increase higher some						
						}
						if ( oldchangeint > 0 && AetherGlobe.changeint > 0 && Utility.RandomBool()) // curse went up twice in a row, lower the multiplier
							AetherGlobe.Multiplier -= 1;
						else if ( oldchangeint < 0 && AetherGlobe.changeint < 0 && Utility.RandomBool()) // curse went down twice in a row, up the multiplier
							AetherGlobe.Multiplier += 1;

						Console.WriteLine( "step 2 rateofchange " + rateofchange); // debug console message
						
						if (AetherGlobe.olddoomcurse == 0)
							AetherGlobe.olddoomcurse = MyServerSettings.curseincrease();
						
						if ( ((Math.Abs((double)AetherGlobe.changeint) / 100000) < ((double)MyServerSettings.curseincrease() / 100000) ) && ( (Math.Abs(oldchangeint) / AetherGlobe.olddoomcurse) < ((double)MyServerSettings.curseincrease() / 50000) ) && !AetherGlobe.killbonus)	// curse changed by a small amount, not much movement seen last 2 days, cool it down
						{
							AetherGlobe.rateofchange += ( Math.Abs( (double)AetherGlobe.olddoomcurse - (double)AetherGlobe.DoomCurse ) /2 ) + ( ( Math.Abs(AetherGlobe.delta1) * (double)MyServerSettings.curseincrease()) - ( ( ( (AetherGlobe.changeint /3) / ( 1- AetherGlobe.delta1 ) ) /1.25)  + ( ( ((double)oldchangeint /3) / ( 1 - olddelta ) ) / 2 ) ) );
							AetherGlobe.rateofchange += (MyServerSettings.curseincrease()*(Math.Abs(changeint)/(MyServerSettings.curseincrease()*3))) * Multiplier; // apply multiplier effect.
							AetherGlobe.rateofchange *= 0.50;
							Console.WriteLine( "step 3" + rateofchange +" "+ AetherGlobe.changeint +" "+ AetherGlobe.delta1 +" "+ oldchangeint+" "+ olddelta);
						}
						else
						{
							// main calculation for the new curse
							AetherGlobe.rateofchange += ( Math.Abs((double)AetherGlobe.olddoomcurse - (double)AetherGlobe.DoomCurse ) /2 ) + ((double)MyServerSettings.curseincrease() - ( ( ( ((double)AetherGlobe.changeint /3) / ( 1- AetherGlobe.delta1  ) ) /1.25 ) + ( ( ((double)oldchangeint/3) / ( 1 - olddelta ) ) / 2 ) ) ) ;
							AetherGlobe.rateofchange += (MyServerSettings.curseincrease()*(Math.Abs(changeint)/(MyServerSettings.curseincrease()*3))) * Multiplier; // apply multiplier effect.
							AetherGlobe.rateofchange *= 0.50;
							Console.WriteLine( "step 4" + rateofchange +" "+ AetherGlobe.changeint +" "+ AetherGlobe.delta1 +" "+ oldchangeint+" "+ olddelta);
						}
						
						if ( AetherGlobe.rateofchange > 0 && oldchangeint > 0 && AetherGlobe.changeint > 0 && Utility.RandomDouble() < 0.05 ) // 5% chance of an increase being changed to a decrease 
							AetherGlobe.rateofchange = -(AetherGlobe.rateofchange);
						else if ( AetherGlobe.rateofchange < 0 && oldchangeint < 0 && AetherGlobe.changeint < 0 && Utility.RandomDouble() < 0.05 ) // 5% chance of a decrease switchign to an increase
							AetherGlobe.rateofchange = Math.Abs(AetherGlobe.rateofchange);
							
						AetherGlobe.olddoomcurse = AetherGlobe.DoomCurse; // records this value for next cycle	
						AetherGlobe.DoomCurse += (int)AetherGlobe.rateofchange;
						
						if (AetherGlobe.DoomCurse > 100000)
							AetherGlobe.DoomCurse = 95000;
						

						Console.WriteLine( "step 6" + rateofchange);
						
					}
					lastchanged = DateTime.Now;
					Console.WriteLine( "rateofreturn is " + AetherGlobe.rateofreturn + " and doomcurse changed by " + AetherGlobe.rateofchange + " and is now " + AetherGlobe.DoomCurse );	
					LoggingFunctions.LogServer( "rateofreturn is" + AetherGlobe.rateofreturn + " and doomcurse is " + AetherGlobe.DoomCurse );
				}
				else
					return;
			}
			else
				AetherGlobe.DoomCurse += change;

			if (AetherGlobe.DoomCurse > 100000)
			    AetherGlobe.DoomCurse = 95000;

			if (AetherGlobe.DoomCurse <= 0) // divide by 0 check
			    AetherGlobe.DoomCurse = 1;

			AetherGlobe.UpdateColor();
			
			

		}
		
		public static void UpdateRateOfReturn() // updates the rate of return
		{

			
			Console.WriteLine( "Updating Rate of return" );
			rateofreturn = 0;
			double risklevel = AetherGlobe.DoomCurse / 200000; // lower is better
			double multiplier = 1;
			rateofreturn =  ( (double)AetherGlobe.olddoomcurse  - (double)AetherGlobe.DoomCurse) / 200000; // base everything on how much the curse changed by

			Console.WriteLine( "ROR Step1 " + rateofreturn + " " + "balance was " + (double)AetherGlobe.olddoomcurse + " yesterday and is now " + (double)AetherGlobe.DoomCurse );		
				
			if (AetherGlobe.DoomCurse == 1) // curse was brought to 1, investments will do well
			{
				rateofreturn *= 1.25;
				multiplier += 1;
				if (AetherGlobe.olddoomcurse <= 1 || (AetherGlobe.olddoomcurse - AetherGlobe.rateofchange) <= 1) // two days in a row, jackpot!
					{
						rateofreturn *= 2;
						multiplier += 1;
					}
				
			}

				if (Utility.RandomBool())
				{				
					if (Utility.RandomDouble() > risklevel && rateofreturn > 0 && Utility.RandomBool() ) // investments going up, apply a multiplier based on the risk level and some change
					{
						rateofreturn *= 1 + ( 1 - (risklevel / 2));
						Console.WriteLine( "ROR Step2 " + rateofreturn );
					}
					else if (Utility.RandomDouble() > risklevel && rateofreturn < 0 && Utility.RandomBool() ) // returns are negative, random changes
					{
						rateofreturn /= 1 + (risklevel/2);
						Console.WriteLine( "ROR Step3 " + rateofreturn );
					}
					else if (rateofreturn > 0) // failed both checks, reduce investments or increase losses
						rateofreturn /= 1 + risklevel; 
					else if (rateofreturn < 0)
						rateofreturn *= 1 + risklevel;
				}

				if (risklevel >= 0.35) // high risk investing
				{
					if (Utility.RandomDouble() < risklevel)  // what were people thinking??? investing when risk level was high?!?!?!
					{
						if ( ( AetherGlobe.olddoomcurse - AetherGlobe.DoomCurse ) > 0 ) // check if curse went down
							rateofreturn *= 3; // risk was lowered, increased returns for everyone
						else 
							rateofreturn /= 3; // risk went up, everybody loses
				Console.WriteLine( "ROR Step4 " + rateofreturn );						
						if ( (AetherGlobe.olddoomcurse / 100000) > 0.50 && ( Utility.RandomDouble() < ( AetherGlobe.olddoomcurse / 100000 ) ) ) // was also risk yesterday! this person is putting his balls on the coals!
						{
							multiplier = 1; // resets multiplier
							rateofreturn = 0; // lose it all
						}
						else if ( (AetherGlobe.olddoomcurse / 100000) > 0.50)
							multiplier *= 5; // taking risks can have its rewards
				Console.WriteLine( "ROR Step5 " + rateofreturn );						
					}
					else// lucky person during high risk
					{
						multiplier *= 3; // was risky today, paid off
						if ( (AetherGlobe.olddoomcurse / 100000) > 0.50 && ( Utility.RandomDouble() < ( AetherGlobe.olddoomcurse / 100000 ) ) ) // second risk check for previous day
						{
							rateofreturn /= 3; // failed risk check for previous day, some loss
				Console.WriteLine( "ROR Step6 " + rateofreturn );
						}
						else if ((AetherGlobe.olddoomcurse / 100000) > 0.50) //this person was lucky during two periods of risk, jackpot payout
							multiplier *= 5; // taking risks can have its rewards
					}
				}
				else if (Utility.RandomDouble() < risklevel && Utility.RandomDouble() < (AetherGlobe.olddoomcurse / 100000))   //check to see if person is unlucky in low risk times (unlikely if risklevel is low)
				{	
					if ( rateofreturn > 0)
						rateofreturn = -(rateofreturn); // failed, flip return negative
					else if ( rateofreturn < 0)
						rateofreturn *= (1 + Math.Abs(rateofreturn)); // in case returns are negative, increase them

					if (Utility.RandomDouble() < 0.10) // if the above negative check occurs, there is a 10% chance of losing everything
						rateofreturn = 0;
				}					

					
				if (multiplier > 1 && rateofreturn == 0)
				{
					rateofreturn = 0.25; // final check: If players passed a luck check, return should never be 0
					Console.WriteLine( "ROR Step7 " + rateofreturn );
				}
				
				rateofreturn *= multiplier; // add multiplier
				Console.WriteLine( "ROR Step8 " + rateofreturn );
				
				if (rateofreturn > 10)
					rateofreturn = 10;
				if (rateofreturn < -1)
					rateofreturn = 0;
				Console.WriteLine( "ROR Step9 " + rateofreturn );
				
				// Aetherglobe.rateofreturn is calculated, now to see if it is positive or negative				
				//if ( ( ( ( (double)AetherGlobe.olddoomcurse  - (double)AetherGlobe.DoomCurse ) >= 0 ) && (Utility.RandomDouble() > (risklevel*1.5)) ) || ( (( (double)AetherGlobe.olddoomcurse  - (double)AetherGlobe.DoomCurse ) < 0) && (Utility.RandomDouble() > (risklevel*1.5)) && ( multiplier > 1 || Utility.RandomDouble() < ((double)MyServerSettings.curseincrease() / 100000) ) ) )

				//if ( ( (double)AetherGlobe.olddoomcurse  - (double)AetherGlobe.DoomCurse ) > 0 && Utility.RandomDouble() < (risklevel*2) && Utility.RandomBool() && rateofreturn > 0) // curse went down, rateofreturn should be positive, with chance of negative.
				//	rateofreturn = -(rateofreturn); // unlucky, negative return.
				//else if ( ( (double)AetherGlobe.olddoomcurse  - (double)AetherGlobe.DoomCurse ) < 0 && Utility.RandomDouble() > (risklevel*2) && Utility.RandomBool() && rateofreturn < 0) // curse went up, rateofreturn should be negative, chance of positive.
				//	rateofreturn = Math.Abs(rateofreturn); // positive returns
					
				Console.WriteLine( "ROR Step10 " + rateofreturn );

		}	
	
		
		
		
    }
}
