
using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Gumps
{
	public class MonsterContractGump : Gump
	{
		private MonsterContract MCparent;
		
		public MonsterContractGump( Mobile from, MonsterContract parentMC ) : base( 0, 0 )
		{
			if(from != null)from.CloseGump( typeof( MonsterContractGump ) );
			
			if(parentMC != null)
			{
				
				MCparent = parentMC;
			
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				this.AddPage(0);
				this.AddBackground(0, 0, 300, 170, 5170);
				this.AddLabel(40, 40, 0, @"Contract For: " + parentMC.AmountToTame + " " + MonsterContractType.Get[parentMC.Monster].Name);
				this.AddLabel(40, 60, 0, @"Quantity Tamed: " + parentMC.AmountTamed);
				this.AddLabel(40, 80, 0, @"Reward: " + parentMC.Reward);
				if ( parentMC.AmountTamed != parentMC.AmountToTame )
				{
					this.AddButton(90, 110, 2061, 2062, 1, GumpButtonType.Reply, 0);
					this.AddLabel(104, 108, 0, @"Add Pet");
				}
				else
				{
					this.AddButton(90, 110, 2061, 2062, 2, GumpButtonType.Reply, 0);
					this.AddLabel(104, 108, 0, @"Reward");
				}
			}
		}
		
		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile m_from = state.Mobile;
			
			if(m_from != null && MCparent != null)
			{
				if ( info.ButtonID == 1 )
				{
					m_from.SendMessage("Choose the tamed animal to add.");
					m_from.Target = new MonsterCorpseTarget( MCparent );
				}
				if ( info.ButtonID == 2 )
				{
					m_from.SendMessage("Your reward was placed in your bag.");

					
									Item shelf = null;
									int reward = 0;
									
									if (MCparent.Reward <6000)
										reward = Utility.RandomMinMax(0, 250); // 23% easy
									else if (MCparent.Reward <10000)
										reward = Utility.RandomMinMax(100, 350); // 13.8% easy // 6.4% medium
									else if (MCparent.Reward <15000)
										reward = Utility.RandomMinMax(200, 400); // 5.7% easy // 10% medium // 1% rare
									else if (MCparent.Reward <25000)
										reward = Utility.RandomMinMax(300, 500); // 6% medium // 3.5% rare // 0.6% impossible
									else if (MCparent.Reward < 40000)
										reward = Utility.RandomMinMax(400, 500); // 5.2% rare // 1.2% impossible
									else if (MCparent.Reward >= 40000)
										reward = Utility.RandomMinMax(450, 500); // 3% rare // 2.4% impossible
									
									if (reward <= 250)// easy finds
									{
										switch ( Utility.Random( 30 ) ) // 6/25 or 24% chance
										{
												case 0: shelf = new BluePetDye(); break;
												case 1: shelf = new GreenPetDye(); break;
												case 2: shelf = new OrangePetDye(); break;
												case 3: shelf = new PurplePetDye(); break;
												case 4: shelf = new RedPetDye(); break;
												case 5: shelf = new YellowPetDye(); break;
												case 6: shelf = new PetTrainer(); break;
										} 											
									}

									else if (reward <=375) // medium finds
									{
											switch ( Utility.Random( 50 ) ) // 16%
											{
												case 0: shelf = new BlackPetDye(); break;
												case 1: shelf = new WhitePetDye(); break;
												case 2: shelf = new BloodPetDye(); break;	
												case 3: shelf = new GoldPetDye(); break;	
												case 4: shelf = new MossGreenPetDye(); break;
												case 5: shelf = new PinkPetDye(); break;
												case 6: shelf = new PetBondDeed(); break;
												case 7: shelf = new PetControlDeed(); break;
	
											}
									}

									else if (reward <= 470) // rare finds
									{
											switch ( Utility.Random( 80 ) ) // 7.5%
											{
												case 0: shelf = new FrostBluePetDye(); break;		
												case 1: shelf = new BlazePetDye(); break;
												case 2: shelf = new IceWhitePetDye(); break;
												case 3: shelf = new IceBluePetDye(); break;
												case 4: shelf = new IceGreenPetDye(); break;
												case 5: shelf = new PetEasingDeed(); break;												
											}
									}
									

									else if (reward <= 500)  // impossible finds
									{
											switch ( Utility.Random( 50 ) ) // 4%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetSlotDeed(); break;
											}
									}
									Container backpack = m_from.Backpack;
									if (shelf != null)
									{
										backpack.DropItem( shelf );	
										m_from.SendMessage("You got a special drop!");		
									}										
									backpack.DropItem( new BankCheck( MCparent.Reward ) );									
					
					MCparent.Delete();
				}
			}
		}
	}
	
	public class MonsterCorpseTarget : Target
	{
		private MonsterContract MCparent;
		
		public MonsterCorpseTarget( MonsterContract parentMC ) : base( -1, true, TargetFlags.None )
		{
			MCparent = parentMC;
		}
		
		protected override void OnTarget( Mobile from, object o )
		{
            if ( MCparent == null || from == null || o == null || MCparent.Monster == null)
            {
                Console.WriteLine( "MonsterContract: Sa bug !! Mais o�, on sait pas :p" );
                return;
            }
			
			
			if ( o is BaseCreature )
			{
				BaseCreature pet = (BaseCreature)o;

				if ( !pet.Controlled || pet.ControlMaster != from ) 
					from.SendLocalizedMessage( 1042562 ); // You do not own that pet! 
				else if ( pet.IsDeadPet ) 
					from.SendLocalizedMessage( 1049668 ); // Living pets only, please. 
				else if ( pet.Summoned ) 
					from.SendMessage( "This creature was summoned." ); // I can not PetSale summoned creatures. 
				else if ( pet.Body.IsHuman ) 
					from.SendMessage( "This won't work on humans." ); // HA HA HA! Sorry, I am not an inn. 
				else if ( (pet is PackLlama || pet is PackHorse || pet is Beetle) && (pet.Backpack != null && pet.Backpack.Items.Count > 0) ) 
					from.SendLocalizedMessage( 1042563 ); // You need to unload your pet. 
				else if ( pet.Combatant != null && pet.InRange( pet.Combatant, 12 ) && pet.Map == pet.Combatant.Map ) 
					from.SendLocalizedMessage( 1042564 ); // I'm sorry.  Your pet seems to be busy. 
				else if ( pet.GetType() == MonsterContractType.Get[MCparent.Monster].Type )
					{
						MCparent.AmountTamed += 1;
						pet.ControlTarget = null; 
						pet.ControlOrder = OrderType.None; 
						pet.Internalize(); 
						pet.SetControlMaster( null ); 
						pet.SummonMaster = null;
						pet.Delete();	
					}
				else
					from.SendMessage("This pet won't work.");
			}
			else
				from.SendMessage("This is not a tamable pet.");
		}

	}
}
