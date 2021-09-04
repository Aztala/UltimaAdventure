using System;
using System.IO;
using System.Reflection;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;

namespace Server
{
	public class Loot
	{
		#region List definitions
		
		private static Type[] m_LibraryBookTypes = new Type[]
			{
				typeof( GrammarOfOrcish ),		typeof( CallToAnarchy ),				typeof( ArmsAndWeaponsPrimer ),
				typeof( SongOfSamlethe ),		typeof( TaleOfThreeTribes ),			typeof( GuideToGuilds ),
				typeof( BirdsOfBritannia ),		typeof( BritannianFlora ),				typeof( ChildrenTalesVol2 ),
				typeof( TalesOfVesperVol1 ),	typeof( DeceitDungeonOfHorror ),		typeof( DimensionalTravel ),
				typeof( EthicalHedonism ),		typeof( MyStory ),						typeof( DiversityOfOurLand ),
				typeof( QuestOfVirtues ),		typeof( RegardingLlamas ),				typeof( TalkingToWisps ),
				typeof( TamingDragons ),		typeof( BoldStranger ),					typeof( BurningOfTrinsic ),
				typeof( TheFight ),				typeof( LifeOfATravellingMinstrel ),	typeof( MajorTradeAssociation ),
				typeof( RankingsOfTrades ),		typeof( WildGirlOfTheForest ),			typeof( TreatiseOnAlchemy ),
				typeof( VirtueBook )
			};

		public static Type[] LibraryBookTypes{ get{ return m_LibraryBookTypes; } }

		private static Type[] m_SEWeaponTypes = new Type[]
			{
				typeof( Bokuto ),				typeof( Daisho ),				typeof( Kama ),
				typeof( Lajatang ),				typeof( NoDachi ),				typeof( Nunchaku ),
				typeof( Sai ),					typeof( Tekagi ),				typeof( Tessen ),
				typeof( Tetsubo ),				typeof( Wakizashi ), 			typeof( PugilistGloves ),
				typeof( RepeatingCrossbow ),	typeof( Katana ),
				typeof( QuarterStaff ),			typeof( Pike ),					typeof( BladedStaff ),
				typeof( Spear ),				typeof( Axe ),					typeof( ElvenMachete ),
				typeof( Scimitar ),				typeof( Leafblade ),			typeof( Longsword ),
				typeof( Dagger ),				typeof( WarMace )
			};

		public static Type[] SEWeaponTypes{ get{ return m_SEWeaponTypes; } }

		private static Type[] m_AosWeaponTypes = new Type[]
			{
				typeof( Scythe ),				typeof( BoneHarvester ),		typeof( Scepter ),
				typeof( BladedStaff ),			typeof( Pike ),					typeof( DoubleBladedStaff ),
				typeof( Lance ),				typeof( CrescentBlade )
			};

		public static Type[] AosWeaponTypes{ get{ return m_AosWeaponTypes; } }

		private static Type[] m_WeaponTypes = new Type[]
			{
				typeof( Axe ),				typeof( BattleAxe ),		typeof( DoubleAxe ),
				typeof( ExecutionersAxe ),	typeof( Hatchet ),			typeof( LargeBattleAxe ),
				typeof( TwoHandedAxe ),		typeof( WarAxe ),			typeof( Club ),
				typeof( Mace ),				typeof( Maul ),				typeof( WarHammer ),
				typeof( WarMace ),			typeof( Bardiche ),			typeof( Halberd ),
				typeof( Spear ),			typeof( ShortSpear ),		typeof( Pitchfork ),
				typeof( WarFork ),			typeof( BlackStaff ),		typeof( GnarledStaff ),
				typeof( QuarterStaff ),		typeof( Broadsword ),		typeof( Cutlass ),
				typeof( Katana ),			typeof( Kryss ),			typeof( Longsword ),
				typeof( Scimitar ),			typeof( VikingSword ),		typeof( Pickaxe ),
				typeof( HammerPick ),		typeof( ButcherKnife ),		typeof( Cleaver ),
				typeof( Dagger ),			typeof( SkinningKnife ),	typeof( ShepherdsCrook ),
				typeof( Axe ),				typeof( BattleAxe ),		typeof( DoubleAxe ),
				typeof( ExecutionersAxe ),	typeof( Hatchet ),			typeof( LargeBattleAxe ),
				typeof( TwoHandedAxe ),		typeof( WarAxe ),			typeof( Club ),
				typeof( Mace ),				typeof( Maul ),				typeof( WarHammer ),
				typeof( WarMace ),			typeof( Bardiche ),			typeof( Halberd ),
				typeof( Spear ),			typeof( ShortSpear ),		typeof( Pitchfork ),
				typeof( WarFork ),			typeof( BlackStaff ),		typeof( GnarledStaff ),
				typeof( QuarterStaff ),		typeof( Broadsword ),		typeof( Cutlass ),
				typeof( Katana ),			typeof( Kryss ),			typeof( Longsword ),
				typeof( Scimitar ),			typeof( VikingSword ),		typeof( Pickaxe ),
				typeof( HammerPick ),		typeof( ButcherKnife ),		typeof( Cleaver ),
				typeof( Dagger ),			typeof( SkinningKnife ),	typeof( ShepherdsCrook ),
				typeof( PugilistGlove ),	typeof( PugilistGloves ),	typeof( AssassinSpike ),
				typeof( ElvenMachete ),		typeof( DiamondMace ),		typeof( Leafblade ),
				typeof( OrnateAxe ),		typeof( ElvenSpellblade ),	typeof( WildStaff ),
				typeof( RuneBlade ),		typeof( RadiantScimitar ),	typeof( WarCleaver ),
				typeof( RoyalSword ),		typeof( WizardWand ),		typeof( WizardStaff ),
				typeof( WizardStick ),		typeof( Harpoon )
			};

		public static Type[] WeaponTypes{ get{ return m_WeaponTypes; } }

		private static Type[] m_SERangedWeaponTypes = new Type[]
			{
				typeof( Yumi ),					typeof( Yumi ),					typeof( Yumi ),
				typeof( Crossbow ),				typeof( ElvenCompositeLongbow ),
				typeof( ThrowingGloves )
			};

		public static Type[] SERangedWeaponTypes{ get{ return m_SERangedWeaponTypes; } }

		private static Type[] m_AosRangedWeaponTypes = new Type[]
			{
				typeof( CompositeBow ),				typeof( RepeatingCrossbow ),
				typeof( ElvenCompositeLongbow ),	typeof( MagicalShortbow )	
			};

		public static Type[] AosRangedWeaponTypes{ get{ return m_AosRangedWeaponTypes; } }

		private static Type[] m_RangedWeaponTypes = new Type[]
			{
				typeof( Bow ),					typeof( Crossbow ),				typeof( HeavyCrossbow ),
				typeof( ThrowingGloves )
			};

		public static Type[] RangedWeaponTypes{ get{ return m_RangedWeaponTypes; } }

		private static Type[] m_SEArmorTypes = new Type[]
			{
				typeof( ChainHatsuburi ),		typeof( LeatherDo ),				typeof( LeatherHaidate ),
				typeof( LeatherHiroSode ),		typeof( LeatherJingasa ),			typeof( LeatherMempo ),
				typeof( LeatherNinjaHood ),		typeof( LeatherNinjaJacket ),		typeof( LeatherNinjaMitts ),
				typeof( LeatherNinjaPants ),	typeof( LeatherSuneate ),			typeof( DecorativePlateKabuto ),
				typeof( HeavyPlateJingasa ),	typeof( LightPlateJingasa ),		typeof( PlateBattleKabuto ),
				typeof( PlateDo ),				typeof( PlateHaidate ),				typeof( PlateHatsuburi ),
				typeof( PlateHiroSode ),		typeof( PlateMempo ),				typeof( PlateSuneate ),
				typeof( SmallPlateJingasa ),	typeof( StandardPlateKabuto ),		typeof( StuddedDo ),
				typeof( StuddedHaidate ),		typeof( StuddedHiroSode ),			typeof( StuddedMempo ),
				typeof( StuddedSuneate ),		typeof( BoneSkirt ),				typeof( HideChest ), 
				typeof( SavageArms ), 			typeof( SavageChest ), 				typeof( SavageGloves ),
				typeof( SavageHelm ), 			typeof( SavageLegs ),				typeof( StuddedHideChest ), 
				typeof( Bascinet ),				typeof( LeatherSandals ),			typeof( RingmailArms ),			
				typeof( BoneArms ),				typeof( LeatherShoes ),				typeof( RingmailChest ),			
				typeof( BoneChest ),			typeof( LeatherShorts ),			typeof( RingmailGloves ),			
				typeof( BoneGloves ),			typeof( LeatherSkirt ),				typeof( RingmailLegs ),			
				typeof( BoneHelm ),				typeof( LeatherSoftBoots ),			typeof( RoyalArms ),			
				typeof( BoneLegs ),				typeof( LeatherThighBoots ),		typeof( RoyalBoots ),			
				typeof( ChainChest ),			typeof( MagicBoneArms ),			typeof( RoyalChest ),			
				typeof( ChainCoif ),			typeof( MagicBoneChest ),			typeof( DreadHelm ),
				typeof( ChainLegs ),			typeof( MagicBoneGloves ),			typeof( RoyalGloves ),			
				typeof( CloseHelm ),			typeof( MagicBoneHelm ),			typeof( RoyalGorget ),			
				typeof( FemaleLeatherChest ),	typeof( MagicBoneLegs ),			typeof( RoyalHelm ),			
				typeof( FemalePlateChest ),		typeof( RoyalsLegs ),				typeof( PlateSkirt ),
				typeof( FemaleStuddedChest ),	typeof( StuddedArms ),				typeof( ChainSkirt ),
				typeof( Helmet ),				typeof( StuddedBustierArms ),		typeof( RingmailSkirt ),
				typeof( MagicFurArms ),			typeof( MagicFurChest ),			typeof( MagicFurLegs ),
				typeof( LeatherArms ),			typeof( StuddedChest ),				typeof( StuddedSkirt ),
				typeof( LeatherBoots ),			typeof( StuddedGloves ),			typeof( MagicBoneSkirt ),
				typeof( LeatherBustierArms ),	typeof( NorseHelm ),				typeof( StuddedGorget ),			
				typeof( LeatherCap ),			typeof( OrcHelm ),					typeof( StuddedLegs ),			
				typeof( LeatherChest ),			typeof( PlateArms ),				typeof( WoodenPlateArms ),			
				typeof( LeatherCloak ),			typeof( PlateChest ),				typeof( WoodenPlateChest ),			
				typeof( LeatherGloves ),		typeof( PlateGloves ),				typeof( WoodenPlateGloves ),			
				typeof( LeatherGorget ),		typeof( PlateGorget ),				typeof( WoodenPlateGorget ),			
				typeof( LeatherLegs ),			typeof( PlateHelm ),				typeof( WoodenPlateHelm ),			
				typeof( LeatherRobe ),			typeof( PlateLegs ),				typeof( WoodenPlateLegs ),
				typeof( WhiteFurLegs ),			typeof( WhiteFurTunic ),			typeof( WhiteFurArms ),
				typeof( FurLegs ),				typeof( FurTunic ),					typeof( FurArms )
			};

		public static Type[] SEArmorTypes{ get{ return m_SEArmorTypes; } }

		private static Type[] m_ArmorTypes = new Type[]
			{
				typeof( Bascinet ),				typeof( LeatherSandals ),			typeof( RingmailArms ),			
				typeof( BoneArms ),				typeof( LeatherShoes ),				typeof( RingmailChest ),			
				typeof( BoneChest ),			typeof( LeatherShorts ),			typeof( RingmailGloves ),			
				typeof( BoneGloves ),			typeof( LeatherSkirt ),				typeof( RingmailLegs ),			
				typeof( BoneHelm ),				typeof( LeatherSoftBoots ),			typeof( RoyalArms ),			
				typeof( BoneLegs ),				typeof( LeatherThighBoots ),		typeof( RoyalBoots ),			
				typeof( ChainChest ),			typeof( MagicBoneArms ),			typeof( RoyalChest ),			
				typeof( ChainCoif ),			typeof( MagicBoneChest ),			typeof( DreadHelm ),
				typeof( ChainLegs ),			typeof( MagicBoneGloves ),			typeof( RoyalGloves ),			
				typeof( CloseHelm ),			typeof( MagicBoneHelm ),			typeof( RoyalGorget ),			
				typeof( FemaleLeatherChest ),	typeof( MagicBoneLegs ),			typeof( RoyalHelm ),			
				typeof( FemalePlateChest ),		typeof( RoyalsLegs ),				typeof( PlateSkirt ),
				typeof( FemaleStuddedChest ),	typeof( StuddedArms ),				typeof( ChainSkirt ),
				typeof( Helmet ),				typeof( StuddedBustierArms ),		typeof( RingmailSkirt ),
				typeof( MagicFurArms ),			typeof( MagicFurChest ),			typeof( MagicFurLegs ),
				typeof( LeatherArms ),			typeof( StuddedChest ),				typeof( StuddedSkirt ),
				typeof( LeatherBoots ),			typeof( StuddedGloves ),			typeof( MagicBoneSkirt ),
				typeof( LeatherBustierArms ),	typeof( NorseHelm ),				typeof( StuddedGorget ),			
				typeof( LeatherCap ),			typeof( OrcHelm ),					typeof( StuddedLegs ),			
				typeof( LeatherChest ),			typeof( PlateArms ),				typeof( WoodenPlateArms ),			
				typeof( LeatherCloak ),			typeof( PlateChest ),				typeof( WoodenPlateChest ),			
				typeof( LeatherGloves ),		typeof( PlateGloves ),				typeof( WoodenPlateGloves ),			
				typeof( LeatherGorget ),		typeof( PlateGorget ),				typeof( WoodenPlateGorget ),			
				typeof( LeatherLegs ),			typeof( PlateHelm ),				typeof( WoodenPlateHelm ),			
				typeof( LeatherRobe ),			typeof( PlateLegs ),				typeof( WoodenPlateLegs ),
				typeof( BoneSkirt ),			typeof( HideChest ), 
				typeof( SavageArms ), 			typeof( SavageChest ), 				typeof( SavageGloves ),
				typeof( SavageHelm ), 			typeof( SavageLegs ),				typeof( StuddedHideChest )
			};

		public static Type[] ArmorTypes{ get{ return m_ArmorTypes; } }

		private static Type[] m_DragonArmorTypes = new Type[]
			{
				typeof( MagicDragonArms ),		typeof( MagicDragonChest ),			typeof( MagicDragonGloves ),
				typeof( MagicDragonHelm ),		typeof( MagicDragonLegs )
			};

		public static Type[] DragonArmorTypes{ get{ return m_DragonArmorTypes; } }

		private static Type[] m_AosShieldTypes = new Type[]
			{
				typeof( ChaosShield ),			typeof( OrderShield ),			typeof( RoyalShield ),
				typeof( GuardsmanShield ),		typeof( ElvenShield ),			typeof( DarkShield ),
				typeof( CrestedShield ),		typeof( ChampionShield ),		typeof( JeweledShield )
			};

		public static Type[] AosShieldTypes{ get{ return m_AosShieldTypes; } }

		private static Type[] m_ShieldTypes = new Type[]
			{
				typeof( BronzeShield ),			typeof( Buckler ),				typeof( HeaterShield ),
				typeof( MetalShield ),			typeof( MetalKiteShield ),		typeof( WoodenKiteShield ),
				typeof( WoodenShield )
			};

		public static Type[] ShieldTypes{ get{ return m_ShieldTypes; } }

		private static Type[] m_GemTypes = new Type[]
			{
				typeof( Amber ),				typeof( Amethyst ),				typeof( Citrine ),
				typeof( Diamond ),				typeof( Emerald ),				typeof( Ruby ),
				typeof( Sapphire ),				typeof( StarSapphire ),			typeof( Tourmaline ),
				typeof( Amber ),				typeof( Amethyst ),				typeof( Citrine ),
				typeof( Diamond ),				typeof( Emerald ),				typeof( Ruby ),
				typeof( Sapphire ),				typeof( StarSapphire ),			typeof( Tourmaline ),
				typeof( Amber ),				typeof( Amethyst ),				typeof( Citrine ),
				typeof( Diamond ),				typeof( Emerald ),				typeof( Ruby ),
				typeof( Sapphire ),				typeof( StarSapphire ),			typeof( Tourmaline ),
				typeof( MagicJewelryRing ),		typeof( MagicJewelryNecklace ),	typeof( MagicJewelryEarrings ),
				typeof( MagicJewelryBracelet ),	typeof( MagicJewelryCirclet ), 	typeof( RandomLetter )
			};

		public static Type[] GemTypes{ get{ return m_GemTypes; } }

		private static Type[] m_JewelryTypes = new Type[]
			{
				typeof( MagicJewelryRing ),		typeof( MagicJewelryNecklace ),	typeof( MagicJewelryEarrings ),	
				typeof( MagicJewelryBracelet ),	typeof( MagicJewelryCirclet ),
				typeof( MagicCandle ),			typeof( MagicTorch ),			typeof( MagicLantern ),
				typeof( MagicSash ),			typeof( MagicCloak ),			typeof( MagicRobe ),			
				typeof( MagicRobe ), 			typeof( MagicBelt ), 			typeof( MagicHat ),
				typeof( MagicCloak ), 			typeof( MagicBoots ), 			typeof( MagicTalisman )
			};

		public static Type[] JewelryTypes{ get{ return m_JewelryTypes; } }

		private static Type[] m_SecretRegTypes = new Type[]
			{
				typeof( UnknownReagent )
			};

		public static Type[] SecretRegTypes { get{ return m_SecretRegTypes ; } }

		private static Type[] m_RegTypes = new Type[]
			{
				typeof( BlackPearl ),			typeof( Bloodmoss ),			typeof( Garlic ),
				typeof( Ginseng ),				typeof( MandrakeRoot ),			typeof( Nightshade ),
				typeof( SulfurousAsh ),			typeof( SpidersSilk )
			};

		public static Type[] RegTypes{ get{ return m_RegTypes; } }

		private static Type[] m_NecroRegTypes = new Type[]
			{
				typeof( BatWing ),				typeof( GraveDust ),			typeof( DaemonBlood ),
				typeof( NoxCrystal ),			typeof( PigIron )
			};

		public static Type[] NecroRegTypes{ get{ return m_NecroRegTypes; } }

		private static Type[] m_MixerRegTypes = new Type[]
			{
				typeof( EyeOfToad ),			typeof( FairyEgg ),				typeof( GargoyleEar ),
				typeof( BeetleShell ),			typeof( MoonCrystal ),			typeof( PixieSkull ),
				typeof( RedLotus ),				typeof( SeaSalt ),				typeof( SilverWidow ),
				typeof( SwampBerries ),			typeof( Brimstone ),			typeof( ButterflyWings )
			};

		public static Type[] MixerRegTypes{ get{ return m_MixerRegTypes; } }

		private static Type[] m_HerbRegTypes = new Type[]
			{
				typeof( PlantHerbalism_Leaf ),		typeof( PlantHerbalism_Flower ),		typeof( PlantHerbalism_Mushroom ),
				typeof( PlantHerbalism_Lilly ),		typeof( PlantHerbalism_Cactus ),		typeof( PlantHerbalism_Grass )
			};

		public static Type[] HerbRegTypes{ get{ return m_HerbRegTypes; } }

		private static Type[] m_PotionTypes = new Type[]
			{
				typeof( AgilityPotion ),				typeof( StrengthPotion ),				typeof( RefreshPotion ),
				typeof( LesserCurePotion ),				typeof( LesserHealPotion ),				typeof( LesserPoisonPotion ),
				typeof( ConflagrationPotion ),			typeof( ConfusionBlastPotion ),			typeof( CurePotion ),
				typeof( DeadlyPoisonPotion ),			typeof( ExplosionPotion ),				typeof( GreaterAgilityPotion ),
				typeof( GreaterConflagrationPotion ),	typeof( GreaterConfusionBlastPotion ),	typeof( GreaterCurePotion ),
				typeof( GreaterExplosionPotion ),		typeof( GreaterHealPotion ),			typeof( GreaterPoisonPotion ),
				typeof( GreaterStrengthPotion ),		typeof( HealPotion ),					typeof( LesserExplosionPotion ),
				typeof( NightSightPotion ),				typeof( PoisonPotion ),					typeof( TotalRefreshPotion ),
				typeof( LethalPoisonPotion ),			typeof( RejuvenatePotion ),				typeof( GreaterRejuvenatePotion ),
				typeof( InvisibilityPotion ),			typeof( GreaterInvisibilityPotion ),	typeof( LesserInvisibilityPotion ),
				typeof( LesserManaPotion ),				typeof( ManaPotion ),					typeof( PackGrenade ),
				typeof( GreaterManaPotion ),			typeof( LesserRejuvenatePotion ),
				typeof( FrostbitePotion ),				typeof( GreaterFrostbitePotion )
			};

		public static Type[] PotionTypes{ get{ return m_PotionTypes; } }

		private static Type[] m_UPotionTypes = new Type[]
			{
				typeof( UnknownLiquid )
			};

		public static Type[] UPotionTypes{ get{ return m_UPotionTypes; } }

		private static Type[] m_SEInstrumentTypes = new Type[]
			{
				typeof( Drums ),				typeof( LapHarp ),
				typeof( Lute ),					typeof( Tambourine ),
				typeof( BambooFlute )
			};

		public static Type[] SEInstrumentTypes{ get{ return m_SEInstrumentTypes; } }

		private static Type[] m_InstrumentTypes = new Type[]
			{
				typeof( Drums ),				typeof( LapHarp ),
				typeof( Lute ),					typeof( TambourineTassel ),
				typeof( BambooFlute )
			};

		public static Type[] InstrumentTypes{ get{ return m_InstrumentTypes; } }

		private static Type[] m_QuiverTypes = new Type[]
		{
			typeof( MagicQuiver ), typeof( MagicPoonBag )
		};

		public static Type[] QuiverTypes{ get{ return m_QuiverTypes; } }

		private static Type[] m_StatueTypes = new Type[]
		{
			typeof( StatueSouth ),			typeof( StatueSouth2 ),			typeof( StatueNorth ),
			typeof( StatueWest ),			typeof( StatueEast ),			typeof( StatueEast2 ),
			typeof( StatueSouthEast ),		typeof( BustSouth ),			typeof( BustEast ),
			typeof( RandomLetter )
		};

		public static Type[] StatueTypes{ get{ return m_StatueTypes; } }

		private static Type[] m_RegularScrollTypes = new Type[]
			{
				typeof( ReactiveArmorScroll ),	typeof( ClumsyScroll ),					typeof( CreateFoodScroll ),				typeof( FeeblemindScroll ),
				typeof( HealScroll ),			typeof( MagicArrowScroll ),				typeof( NightSightScroll ),				typeof( WeakenScroll ),
				typeof( AgilityScroll ),		typeof( CunningScroll ),				typeof( CureScroll ),					typeof( HarmScroll ),
				typeof( MagicTrapScroll ),		typeof( MagicUnTrapScroll ),			typeof( ProtectionScroll ),				typeof( StrengthScroll ),
				typeof( BlessScroll ),			typeof( FireballScroll ),				typeof( MagicLockScroll ),				typeof( PoisonScroll ),
				typeof( TelekinisisScroll ),	typeof( TeleportScroll ),				typeof( UnlockScroll ),					typeof( WallOfStoneScroll ),
				typeof( ArchCureScroll ),		typeof( ArchProtectionScroll ),			typeof( CurseScroll ),					typeof( FireFieldScroll ),
				typeof( GreaterHealScroll ),	typeof( LightningScroll ),				typeof( ManaDrainScroll ),				typeof( RecallScroll ),
				typeof( BladeSpiritsScroll ),	typeof( DispelFieldScroll ),			typeof( IncognitoScroll ),				typeof( MagicReflectScroll ),
				typeof( MindBlastScroll ),		typeof( ParalyzeScroll ),				typeof( PoisonFieldScroll ),			typeof( SummonCreatureScroll ),
				typeof( DispelScroll ),			typeof( EnergyBoltScroll ),				typeof( ExplosionScroll ),				typeof( InvisibilityScroll ),
				typeof( MarkScroll ),			typeof( MassCurseScroll ),				typeof( ParalyzeFieldScroll ),			typeof( RevealScroll ),
				typeof( ChainLightningScroll ), typeof( EnergyFieldScroll ),			typeof( FlamestrikeScroll ),			typeof( GateTravelScroll ),
				typeof( ManaVampireScroll ),	typeof( MassDispelScroll ),				typeof( MeteorSwarmScroll ),			typeof( PolymorphScroll ),
				typeof( EarthquakeScroll ),		typeof( EnergyVortexScroll ),			typeof( ResurrectionScroll ),			typeof( SummonAirElementalScroll ),
				typeof( SummonDaemonScroll ),	typeof( SummonEarthElementalScroll ),	typeof( SummonFireElementalScroll ),	typeof( SummonWaterElementalScroll )
			};

		private static Type[] m_NecromancyScrollTypes = new Type[]
			{
				typeof( AnimateDeadScroll ),		typeof( BloodOathScroll ),		typeof( CorpseSkinScroll ),	typeof( CurseWeaponScroll ),
				typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),	typeof( LichFormScroll ),	typeof( MindRotScroll ),
				typeof( PainSpikeScroll ),			typeof( PoisonStrikeScroll ),	typeof( StrangleScroll ),	typeof( SummonFamiliarScroll ),
				typeof( VampiricEmbraceScroll ),	typeof( VengefulSpiritScroll ),	typeof( WitherScroll ),		typeof( WraithFormScroll ),
				typeof( ExorcismScroll )
			};
			
		private static Type[] m_SENecromancyScrollTypes = new Type[]
			{
				typeof( AnimateDeadScroll ),		typeof( BloodOathScroll ),		typeof( CorpseSkinScroll ),	typeof( CurseWeaponScroll ),
				typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),	typeof( LichFormScroll ),	typeof( MindRotScroll ),
				typeof( PainSpikeScroll ),			typeof( PoisonStrikeScroll ),	typeof( StrangleScroll ),	typeof( SummonFamiliarScroll ),
				typeof( VampiricEmbraceScroll ),	typeof( VengefulSpiritScroll ),	typeof( WitherScroll ),		typeof( WraithFormScroll ),
				typeof( ExorcismScroll )
			};

		private static Type[] m_PaladinScrollTypes = new Type[0];

		private static Type[] m_ArcaneScrollTypes = new Type[]
			{
				typeof( ReactiveArmorScroll ),	typeof( ClumsyScroll ),					typeof( CreateFoodScroll ),				typeof( FeeblemindScroll ),
				typeof( HealScroll ),			typeof( MagicArrowScroll ),				typeof( NightSightScroll ),				typeof( WeakenScroll ),
				typeof( AgilityScroll ),		typeof( CunningScroll ),				typeof( CureScroll ),					typeof( HarmScroll ),
				typeof( MagicTrapScroll ),		typeof( MagicUnTrapScroll ),			typeof( ProtectionScroll ),				typeof( StrengthScroll ),
				typeof( BlessScroll ),			typeof( FireballScroll ),				typeof( MagicLockScroll ),				typeof( PoisonScroll ),
				typeof( TelekinisisScroll ),	typeof( TeleportScroll ),				typeof( UnlockScroll ),					typeof( WallOfStoneScroll ),
				typeof( ArchCureScroll ),		typeof( ArchProtectionScroll ),			typeof( CurseScroll ),					typeof( FireFieldScroll ),
				typeof( GreaterHealScroll ),	typeof( LightningScroll ),				typeof( ManaDrainScroll ),				typeof( RecallScroll ),
				typeof( BladeSpiritsScroll ),	typeof( DispelFieldScroll ),			typeof( IncognitoScroll ),				typeof( MagicReflectScroll ),
				typeof( MindBlastScroll ),		typeof( ParalyzeScroll ),				typeof( PoisonFieldScroll ),			typeof( SummonCreatureScroll ),
				typeof( DispelScroll ),			typeof( EnergyBoltScroll ),				typeof( ExplosionScroll ),				typeof( InvisibilityScroll ),
				typeof( MarkScroll ),			typeof( MassCurseScroll ),				typeof( ParalyzeFieldScroll ),			typeof( RevealScroll ),
				typeof( ChainLightningScroll ), typeof( EnergyFieldScroll ),			typeof( FlamestrikeScroll ),			typeof( GateTravelScroll ),
				typeof( ManaVampireScroll ),	typeof( MassDispelScroll ),				typeof( MeteorSwarmScroll ),			typeof( PolymorphScroll ),
				typeof( EarthquakeScroll ),		typeof( EnergyVortexScroll ),			typeof( ResurrectionScroll ),			typeof( SummonAirElementalScroll ),
				typeof( SummonDaemonScroll ),	typeof( SummonEarthElementalScroll ),	typeof( SummonFireElementalScroll ),	typeof( SummonWaterElementalScroll )
			};

		public static Type[] RegularScrollTypes{ get{ return m_RegularScrollTypes; } }
		public static Type[] NecromancyScrollTypes{ get{ return m_NecromancyScrollTypes; } }
		public static Type[] SENecromancyScrollTypes{ get{ return m_SENecromancyScrollTypes; } }
		public static Type[] PaladinScrollTypes{ get{ return m_PaladinScrollTypes; } }
		public static Type[] ArcaneScrollTypes{ get{ return m_ArcaneScrollTypes; } }

		private static Type[] m_WandTypes = new Type[]
			{

				typeof( AgilityMagicStaff ),		typeof( PoisonFieldMagicStaff ),	typeof( MagicArrowMagicStaff ),			typeof( EarthElementalMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicArrowMagicStaff ),			typeof( EarthquakeMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicArrowMagicStaff ),			typeof( EnergyBoltMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicLockMagicStaff ),			typeof( EnergyBoltMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicLockMagicStaff ),			typeof( EnergyBoltMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicLockMagicStaff ),			typeof( EnergyFieldMagicStaff ),
				typeof( AgilityMagicStaff ),		typeof( PoisonMagicStaff ),			typeof( MagicLockMagicStaff ),			typeof( EnergyFieldMagicStaff ),
				typeof( AirElementalMagicStaff ),	typeof( PolymorphMagicStaff ),		typeof( MagicLockMagicStaff ),			typeof( EnergyVortexMagicStaff ),
				typeof( ArchCureMagicStaff ),		typeof( PolymorphMagicStaff ),		typeof( MagicLockMagicStaff ),			typeof( ExplosionMagicStaff ),
				typeof( ArchCureMagicStaff ),		typeof( ProtectionMagicStaff ),		typeof( MagicReflectionMagicStaff ),	typeof( ExplosionMagicStaff ),
				typeof( ArchCureMagicStaff ),		typeof( ProtectionMagicStaff ),		typeof( MagicReflectionMagicStaff ),	typeof( ExplosionMagicStaff ),
				typeof( ArchCureMagicStaff ),		typeof( ProtectionMagicStaff ),		typeof( MagicReflectionMagicStaff ),	typeof( FeebleMagicStaff ),
				typeof( ArchCureMagicStaff ),		typeof( ProtectionMagicStaff ),		typeof( MagicReflectionMagicStaff ),	typeof( FeebleMagicStaff ),
				typeof( ArchProtectionMagicStaff ),	typeof( ProtectionMagicStaff ),		typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( ArchProtectionMagicStaff ),	typeof( ProtectionMagicStaff ),		typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( ArchProtectionMagicStaff ),	typeof( ProtectionMagicStaff ),		typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( ArchProtectionMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( ArchProtectionMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( BladeSpiritsMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicTrapMagicStaff ),			typeof( FeebleMagicStaff ),
				typeof( BladeSpiritsMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicTrapMagicStaff ),			typeof( FireballMagicStaff ),
				typeof( BladeSpiritsMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicUnlockMagicStaff ),		typeof( FireballMagicStaff ),
				typeof( BladeSpiritsMagicStaff ),	typeof( ReactiveArmorMagicStaff ),	typeof( MagicUnlockMagicStaff ),		typeof( FireballMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( ReactiveArmorMagicStaff ),	typeof( MagicUnlockMagicStaff ),		typeof( FireballMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( ReactiveArmorMagicStaff ),	typeof( MagicUnlockMagicStaff ),		typeof( FireballMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( RecallMagicStaff ),			typeof( MagicUnlockMagicStaff ),		typeof( FireballMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( RecallMagicStaff ),			typeof( MagicUnlockMagicStaff ),		typeof( FireElementalMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( RecallMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FireFieldMagicStaff ),
				typeof( BlessMagicStaff ),			typeof( RecallMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FireFieldMagicStaff ),
				typeof( ChainLightningMagicStaff ),	typeof( RecallMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FireFieldMagicStaff ),
				typeof( ChainLightningMagicStaff ),	typeof( ResurrectionMagicStaff ),	typeof( MagicUntrapMagicStaff ),		typeof( FireFieldMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( RevealMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FireFieldMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( RevealMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FlameStrikeMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( RevealMagicStaff ),			typeof( MagicUntrapMagicStaff ),		typeof( FlameStrikeMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( StrengthMagicStaff ),		typeof( ManaDrainMagicStaff ),			typeof( GateTravelMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( StrengthMagicStaff ),		typeof( ManaDrainMagicStaff ),			typeof( GateTravelMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( StrengthMagicStaff ),		typeof( ManaDrainMagicStaff ),			typeof( GreaterHealMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( StrengthMagicStaff ),		typeof( ManaDrainMagicStaff ),			typeof( GreaterHealMagicStaff ),
				typeof( ClumsyMagicStaff ),			typeof( StrengthMagicStaff ),		typeof( ManaDrainMagicStaff ),			typeof( GreaterHealMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( StrengthMagicStaff ),		typeof( ManaVampireMagicStaff ),		typeof( GreaterHealMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( StrengthMagicStaff ),		typeof( ManaVampireMagicStaff ),		typeof( GreaterHealMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( SummonCreatureMagicStaff ),	typeof( MarkMagicStaff ),				typeof( HarmMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( SummonCreatureMagicStaff ),	typeof( MarkMagicStaff ),				typeof( HarmMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( SummonCreatureMagicStaff ),	typeof( MarkMagicStaff ),				typeof( HarmMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( SummonCreatureMagicStaff ),	typeof( MassCurseMagicStaff ),			typeof( HarmMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( SummonDaemonMagicStaff ),	typeof( MassCurseMagicStaff ),			typeof( HarmMagicStaff ),
				typeof( CreateFoodMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MassCurseMagicStaff ),			typeof( HarmMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MassDispelMagicStaff ),			typeof( HarmMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MassDispelMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MeteorSwarmMagicStaff ),		typeof( HealMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MeteorSwarmMagicStaff ),		typeof( HealMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TelekinesisMagicStaff ),	typeof( MindBlastMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TeleportMagicStaff ),		typeof( MindBlastMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CunningMagicStaff ),		typeof( TeleportMagicStaff ),		typeof( MindBlastMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CureMagicStaff ),			typeof( TeleportMagicStaff ),		typeof( MindBlastMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CureMagicStaff ),			typeof( TeleportMagicStaff ),		typeof( NightSightMagicStaff ),			typeof( HealMagicStaff ),
				typeof( CureMagicStaff ),			typeof( TeleportMagicStaff ),		typeof( NightSightMagicStaff ),			typeof( IncognitoMagicStaff ),
				typeof( CureMagicStaff ),			typeof( TeleportMagicStaff ),		typeof( NightSightMagicStaff ),			typeof( IncognitoMagicStaff ),
				typeof( CureMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( NightSightMagicStaff ),			typeof( IncognitoMagicStaff ),
				typeof( CureMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( NightSightMagicStaff ),			typeof( IncognitoMagicStaff ),
				typeof( CureMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( NightSightMagicStaff ),			typeof( InvisibilityMagicStaff ),
				typeof( CurseMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( NightSightMagicStaff ),			typeof( InvisibilityMagicStaff ),
				typeof( CurseMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( NightSightMagicStaff ),			typeof( InvisibilityMagicStaff ),
				typeof( CurseMagicStaff ),			typeof( WallofStoneMagicStaff ),	typeof( ParalyzeFieldMagicStaff ),		typeof( LightningMagicStaff ),
				typeof( CurseMagicStaff ),			typeof( WaterElementalMagicStaff ),	typeof( ParalyzeFieldMagicStaff ),		typeof( LightningMagicStaff ),
				typeof( CurseMagicStaff ),			typeof( WeaknessMagicStaff ),		typeof( ParalyzeFieldMagicStaff ),		typeof( LightningMagicStaff ),
				typeof( DispelFieldMagicStaff ),	typeof( WeaknessMagicStaff ),		typeof( ParalyzeMagicStaff ),			typeof( LightningMagicStaff ),
				typeof( DispelFieldMagicStaff ),	typeof( WeaknessMagicStaff ),		typeof( ParalyzeMagicStaff ),			typeof( LightningMagicStaff ),
				typeof( DispelFieldMagicStaff ),	typeof( WeaknessMagicStaff ),		typeof( ParalyzeMagicStaff ),			typeof( MagicArrowMagicStaff ),
				typeof( DispelFieldMagicStaff ),	typeof( WeaknessMagicStaff ),		typeof( ParalyzeMagicStaff ),			typeof( MagicArrowMagicStaff ),
				typeof( DispelMagicStaff ),			typeof( WeaknessMagicStaff ),		typeof( PoisonFieldMagicStaff ),		typeof( MagicArrowMagicStaff ),
				typeof( DispelMagicStaff ),			typeof( WeaknessMagicStaff ),		typeof( PoisonFieldMagicStaff ),		typeof( MagicArrowMagicStaff ),
				typeof( DispelMagicStaff ),			typeof( WeaknessMagicStaff ),		typeof( PoisonFieldMagicStaff ),		typeof( MagicArrowMagicStaff )
			};
		public static Type[] WandTypes{ get{ return m_WandTypes; } }

		private static Type[] m_ArtyTypes = new Type[]
			{
				typeof( GlassSword ),				typeof( EarringsOfTheVile ),				typeof( QuiverOfInfinity ),
				typeof( JesterHatofChuckles ), 		typeof( ShaminoCrossbow ), 					typeof( EarringsOfProtection ),
				typeof( SamuraiHelm ), 				typeof( ArcticDeathDealer ),				typeof( LuckyNecklace ),
				typeof( BlazeOfDeath ), 			typeof( BowOfTheJukaKing ),					typeof( HolySword ),
				typeof( BurglarsBandana ), 			typeof( CavortingClub ),					typeof( DupresShield ),
				typeof( EnchantedTitanLegBone ), 	typeof( GwennosHarp ),						typeof( EarringBoxSet ),
				typeof( IolosLute ), 				typeof( LunaLance ),						typeof( SilvanisFeywoodBow ),
				typeof( NightsKiss ), 				typeof( NoxRangersHeavyCrossbow ),			typeof( TheNightReaper ),
				typeof( OrcishVisage ), 			typeof( PolarBearMask ),					typeof( BlightGrippedLongbow ),
				typeof( ShieldOfInvulnerability ), 	typeof( StaffOfPower ),						typeof( ColdForgedBlade ),
				typeof( VioletCourage ), 			typeof( HeartOfTheLion ), 					typeof( LuminousRuneBlade),
				typeof( WrathOfTheDryad ), 			typeof( PixieSwatter ), 					typeof( OverseerSunderedBlade),
				typeof( GlovesOfThePugilist ),		typeof( DreadPirateHat ),					typeof( PhantomStaff ),
				typeof( CandelabraOfSouls ),		typeof( ColdBlood ),						typeof( RuneCarvingKnife),
				typeof( ArcaneArms ),				typeof( GlovesOfAegis ),					typeof( JackalsTunic ), 
				typeof( ArcaneCap ),				typeof( GlovesOfFortune ),					typeof( LeggingsOfAegis ), 
				typeof( ArcaneGloves ),				typeof( GlovesOfInsight ),					typeof( LeggingsOfFire ), 
				typeof( ArcaneGorget ),				typeof( GlovesOfTheFallenKing ),			typeof( LegsOfFortune ), 
				typeof( ArcaneLeggings ),			typeof( GlovesOfTheHarrower ),				typeof( LegsOfInsight ), 
				typeof( ArcaneTunic ),				typeof( GorgetOfAegis ),					typeof( LegsOfNobility ), 
				typeof( ArmorOfInsight ),			typeof( GorgetOfFortune ),					typeof( LegsOfTheFallenKing ), 
				typeof( ArmorOfNobility ),			typeof( GorgetOfInsight ),					typeof( LegsOfTheHarrower ), 
				typeof( ArmsOfAegis ),				typeof( HelmOfAegis ),						typeof( MidnightGloves ), 
				typeof( ArmsOfFortune ),			typeof( HolyKnightsArmPlates ),				typeof( MidnightHelm ), 
				typeof( ArmsOfInsight ),			typeof( HolyKnightsGloves ),				typeof( MidnightLegs ), 
				typeof( ArmsOfNobility ),			typeof( HolyKnightsGorget ),				typeof( MidnightTunic ), 
				typeof( ArmsOfTheFallenKing ),		typeof( HolyKnightsLegging ),				typeof( RingOfHealth ), 
				typeof( ArmsOfTheHarrower ),		typeof( HolyKnightsPlateHelm ),				typeof( RingOfTheMagician ), 
				typeof( BraceletOfTheElements ),	typeof( HuntersArms ),						typeof( ShadowDancerArms ), 
				typeof( BraceletOfTheVile ),		typeof( HuntersGloves ),					typeof( ShadowDancerCap ), 
				typeof( CapOfFortune ),				typeof( HuntersGorget ),					typeof( ShadowDancerGloves ), 
				typeof( CapOfTheFallenKing ),		typeof( HuntersLeggings ),					typeof( ShadowDancerGorget ), 
				typeof( CoifOfBane ),				typeof( HuntersTunic ),						typeof( ShadowDancerTunic ), 
				typeof( CoifOfFire ),				typeof( InquisitorsArms ),					typeof( TotemArms ), 
				typeof( DivineArms ),				typeof( InquisitorsGorget ),				typeof( TotemGloves ), 
				typeof( DivineGloves ),				typeof( InquisitorsHelm ),					typeof( TotemGorget ), 
				typeof( DivineGorget ),				typeof( InquisitorsLeggings ),				typeof( TotemLeggings ), 
				typeof( DivineLeggings ),			typeof( InquisitorsTunic ),					typeof( TotemTunic ), 
				typeof( DivineTunic ),				typeof( JackalsArms ),						typeof( TunicOfAegis ), 
				typeof( EarringsOfHealth ),			typeof( JackalsGloves ),					typeof( TunicOfBane ), 
				typeof( EarringsOfTheElements ),	typeof( JackalsHelm ),						typeof( TunicOfTheFallenKing ), 
				typeof( EarringsOfTheMagician ),	typeof( JackalsLeggings ),					typeof( TunicOfTheHarrower ), 
				typeof( YashimotosHatsuburi ),		typeof( WizardsPants ),						typeof( Fortifiedarms ),	
				typeof( WarriorsClasp ),			typeof( MauloftheBeast ),					typeof( FesteringWound ),	
				typeof( WeaponRenamingTool ),		typeof( MarbleShield ),						typeof( FalseGodsScepter ),	
				typeof( MagiciansMempo ),			typeof( EvilMageGloves ),	
				typeof( VampiricDaisho ),			typeof( MagiciansIllusion ),				typeof( EternalFlame ),	
				typeof( TownGuardsHalberd ),		typeof( MagesBand ),						typeof( DupresCollar ),	
				typeof( SwiftStrike ),				typeof( MadmansHatchet ),					typeof( DeathsMask ),	
				typeof( SprintersSandals ),			typeof( LuckyEarrings ),					typeof( DarkNeck ),	
				typeof( ShimmeringTalisman ),		typeof( LongShot ),							typeof( DarkLordsPitchfork ),	
				typeof( ShadowBlade ),				typeof( LeggingsOfEnlightenment ),			typeof( CircletOfTheSorceress ),	
				typeof( RoyalGuardsGorget ),		typeof( LeggingsOfDeceit ),					typeof( DarkGuardiansChest ),	
				typeof( RoyalGuardsChestplate ),	typeof( KamiNarisIndestructableDoubleAxe ),	typeof( BookOfKnowledge ),	
				typeof( RoyalArchersBow ),			typeof( JinBaoriOfGoodFortune ),			typeof( BalancingDeed ),	
				typeof( RobeOfTreason ),			typeof( JadeScimitar ),						typeof( AuraOfShadows ),	
				typeof( Retort ),					typeof( Indecency ),						typeof( ArmsOfToxicity ),	
				typeof( RamusNecromanticScalpel ),	typeof( HellForgedArms ),					typeof( ArcticBeacon ),	
				typeof( PowerSurge ),				typeof( GlovesOfRegeneration ),				typeof( ArcanicRobe ),	
				typeof( Pestilence ),				typeof( GlovesOfCorruption ),				typeof( Annihilation ),	
				typeof( NoxNightlight ),			typeof( GeishasObi ),						typeof( AngeroftheGods ),	
				typeof( NoxBow ),					typeof( Fury ),								typeof( AngelicEmbrace ),	
				typeof( NordicVikingSword ),		typeof( FurCapeOfTheSorceress ),			typeof( AbysmalGloves ),	
				typeof( MinersPickaxe ),			typeof( FortunateBlades ),					typeof( RobeOfTheEquinox ),	
				typeof( VoiceOfTheFallenKing ),		typeof( Quell ),							typeof( ArcaneShield ),	
				typeof( TunicOfFire ),				typeof( OrcChieftainHelm ),					typeof( ZyronicClaw ),	
				typeof( ShadowDancerLeggings ),		typeof( GladiatorsCollar ),					typeof( TitansHammer ),	
				typeof( OrnateCrownOfTheHarrower ),	typeof( FangOfRactus ),						typeof( TheTaskmaster ),	
				typeof( MidnightBracers ),			typeof( CrownOfTalKeesh ),					typeof( TheDryadBow ),	
				typeof( LeggingsOfBane ),			typeof( Calm ),								typeof( TheDragonSlayer ),	
				typeof( JackalsCollar ),			typeof( AcidProofRobe ),					typeof( TheBeserkersMaul ),	
				typeof( InquisitorsResolution ),	typeof( SpiritOfTheTotem ),					typeof( StaffOfTheMagi ),	
				typeof( HolyKnightsBreastplate ),	typeof( HuntersHeaddress ),					typeof( SerpentsFang ),	
				typeof( HelmOfInsight ),			typeof( HatOfTheMagi ),						typeof( LegacyOfTheDreadLord ),	
				typeof( GauntletsOfNobility ),		typeof( DivineCountenance ),				typeof( Frostbringer ),	
				typeof( ArmorOfFortune ),			typeof( CrimsonCincture ),					typeof( BreathOfTheDead ),	
				typeof( TheRobeOfBritanniaAri ),	typeof( RingOfTheVile ),					typeof( QuiverOfElements ),	
				typeof( SamaritanRobe ),			typeof( RingOfTheElements ),				typeof( QuiverOfRage ),	
				typeof( RoyalGuardSurvivalKnife ),	typeof( OrnamentOfTheMagician ),			typeof( RighteousAnger ),	
				typeof( OblivionsNeedle ),			typeof( BraceletOfHealth ),					typeof( RobeOfTheEclipse ),	
				typeof( LieutenantOfTheBritannianRoyalGuard ),									typeof( QuiverOfLightning ),
				typeof( GuantletsOfAnger ),			typeof( QuiverOfIce ),						typeof( SoulSeeker ),	
				typeof( EmbroideredOakLeafCloak ),	typeof( QuiverOfFire ),						typeof( TalonBite ),	
				typeof( DjinnisRing ),				typeof( QuiverOfBlight ),					typeof( WildfireBow ),	
				typeof( Subdue ),					typeof( ElvenQuiver ),						typeof( TotemOfVoid ),	
				typeof( ShroudOfDeciet ),			typeof( Aegis ),							typeof( Windsong ), 
				typeof( EverlastingLoaf ),			typeof( EverlastingBottle ),				typeof( PolarBearCape ),
				typeof( PolarBearBoots ),			typeof( Excalibur ),						typeof( KodiakBearMask ),
				typeof( Stormbringer ), 			typeof( AchillesShield ),					typeof( AchillesSpear ),
				typeof( StaffofSnakes ),			typeof( AilricsLongbow ),					typeof( BeltofHercules ),
				typeof( RobinHoodsFeatheredHat ),	typeof( RobinHoodsBow ),					typeof( HammerofThor ),
				typeof( AxeoftheMinotaur ),			typeof( BowofthePhoenix ),					typeof( GandalfsRobe ),
				typeof( GandalfsHat ),				typeof( GandalfsStaff ), 					typeof( PandorasBox),
				typeof( ConansHelm ),				typeof( ConansSword ),						typeof( SinbadsSword ),
				typeof( SkullChest ),				typeof( ShardThrasher ),					typeof( BeggarsRobe ),
				typeof( VampiresRobe ),				typeof( ConansLoinCloth ),					typeof( PendantOfTheMagi ),
				typeof( EssenceOfBattle ),			typeof( ResilientBracer ),					typeof( GiantBlackjack ), 
				typeof( QuiverOfRage ),				typeof( MelisandesCorrodedHatchet ),		typeof( BloodwoodSpirit ), 
				typeof( GrayMouserCloak ),			typeof( BootsofHermes ),					typeof( FleshRipper ),
				typeof( CandleCold ),				typeof( CandleFire ),						typeof( CandlePoison ),
				typeof( CandleEnergy ),				typeof( CandleWizard ),						typeof( CandleNecromancer ),
				typeof( GrimReapersRobe ),			typeof( GrimReapersMask ),					typeof( GrimReapersLantern ),
				typeof( GrimReapersScythe ),		typeof( TorchOfTrapFinding ),				typeof( RodOfResurrection ),
				typeof( RobeOfTeleportation ),		typeof( MaulOfTheTitans ),					typeof( HelmOfBrilliance ),
				typeof( GlovesOfDexterity ),		typeof( GemOfSeeing ),						typeof( DaggerOfVenom ),
				typeof( ColoringBook ),				typeof( BagOfHolding ), 					typeof( Antiquity ),
				typeof( BloodTrail ),				typeof( BowOfHarps ),						typeof( ChildOfDeath ),
				typeof( Erotica ),					typeof( FeverFall ),						typeof( GlovesOfTheHardWorker ),
				typeof( HandsofTabulature ),		typeof( Kamadon ),							typeof( LaFemme ),
				typeof( PurposeOfPain ),			typeof( Revenge ),							typeof( SatanicHelm ),
				typeof( ShieldOfIce ),				typeof( StandStill ),
				typeof( SwordOfIce ),				typeof( TacticalMask ),						typeof( ThickNeck ),
				typeof( ValasCompromise ),			typeof( Valicious ),						typeof( WizardsStrongArm ),		
				typeof( MIBHunter ),				typeof( Venom ),							typeof( TokenOfHolyFavor ),
				typeof( StandardOfChaos ),			typeof( NightEyes ),						typeof( TorcOfTheGuardians ),				
				typeof( Tangle ),					typeof( ShroudOfTheCondemned ),				typeof( Mangler ),
				typeof( IronwoodCompositeBow ),		typeof( FallenMysticsSpellbook ),			typeof( EternalGuardianStaff ),			
				typeof( DragonJadeEarrings ),		typeof( DemonHuntersStandard ),				typeof( DemonBridleRing ),			
				typeof( DefenderOfTheMagus ),		typeof( CavalrysFolly ),					typeof( BurningAmber ),			
				typeof( AnimatedLegsoftheInsaneTinker ), 										typeof( ElvenPoonBag ), typeof( MagicPoonBag )
			};
		public static Type[] ArtyTypes{ get{ return m_ArtyTypes; } }

		private static Type[] m_RelicTypes = new Type[]
			{
				typeof( DDRelicCoins ),
				typeof( DDRelicClock1 ),		typeof( DDRelicClock2 ),				typeof( DDRelicClock3 ),
				typeof( DDRelicLight2 ), 		typeof( DDRelicLight1 ), 				typeof( DDRelicLight3 ),
				typeof( DDRelicVase ),			typeof( DDRelicPainting ),				typeof( DDRelicArts ),
				typeof( DDRelicStatue ), 		typeof( DDRelicRugAddonDeed ),			typeof( DDRelicWeapon ),
				typeof( DDRelicArmor ),			typeof( DDRelicJewels ),				typeof( DDRelicInstrument ),
				typeof( DDRelicScrolls ),		typeof( DDRelicCloth ),					typeof( DDRelicFur ),
				typeof( DDRelicDrink ),			typeof( DDRelicReagent ),				typeof( DDRelicOrbs ),
				typeof( DDRelicVase ),			typeof( DDRelicPainting ),				typeof( DDRelicArts ),
				typeof( DDRelicStatue ), 		typeof( DDRelicRugAddonDeed ),			typeof( DDRelicWeapon ),
				typeof( DDRelicArmor ),			typeof( DDRelicJewels ),				typeof( DDRelicInstrument ),
				typeof( DDRelicScrolls ),		typeof( DDRelicCloth ),					typeof( DDRelicFur ),
				typeof( DDRelicDrink ),			typeof( DDRelicReagent ),				typeof( DDRelicOrbs ),
				typeof( DDRelicVase ),			typeof( DDRelicPainting ),				typeof( DDRelicArts ),
				typeof( DDRelicStatue ), 		typeof( DDRelicRugAddonDeed ),			typeof( DDRelicWeapon ),
				typeof( DDRelicArmor ),			typeof( DDRelicJewels ),				typeof( DDRelicInstrument ),
				typeof( DDRelicScrolls ),		typeof( DDRelicCloth ),					typeof( DDRelicFur ),
				typeof( DDRelicDrink ),			typeof( DDRelicReagent ),				typeof( DDRelicOrbs ),
				typeof( DDRelicBearRugsAddonDeed ), typeof( DDRelicLeather ),				typeof( DDRelicAlchemy ),
				typeof( DDRelicBook ),			typeof( DDRelicBook ),					typeof( DDRelicBook ),
				typeof( DDRelicTablet ),		typeof( DDRelicGem ),					typeof( DDRelicBanner )
			};
		public static Type[] RelicTypes{ get{ return m_RelicTypes; } }

		private static Type[] m_SeaTypes = new Type[]
			{
				typeof( AdmiralsHeartyRum ),
				typeof( ShipModelOfTheHMSCape ),
				typeof( SeahorseStatuette ),
				typeof( GhostShipAnchor ),
				typeof( AquariumEastAddonDeed ),
				typeof( LightHouseAddonDeed ),
				typeof( MarlinEastAddonDeed ),
				typeof( MarlinSouthAddonDeed ),
				typeof( DolphinSouthSmallAddonDeed ),
				typeof( SkullEastLargeAddonDeed ),
				typeof( SkullEastSmallAddonDeed ),
				typeof( SkullSouthLargeAddonDeed ),
				typeof( SkullSouthSmallAddonDeed ),
				typeof( DolphinSouthLargeAddonDeed ),
				typeof( DolphinEastLargeAddonDeed ),
				typeof( AquariumSouthAddonDeed ),
				typeof( DolphinEastSmallAddonDeed ),
				typeof( SeaShell )
			};
		public static Type[] SeaTypes{ get{ return m_SeaTypes; } }

		private static Type[] m_SArtyTypes = new Type[]
			{
				typeof( GoldBricks ),				typeof( BedOfNailsDeed ),			typeof( DecoGinsengRoot ),		typeof( DecoRoseOfTrinsic ),
				typeof( PhillipsWoodenSteed ),		typeof( BoneCouchDeed ),			typeof( DecoGinsengRoot2 ),		typeof( DecoRoseOfTrinsic2 ),
				typeof( AlchemistsBauble ),			typeof( BoneTableDeed ),			typeof( DecoMandrake ),			typeof( DecoRoseOfTrinsic3 ),
				typeof( SoulStone ),				typeof( BoneThroneDeed ),			typeof( DecoMandrake2 ),		typeof( BrokenChair ),
				typeof( RedSoulstone ),				typeof( CreepyPortraitDeed ),		typeof( DecoMandrake3 ),		typeof( EmptyJar ),
				typeof( BlueSoulstone ),			typeof( DisturbingPortraitDeed ),	typeof( DecoMandrakeRoot ),		typeof( DecoFullJar ),
				typeof( MinotaurStatueDeed ),		typeof( HaunterMirrorDeed ),		typeof( DecoMandrakeRoot2 ),	typeof( HalfEmptyJar ),
				typeof( HangingSkeletonDeed ),		typeof( DirtPatch ),				typeof( DecoNightshade ),		typeof( DecoCrystalBall ),
				typeof( FlamingHeadDeed ),			typeof( EvilIdolSkull ),			typeof( DecoNightshade2 ),		typeof( DecoMagicalCrystal ),
				typeof( RewardBrazierDeed ),		typeof( WallBlood ),				typeof( DecoNightshade3 ),		typeof( DecoSpittoon ),
				typeof( BloodyPentagramDeed ),		typeof( SkullPole ),				typeof( DecoObsidian ),			typeof( DecoDeckOfTarot ),
				typeof( AnkhOfSacrificeDeed ),		typeof( MonsterStatueDeed ),		typeof( DecoPumice ),			typeof( DecoDeckOfTarot2 ),
				typeof( WeaponEngravingTool ),		typeof( DecoStatueDeed ),			typeof( DecoWyrmsHeart ),		typeof( DecoTarot ),
				typeof( IronMaidenDeed ),			typeof( GrizzledMareStatuette ),	typeof( DecoArrowShafts ),		typeof( DecoTarot2 ),
				typeof( StoneStatueDeed ),			typeof( TormentedChains ),			typeof( CrossbowBolts ),		typeof( DecoTarot3 ),
				typeof( MountedPixieWhiteDeed ),	typeof( DecoBlackmoor ),			typeof( EmptyToolKit ),			typeof( DecoTarot4 ),
				typeof( MountedPixieBlueDeed ),		typeof( DecoBloodspawn ),			typeof( EmptyToolKit2 ),		typeof( DecoTarot5 ),
				typeof( MountedPixieGreenDeed ),	typeof( DecoBrimstone ),			typeof( Lockpicks ),			typeof( DecoTarot6 ),
				typeof( MountedPixieLimeDeed ),		typeof( DecoDragonsBlood ),			typeof( ToolKit ),				typeof( DecoTarot7 ),
				typeof( MountedPixieOrangeDeed ),	typeof( DecoDragonsBlood2 ),		typeof( UnfinishedBarrel ),		typeof( Cards ),
				typeof( SacrificialAltarDeed ),		typeof( DecoEyeOfNewt ),			typeof( DecoRock2 ),			typeof( Cards2 ),
				typeof( UnsettlingPortraitDeed ),	typeof( DecoGarlic ),				typeof( DecoRocks ),			typeof( Cards3 ),
				typeof( GuillotineDeed ),			typeof( DecoGarlic2 ),				typeof( DecoRocks2 ),			typeof( Cards4 ),
				typeof( WindSpirit ),				typeof( DecoGarlicBulb ),			typeof( DecoRock ),				typeof( DecoCards5 ),
				typeof( SuitOfGoldArmorDeed ),		typeof( DecoGarlicBulb2 ),			typeof( DecoFlower ),			typeof( PlayingCards ),
				typeof( SuitOfSilverArmorDeed ),	typeof( DecoGinseng ),				typeof( DecoFlower2 ),			typeof( PlayingCards2 ),
				typeof( WoodenCoffinDeed ),			typeof( DecoGinseng2 ),				typeof( JokeBook ),				typeof( HorseArmor ),
				typeof( Dice4 ),					typeof( Dice6 ),					typeof( Dice8 ),				typeof( Dice10 ),
				typeof( Dice12 ),					typeof( Dice20 ),					typeof( MonsterManual ),		typeof( PlayersHandbook ),
				typeof( DragonOrbStatue ),		typeof( WizardsStatue ),
				typeof( DungeonMastersGuide ),		typeof( GygaxStatue ), 				typeof( AwesomeDisturbingPortraitDeed )
			};
		public static Type[] SArtyTypes{ get{ return m_SArtyTypes; } }
		//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private static Type[] m_SEClothingTypes = new Type[]
			{
				typeof( ClothNinjaJacket ),		typeof( FemaleKimono ),			typeof( Hakama ),
				typeof( HakamaShita ),			typeof( JinBaori ),				typeof( Kamishimo ),
				typeof( MaleKimono ),			typeof( NinjaTabi ),			typeof( Obi ),
				typeof( SamuraiTabi ),			typeof( TattsukeHakama ),		typeof( Waraji ),
				typeof( LeatherNinjaBelt )
			};

		public static Type[] SEClothingTypes{ get{ return m_SEClothingTypes; } }

		private static Type[] m_AosClothingTypes = new Type[]
			{
				typeof( FurSarong ),			typeof( FurCape ),				typeof( FlowerGarland ),
				typeof( GildedDress ),			typeof( FurBoots ),				typeof( FormalShirt ),
				typeof( WhiteFurRobe ),			typeof( WhiteFurBoots ), 		typeof( WhiteFurSarong ),
				typeof( FurRobe ),				typeof( Boots ),				typeof( Sandals ),
				typeof( ThighBoots )
		};

		public static Type[] AosClothingTypes{ get{ return m_AosClothingTypes; } }

		private static Type[] m_ClothingTypes = new Type[]
			{
				typeof( Bonnet ),               typeof( Cap ),		            typeof( FeatheredHat ),
				typeof( FloppyHat ),            typeof( JesterHat ),			typeof( Surcoat ),
				typeof( SkullCap ),             typeof( StrawHat ),	            typeof( TallStrawHat ),
				typeof( TricorneHat ),			typeof( WideBrimHat ),          typeof( WizardsHat ),
				typeof( ReaperHood ),			typeof( ReaperCowl ),			typeof( FancyHood ),
				typeof( Boots ),				typeof( Sandals ),				typeof( Shoes ),
				typeof( ThighBoots ),			typeof( Boots ),				typeof( Sandals ),
				typeof( Shoes ),				typeof( ThighBoots ),			typeof( WitchHat ),
				typeof( BodySash ),             typeof( Doublet ),				typeof( FancyShirt ),
				typeof( FullApron ),            typeof( JesterSuit ),  			typeof( ClothHood ),         
				typeof( Tunic ),				typeof( Shirt ),				typeof( ClothCowl ),
				typeof( FancyDress ),			typeof( PlainDress ),           typeof( Robe ),
				typeof( JokerRobe ),			typeof( AssassinRobe ),			typeof( FancyRobe ),
				typeof( GildedRobe ),			typeof( OrnateRobe ),			typeof( MagistrateRobe ),
				typeof( RoyalRobe ),			typeof( SorcererRobe ),			typeof( AssassinShroud ),
				typeof( NecromancerRobe ),		typeof( SpiderRobe ),			typeof( PirateHat ),
				typeof( HalfApron ), 			typeof( LoinCloth ),			typeof( VagabondRobe), 
				typeof( ShortPants ),			typeof( LongPants ),			typeof( Kilt ),
				typeof( Skirt ),				typeof( ShortPants ),			typeof( LongPants ),
				typeof( Kilt ),					typeof( Skirt ),				typeof( WhiteFurCape ),			
				typeof( Cloak ),				typeof( RoyalCape ), 			typeof( PirateCoat ),
				typeof( Cloak ),				typeof( RoyalCape ), 			typeof( JokerHat ),
				typeof( Cloak ),				typeof( RoyalCape ),			typeof( FoolsCoat ),
				typeof( JesterGarb ),			typeof( FurRobe ), 				typeof( FurCap ),
				typeof( FurCape ),				typeof( WarriorsTunic ),		typeof( ThiefTunic ),	
				typeof( KnightsRobe ),			typeof( GladiatorSash ),		typeof( GladiatorClasp ),	
				typeof ( ThiefPaulettes ),
				typeof( ExquisiteRobe ),		typeof( ProphetRobe ),			typeof( ElegantRobe ),
				typeof( FormalRobe ),			typeof( ArchmageRobe ),			typeof( PriestRobe ),
				typeof( CultistRobe ),			typeof( GildedDarkRobe ),		typeof( GildedLightRobe ),
				typeof( SageRobe ),				typeof( RoyalCoat ),			typeof( RoyalShirt ),
				typeof( RusticShirt ),			typeof( SquireShirt ),			typeof( FormalCoat ),
				typeof( WizardShirt ),			typeof( BeggarVest ),			typeof( RoyalVest ),
				typeof( RusticVest ),			typeof( SailorPants ),			typeof( PiratePants ),
				typeof( DeadMask ),				typeof( WizardHood ),			typeof( RoyalSkirt ),
				typeof( RoyalLongSkirt ),		typeof( BarbarianBoots )
			};
		public static Type[] ClothingTypes{ get{ return m_ClothingTypes; } }

		private static Type[] m_SEHatTypes = new Type[]
			{
				typeof( ClothNinjaHood ),		typeof( Kasa ), 				typeof( Bandana )
			};

		public static Type[] SEHatTypes{ get{ return m_SEHatTypes; } }

		private static Type[] m_AosHatTypes = new Type[]
			{
				typeof( FlowerGarland ),	typeof( BearMask ),		typeof( DeerMask ),		typeof( StagMask), 
				typeof( WolfMask ), 		typeof( WhiteFurCap ), 	typeof( FurCap )
			};

		public static Type[] AosHatTypes{ get{ return m_AosHatTypes; } }

		private static Type[] m_HatTypes = new Type[]
			{
				typeof( SkullCap ),			typeof( Bandana ),		typeof( FloppyHat ),
				typeof( Cap ),				typeof( WideBrimHat ),	typeof( StrawHat ),
				typeof( TallStrawHat ),		typeof( WizardsHat ),	typeof( Bonnet ),
				typeof( WitchHat ),			typeof( ClothCowl ),	typeof( ClothHood ),
				typeof( FeatheredHat ),		typeof( TricorneHat ),	typeof( JesterHat ),
				typeof( PirateHat ),		typeof( JokerHat ),		typeof( FancyHood ),
				typeof( DeadMask ),			typeof( WizardHood )
			};

		public static Type[] HatTypes{ get{ return m_HatTypes; } }

		#endregion

		#region Accessors

		public static bool IsArtefact(Item i)
		{
			if (i == null)
				return false; 

			foreach( Type t in Loot.ArtyTypes )
			{
			    if (t != null && i.GetType().Equals(t))
					return true;
			}
			return false;

		}

		public static BaseMagicStaff RandomWand()
		{
			return Construct( m_WandTypes ) as BaseMagicStaff;
		}
		
		public static BaseBook RandomLibraryBook()
		{
			return Construct( m_LibraryBookTypes ) as BaseBook;
		}

		public static BaseClothing RandomClothing()
		{
			return RandomClothing( false );
		}

		public static BaseClothing RandomClothing( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEClothingTypes ) as BaseClothing;

			return Construct( m_AosClothingTypes, m_ClothingTypes ) as BaseClothing;
		}

		public static BaseWeapon RandomRangedWeapon()
		{
			return RandomRangedWeapon( false );
		}

		public static BaseWeapon RandomRangedWeapon( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SERangedWeaponTypes ) as BaseWeapon;

			return Construct( m_AosRangedWeaponTypes, m_RangedWeaponTypes ) as BaseWeapon;
		}

		public static BaseWeapon RandomWeapon()
		{
			return RandomWeapon( false );
		}

		public static BaseWeapon RandomWeapon( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEWeaponTypes ) as BaseWeapon;

			return Construct( m_AosWeaponTypes, m_WeaponTypes ) as BaseWeapon;
		}

		public static Item RandomWeaponOrJewelry()
		{
			return RandomWeaponOrJewelry( false );
		}

		public static Item RandomWeaponOrJewelry( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEWeaponTypes, m_JewelryTypes );

			return Construct( m_AosWeaponTypes, m_WeaponTypes, m_JewelryTypes );
		}

		public static BaseJewel RandomJewelry()
		{
			return Construct( m_JewelryTypes ) as BaseJewel;
		}

		public static BaseArmor RandomArmor()
		{
			return RandomArmor( false );
		}

		public static BaseArmor RandomArmor( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEArmorTypes ) as BaseArmor;

			if ( 1 == Utility.Random( 1000 ) )
				return Construct( m_DragonArmorTypes ) as BaseArmor;

			return Construct( m_ArmorTypes ) as BaseArmor;
		}

		public static BaseHat RandomHat()
		{
			return RandomHat( false );
		}

		public static BaseHat RandomHat( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEHatTypes ) as BaseHat;

			return Construct( m_AosHatTypes, m_HatTypes ) as BaseHat;
		}

		public static Item RandomArmorOrHatOrClothes()
		{
			return RandomArmorOrHatOrClothes( false );
		}

		public static Item RandomArmorOrHatOrClothes( bool inIslesDread )
		{
			if ( inIslesDread )
				return Construct( m_SEArmorTypes, m_SEHatTypes, m_SEClothingTypes );

			if ( 1 == Utility.Random( 2000 ) )
				return Construct( m_DragonArmorTypes );

			return Construct( m_ArmorTypes, m_ArmorTypes, m_ArmorTypes, m_ArmorTypes, m_AosHatTypes, m_HatTypes, m_AosClothingTypes, m_ClothingTypes );
		}

		public static BaseShield RandomShield()
		{
			return Construct( m_AosShieldTypes, m_ShieldTypes ) as BaseShield;
		}

		public static BaseArmor RandomArmorOrShield()
		{
			return RandomArmorOrShield( false );
		}

		public static BaseArmor RandomArmorOrShield( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEArmorTypes, m_AosShieldTypes, m_ShieldTypes ) as BaseArmor;

			if ( 1 == Utility.Random( 2000 ) )
				return Construct( m_DragonArmorTypes ) as BaseArmor;

			return Construct( m_ArmorTypes, m_AosShieldTypes, m_ShieldTypes ) as BaseArmor;
		}

		public static Item RandomArmorOrShieldOrJewelry()
		{
			return RandomArmorOrShieldOrJewelry( false );
		}

		public static Item RandomArmorOrShieldOrJewelry( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEArmorTypes, m_SEHatTypes, m_AosShieldTypes, m_ShieldTypes, m_JewelryTypes );

			if ( 1 == Utility.Random( 3000 ) )
				return Construct( m_DragonArmorTypes );

			return Construct( m_ArmorTypes, m_AosHatTypes, m_HatTypes, m_AosShieldTypes, m_ShieldTypes, m_JewelryTypes );
		}

		public static Item RandomArmorOrShieldOrWeapon()
		{
			return RandomArmorOrShieldOrWeapon( false );
		}

		public static Item RandomArmorOrShieldOrWeapon( bool inTokuno )
		{
			if ( inTokuno )
				return Construct( m_SEWeaponTypes, m_SERangedWeaponTypes, m_SEArmorTypes, m_SEHatTypes, m_AosShieldTypes, m_ShieldTypes );

			if ( 1 == Utility.Random( 3000 ) )
				return Construct( m_DragonArmorTypes );

			return Construct( m_AosWeaponTypes, m_WeaponTypes, m_AosRangedWeaponTypes, m_RangedWeaponTypes, m_ArmorTypes, m_AosHatTypes, m_HatTypes, m_AosShieldTypes, m_ShieldTypes );
		}

		public static Item RandomArmorOrShieldOrWeaponOrJewelryOrClothing()
		{
			return RandomArmorOrShieldOrWeaponOrJewelryOrClothing( false );
		}

		public static Item RandomArmorOrShieldOrWeaponOrJewelryOrClothing( bool inIslesDread )
		{
			if ( inIslesDread )
				return Construct( m_SEWeaponTypes, m_SERangedWeaponTypes, m_SEArmorTypes, m_SEHatTypes, m_AosShieldTypes, m_ShieldTypes, m_JewelryTypes, m_SEClothingTypes );

			if ( 1 == Utility.Random( 4000 ) )
				return Construct( m_DragonArmorTypes ) as BaseArmor;

			return Construct( m_AosWeaponTypes, m_WeaponTypes, m_AosRangedWeaponTypes, m_RangedWeaponTypes, m_ArmorTypes, m_AosHatTypes, m_HatTypes, m_AosShieldTypes, m_ShieldTypes, m_JewelryTypes, m_AosClothingTypes, m_ClothingTypes );
		}
		
		#region Chest of Heirlooms
		public static Item ChestOfHeirloomsContains()
		{
			return Construct( m_SEArmorTypes, m_SEHatTypes, m_SEWeaponTypes, m_SERangedWeaponTypes, m_JewelryTypes );
		}
		#endregion

		public static Item RandomGem()
		{
			return Construct( m_GemTypes );
		}

		public static Item RandomArty()
		{
			return Construct( m_ArtyTypes );
		}
		public static Item RandomSArty()
		{
			return Construct( m_SArtyTypes );
		}
		public static Item RandomRelic()
		{
			return Construct( m_RelicTypes );
		}

		public static Item RandomSea()
		{
			return Construct( m_SeaTypes );
		}

		public static Item RandomSecretReagent()
		{
			return Construct( m_SecretRegTypes );
		}
		////////////////////////////////////////////////////

		public static Item RandomReagent()
		{
			return Construct( m_RegTypes );
		}

		public static Item RandomNecromancyReagent()
		{
			return Construct( m_NecroRegTypes );
		}

		public static Item RandomMixerReagent()
		{
			return Construct( m_MixerRegTypes );
		}

		public static Item RandomHerbReagent()
		{
			return Construct( m_HerbRegTypes );
		}

		public static Item RandomPossibleReagent()
		{
			return Construct( m_RegTypes, m_NecroRegTypes, m_MixerRegTypes );
		}

		public static Item RandomPotion()
		{
			if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
				return Construct( m_UPotionTypes );

			return Construct( m_PotionTypes );
		}

		public static BaseInstrument RandomInstrument()
		{
			if ( Core.SE )
				return Construct( m_InstrumentTypes, m_SEInstrumentTypes ) as BaseInstrument;

			return Construct( m_InstrumentTypes ) as BaseInstrument;
		}

		public static Item RandomStatue()
		{
			return Construct( m_StatueTypes );
		}

		public static BaseQuiver RandomQuiver()
		{
			return Construct( m_QuiverTypes ) as BaseQuiver;
		}

		public static SpellScroll RandomScroll( int minIndex, int maxIndex, SpellbookType type )
		{
			Type[] types;

			switch ( type )
			{
				default:
				case SpellbookType.Regular: types = m_RegularScrollTypes; break;
				case SpellbookType.Necromancer: types = (Core.SE ? m_SENecromancyScrollTypes : m_NecromancyScrollTypes ); break;
				case SpellbookType.Arcanist: types = m_ArcaneScrollTypes; break;
			}

			return Construct( types, Utility.RandomMinMax( minIndex, maxIndex ) ) as SpellScroll;
		}

		#endregion

		#region Construction methods
		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}

		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		public static Item Construct( params Type[][] types )
		{
			int totalLength = 0;

			for ( int i = 0; i < types.Length; ++i )
				totalLength += types[i].Length;

			if ( totalLength > 0 )
			{
				int index = Utility.Random( totalLength );

				for ( int i = 0; i < types.Length; ++i )
				{
					if ( index >= 0 && index < types[i].Length )
						return Construct( types[i][index] );

					index -= types[i].Length;
				}
			}

			return null;
		}
		#endregion
	}
}
