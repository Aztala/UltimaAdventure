using System;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
//using Server.Items.MusicBox;
using Server.Custom;

namespace Server.Mobiles
{
	public class SBSpammerRares : SBInfo
	{
		private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSpammerRares()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<GenericBuyInfo>
		{
			public InternalBuyInfo()
			{  
				//Add (new GenericBuyInfo( typeof( DawnsMusicBox ), 150000, 20, 0x2AF9, 0 ) );
				Add (new GenericBuyInfo( typeof( RandomTranscendence ), 10000, 20, 0x14F0, 0 ) );
				Add (new GenericBuyInfo( typeof( RandomAlacrity ), 50000, 20, 0x14F0, 0 ) );

				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealLlama ), 2400000, 1, 0x20F6, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( EtherealHorse ), 2500000, 1, 0x20DD, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealOstard ), 3500000, 1, 0x2135, 2858 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( EtherealRidgeback ), 3600000, 1, 0x2615, 2858 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( ClothingBlessDeed ), 750000, 1, 0x14F0, 0 ) );}
				if ( MyServerSettings.SellVeryRareChance() ){Add( new GenericBuyInfo( typeof( ClothingBlessDeed ), 1250000, 1, 0x14F0, 0 ) );}
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectMiningHarvester ), 750000, Utility.Random( 1,2 ), 0x5484, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectLumberHarvester ), 550000, Utility.Random( 1,2 ), 0x5486, 0 ) ); }
				if ( MyServerSettings.SellRareChance() ){Add( new GenericBuyInfo( typeof( PerfectHideHarvester ), 650000, Utility.Random( 1,2 ), 0x5487, 0 ) ); }
				//Add( new GenericBuyInfo( typeof(  ), 10000, 10, 0x14F0, 0 ) );
				//Add( new GenericBuyInfo( typeof( ScrollofAlacrity.CreateRandom() ), 50000, 10, 0x14F0, 0 ) );
/*20, 0x14F0, 0 )
				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( AssassinSpikeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( AxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BardicheOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BattleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BlackStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BladedStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BokutoOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BoneHarvesterOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( BroadswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ButcherKnifeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CleaverOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ClubOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CompositeBowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CrescentBladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( CutlassOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DaggerOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DaishoOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DiamondMaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DoubleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( DoubleBladedStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenCompositeLongbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenMacheteOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ElvenSpellbladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ExecutionersAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( GlacialStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( GnarledStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HalberdOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HammerPickOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HatchetOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( HeavyCrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( JukaBowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KamaOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KatanaOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( KryssOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LajatangOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LanceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LargeBattleAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LeafbladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( LongswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MagicalShortbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( MaulOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( NoDachiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( NunchakuOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( OrnateAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( PikeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( PitchforkOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( QuarterStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RadiantScimitarOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RepeatingCrossbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( RuneBladeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SaiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScepterOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScimitarOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ScytheOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShepherdsCrookOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShortbowOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ShortSpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SkinningKnifeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( SpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TekagiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TessenOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TetsuboOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( ThinLongswordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TribalSpearOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( TwoHandedAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( VikingSwordOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WakizashiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarAxeOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarCleaverOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarForkOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarHammerOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WarMaceOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( WildStaffOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}

				if ( Utility.RandomDouble() < 0.02 )
				{
				Add (new GenericBuyInfo( typeof( YumiOfEvolution ), 450000, 1, 0x13B9, 0 ) );
				}
				*/

			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( MonsterStatuette ), 20000 ); 
				Add( typeof( CrystallineBlackrock ), 1500 ); 
				Add( typeof( CandelabraOfSouls ), 5000 ); 
				Add( typeof( GoldBricks ), 5000 ); 
				Add( typeof( PhillipsWoodenSteed ), 5000 ); 
				Add( typeof( ArcticDeathDealer ), 5000 ); 
				Add( typeof( BlazeOfDeath ), 5000 ); 
				Add( typeof( BurglarsBandana ), 5000 ); 
				Add( typeof( CavortingClub ), 5000 ); 
				Add( typeof( DreadPirateHat ), 5000 ); 
				Add( typeof( EnchantedTitanLegBone ), 5000 ); 
				Add( typeof( GwennosHarp ), 5000 ); 
				Add( typeof( IolosLute ), 5000 ); 
				Add( typeof( LunaLance ), 5000 ); 
				Add( typeof( NightsKiss ), 5000 ); 
				Add( typeof( NoxRangersHeavyCrossbow ), 5000 ); 
				Add( typeof( PolarBearMask ), 5000 ); 
				Add( typeof( VioletCourage ), 5000 ); 
				Add( typeof( HeartOfTheLion ), 5000 ); 
				Add( typeof( ColdBlood ), 5000 ); 
				Add( typeof( AlchemistsBauble ), 5000 ); 
				Add( typeof( TheDragonSlayer ), 15000 ); 
				Add( typeof( ArmorOfFortune ), 15000 ); 
				Add( typeof( GauntletsOfNobility ), 15000 ); 
				Add( typeof( HelmOfInsight ), 15000 ); 
				Add( typeof( HolyKnightsBreastplate ), 15000 ); 
				Add( typeof( JackalsCollar ), 15000 ); 
				Add( typeof( LeggingsOfBane ), 15000 ); 
				Add( typeof( MidnightBracers ), 15000 ); 
				Add( typeof( OrnateCrownOfTheHarrower ), 15000 ); 
				Add( typeof( ShadowDancerLeggings ), 10000 ); 
				Add( typeof( TunicOfFire ), 15000 ); 
				Add( typeof( VoiceOfTheFallenKing ), 15000 ); 
				Add( typeof( BraceletOfHealth ), 15000 ); 
				Add( typeof( OrnamentOfTheMagician ), 15000 ); 
				Add( typeof( RingOfTheElements ), 15000 ); 
				Add( typeof( RingOfTheVile ), 15000 ); 
				Add( typeof( Aegis ), 15000 ); 
				Add( typeof( ArcaneShield ), 15000 ); 
				Add( typeof( AxeOfTheHeavens ), 15000 ); 
				Add( typeof( BladeOfInsanity ), 15000 ); 
				Add( typeof( BoneCrusher ), 15000 );
				Add( typeof( BreathOfTheDead ), 15000 );
				Add( typeof( Frostbringer ), 15000 );
				Add( typeof( SerpentsFang ), 15000 );
				Add( typeof( StaffOfTheMagi ), 10000 );
				Add( typeof( TheBeserkersMaul ), 10000 );
				Add( typeof( TheDryadBow ), 10000 );
				Add( typeof( DivineCountenance ), 15000 );
				Add( typeof( HatOfTheMagi ), 15000 );
				Add( typeof( HuntersHeaddress ), 15000 );
				Add( typeof( Antiquity ), 15000 );
				Add( typeof( BloodTrail ), 15000 );
				Add( typeof( BowOfHarps ), 15000 );
				Add( typeof( ChildOfDeath ), 15000 );
				Add( typeof( Erotica ), 15000 );
				Add( typeof( FeverFall ), 15000 );
				Add( typeof( GlovesOfTheHardWorker ), 15000 );
				Add( typeof( HandsofTabulature ), 15000 );
				Add( typeof( Kamadon ), 25000 );
				Add( typeof( LaFemme ), 15000 );
				Add( typeof( PurposeOfPain ), 20000 );
				Add( typeof( Revenge ), 15000 );
				Add( typeof( SatanicHelm ), 15000 );
				Add( typeof( StandStill ), 15000 );
				Add( typeof( TacticalMask ), 15000 );
				Add( typeof( ThickNeck ), 15000 );
				Add( typeof( ValasCompromise ), 15000 );
				Add( typeof( Valicious ), 15000 );
				Add( typeof( WizardsStrongArm ), 15000 );
				Add( typeof( SpiritOfTheTotem ), 15000 );
				Add( typeof( SpinedSewingKit ), 1000 ); 
				Add( typeof( HornedSewingKit ), 10000 ); 
				Add( typeof( BarbedSewingKit ), 100000 ); 
				

			}
		}
	}
}
