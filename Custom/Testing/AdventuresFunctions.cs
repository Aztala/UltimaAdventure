using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Engines.CannedEvil;
using Felladrin.Automations;

namespace Server.Commands
{
	public class AdventuresCommands
	{
		public static void Initialize()
		{
			CommandSystem.Register( "ResetMobs", AccessLevel.Administrator, new CommandEventHandler( ResetMobs_OnCommand ) );
			CommandSystem.Register( "ResetMobs", AccessLevel.Administrator, new CommandEventHandler( ResetTamedMobs_OnCommand ) );
			CommandSystem.Register( "ClearCorpses", AccessLevel.Administrator, new CommandEventHandler( clearcorpses_OnCommand ) );
			CommandSystem.Register( "ClearNullItems", AccessLevel.Administrator, new CommandEventHandler( clearnullitems_OnCommand ) );
			CommandSystem.Register( "ClearNullMobs", AccessLevel.Administrator, new CommandEventHandler( clearnullmobs_OnCommand ) );
			CommandSystem.Register( "ClearLightSourceItems", AccessLevel.GameMaster, new CommandEventHandler( clearlightsourceitems_OnCommand ) );
			CommandSystem.Register( "ClearClones", AccessLevel.GameMaster, new CommandEventHandler( clearclones_OnCommand ) );
			CommandSystem.Register( "SimulateInvasion", AccessLevel.GameMaster, new CommandEventHandler( simulateinvasion_OnCommand ) );
			CommandSystem.Register( "InvasionStart", AccessLevel.GameMaster, new CommandEventHandler( startinvasion_OnCommand ) );
			CommandSystem.Register( "FixLevelItems", AccessLevel.GameMaster, new CommandEventHandler( fixlevelitems_OnCommand ) );
			CommandSystem.Register( "NerfBogus", AccessLevel.GameMaster, new CommandEventHandler( nerfbogus_OnCommand ) );
			CommandSystem.Register( "ResetDiscovered", AccessLevel.GameMaster, new CommandEventHandler( resetDiscovered_OnCommand ) );
			CommandSystem.Register( "AdjustCorpses", AccessLevel.GameMaster, new CommandEventHandler( adjustCorpses_OnCommand ) );
			CommandSystem.Register( "ClearWayPoints", AccessLevel.GameMaster, new CommandEventHandler( clearwaypoints_OnCommand ) );
			CommandSystem.Register( "ClearVendorPacks", AccessLevel.GameMaster, new CommandEventHandler( clearvendorpacks_OnCommand ) );
			CommandSystem.Register( "CleanupInternal", AccessLevel.GameMaster, new CommandEventHandler( cleanupinternal_OnCommand ) );

		}

		[Usage( "CleanupInternal" )]
		[Description( "Calculate and clean up the internal objects." )]
		public static void cleanupinternal_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.CleanupInternalObjects( e.Mobile, true );
		}

		[Usage( "AdjustCorpses" )]
		[Description( "Changes weight for bones" )]
		public static void adjustCorpses_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is Corpse )
			{

				Corpse bod = (Corpse)body;
				BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 2 );

				if ( bod.Owner is PlayerMobile && house != null )
				{
					bod.Weight = 0.1;
				}

			}
		}

		[Usage( "ResetDiscovered" )]
		[Description( "resets discovered locations" )]
		public static void resetDiscovered_OnCommand( CommandEventArgs e )
		{
			string name = null;
			if( e.Length >= 1 )
			{
 				name = e.Arguments[0];
			}

			Mobile player = null;

			ArrayList players = new ArrayList();
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is PlayerMobile && m.Name == name)
                {
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
					string discovered = DB.CharacterDiscovered;

					discovered = "0#0#0#0#0#0#0#0#0#0#"; 
					e.Mobile.Say("I've reset "+ name + "'s discovered lands records.");
                }
            }
		}



		[Usage( "ResetMobs" )]
		[Description( "resets basecreatures." )]
		public static void ResetMobs_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Summoned && !bc.Controlled && !bc.IsHitchStabled && !bc.IsStabled && bc.ControlMaster == null) // make sure not stabled or a pet
                    {
                        bc.DynamicFameKarma();
			            bc.DynamicTaming();
						bc.DynamicGold();
                    }
                }
            }
		}

		[Usage( "ResetTamedMobs" )]
		[Description( "resets tamed basecreatures." )]
		public static void ResetTamedMobs_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Summoned && ( bc.Controlled || bc.IsHitchStabled || bc.IsStabled ) ) // make sure not stabled or a pet
                    {
                        bc.DynamicFameKarma();
			            bc.DynamicTaming();	
                    }
                }
            }
		}

		[Usage( "ClearCorpses" )]
		[Description( "Clears empty corpses" )]
		public static void clearcorpses_OnCommand( CommandEventArgs e )
		{
			ArrayList bodies = new ArrayList();
			foreach ( Item body in World.Items.Values ) 
			{
				if ( body is Corpse )
				{
					if ( ((Corpse)body).Owner != null && ((Corpse)body).Owner is PlayerMobile)
					{
						int carrying = body.GetTotal( TotalType.Items );
						BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 2 );

						if ( house == null && carrying == 0 )
						{
							bodies.Add(body);
						}
					}

				}
			}
			for ( int i = 0; i < bodies.Count; ++i )
			{
				Item NM = ( Item )bodies[ i ];
				Item corpseitem = new CorpseItem();
				corpseitem.Name = "the bones of " +NM.Name;
				corpseitem.MoveToWorld( NM.Location, NM.Map );
				NM.Delete();
			}
		}

		[Usage( "ClearNullItems" )]
		[Description( "Clears items with no itemids" )]
		public static void clearnullitems_OnCommand( CommandEventArgs e )
		{
			foreach ( Item thing in World.Items.Values ) 
			if ( thing.ItemID == null )
			{

				thing.Delete();

			}
		}

		[Usage( "ClearWayPoints" )]
		[Description( "Clears waypoints in world" )]
		public static void clearwaypoints_OnCommand( CommandEventArgs e )
		{
			foreach ( Item thing in World.Items.Values ) 
			if ( thing is WayPoint )
			{
				Region reg = Region.Find( thing.Location, thing.Map );
				if (reg.IsPartOf("Peter's Prison"))
				{
					thing.Delete();
				}

			}
		}

		[Usage( "ClearNullMobs" )]
		[Description( "Clears mobs with null maps" )]
		public static void clearnullmobs_OnCommand( CommandEventArgs e )
		{
			ArrayList nullmobs = new ArrayList();
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Controlled && !bc.IsHitchStabled && !bc.IsStabled && bc.Map == null) 
                    {
						nullmobs.Add( bc );
                    }
					Point3D loc = bc.Location;
					if (loc.X == 0 && loc.Y == 0)
						nullmobs.Add( bc );
                }
            }
			for ( int i = 0; i < nullmobs.Count; ++i )
			{
				Mobile NM = ( Mobile )nullmobs[ i ];
				Console.WriteLine( "deleting " + NM);
				NM.Delete();
			}
		}

		[Usage( "ClearLightSourceItems" )]
		[Description( "Clears lightsource items caused by SB bug" )]
		public static void clearlightsourceitems_OnCommand( CommandEventArgs e )
		{
			List<Item> items = new List<Item>(World.Items.Values);
			int counter = 1;
			foreach ( Item thing in items.ToArray()) {
				object parent = thing.Parent;
				if (parent == null && thing is LighterSource) {
					Mobile heldBy = thing.HeldBy;
					if (heldBy == null) {
						thing.Delete();
					}
				} 
				counter++;
			}
		}

		[Usage( "ClearClones" )]
		[Description( "Clears clones that have less than 175 stats" )]
		public static void clearclones_OnCommand( CommandEventArgs e )
		{
            foreach (var mobile in new List<Mobile>(World.Mobiles.Values))
			{
                if (mobile is CloneCharacterOnLogout.CharacterClone )
				{
					Mobile playr = ((CloneCharacterOnLogout.CharacterClone)mobile).Original;
					if ( (playr.RawStr + playr.RawInt + playr.RawDex) < 150)
						mobile.Delete();
				}
			}
                    
		}

		[Usage( "SimulateInvasion" )]
		[Description( "Runs daily invasion routine" )]
		public static void simulateinvasion_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.InvasionRoutine();            
		}

		[Usage( "StartInvasion" )]
		[Description( "Starts Invasion" )]
		public static void startinvasion_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.InvasionRoutine( true );            
		}


		[Usage( "FixLevelItems" )]
		[Description( "fixes levellable items" )]
		public static void fixlevelitems_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is ILevelable )
			{
				ILevelable arty = (ILevelable)body;
				arty.MaxLevel = LevelItems.MaxLevelsCap;
			}
		}

		[Usage( "ClearnVendorPacks" )]
		[Description( "Clears npc vendor packs" )]
		public static void clearvendorpacks_OnCommand( CommandEventArgs e )
		{
			int counter = 0;
			foreach ( Item pack in World.Items.Values ) 
			if ( pack is Container && pack.Layer == Layer.ShopBuy )
			{
					List<Item> belongings = new List<Item>();
					foreach( Item i in pack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}
			}
			e.Mobile.SendMessage(0, counter + " items removed from the game");
		}

		[Usage( "NerfBogus" )]
		[Description( "nerfs bogus sword" )]
		public static void nerfbogus_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is HellSword )
			{
				HellSword sword = (HellSword)body;
			sword.WeaponAttributes.DurabilityBonus = 0; // Pick and choose the attributes for your weapon (remember to remove the // before the ones you entend to use)
			sword.WeaponAttributes.HitColdArea = 0;
			sword.WeaponAttributes.HitDispel = 10;
			sword.WeaponAttributes.HitEnergyArea = 0;
			sword.WeaponAttributes.HitFireArea = 0;
			sword.WeaponAttributes.HitFireball = 25;
			sword.WeaponAttributes.HitHarm = 0;
			sword.WeaponAttributes.HitLeechHits = 20;
			sword.WeaponAttributes.HitLeechMana = 20;
			sword.WeaponAttributes.HitLeechStam = 20;                                   
			sword.WeaponAttributes.HitLightning = 0;
			sword.WeaponAttributes.HitLowerAttack = 0;
			sword.WeaponAttributes.HitLowerDefend = 0;
			sword.WeaponAttributes.HitMagicArrow = 0;
			sword.WeaponAttributes.HitPhysicalArea = 0;
			sword.WeaponAttributes.HitPoisonArea = 0;
			sword.WeaponAttributes.LowerStatReq = 0;
			sword.WeaponAttributes.MageWeapon = 0;    
			sword.WeaponAttributes.ResistColdBonus = 0;
			sword.WeaponAttributes.ResistEnergyBonus = 2;
			sword.WeaponAttributes.ResistPhysicalBonus = 4;
			sword.WeaponAttributes.ResistFireBonus = 15;
			sword.WeaponAttributes.ResistPoisonBonus = 2;
			sword.WeaponAttributes.SelfRepair = 0;

			sword.Attributes.AttackChance = 15;
			sword.Attributes.BonusDex = 10;
			sword.Attributes.BonusHits = 0;
			sword.Attributes.BonusInt = 0;
			sword.Attributes.BonusMana = 0;
			sword.Attributes.BonusStam = 10;
			sword.Attributes.BonusStr = 5;
			sword.Attributes.CastRecovery = 0;
			sword.Attributes.CastSpeed = 0;
			sword.Attributes.DefendChance = 0;
			sword.Attributes.EnhancePotions = 0;
			sword.Attributes.LowerRegCost = 0;
			sword.Attributes.Luck = 100;
			sword.Attributes.SpellChanneling = 1;
			sword.Attributes.SpellDamage = 0;


			}
		}
		
	}
}

namespace Server.Misc
{
	public class AdventuresFunctions
	{

		public static List<String> InfectedRegions = new List<String>();

		public static bool RegionIsInfected (String region )
		{

			if ( region == "the Widow's Keep" || region == "Widow's Lament")
				return false;
				
			if (InfectedRegions == null)
                InfectedRegions = new List<String>();

			for ( int i = 0; i < InfectedRegions.Count; i++ ) // check if region is in list
			{			
				String r = (String)InfectedRegions[i];
				if (r == region) //is in the list
					return true;
			}
			return false; //not infected
		}

		public static String GetNameFromRegion( Region reg )
		{
			if (reg == null)
			{
				//Console.WriteLine("getnamefromregion was null");  // +++ debug
				return null;
			}

			string name = null;

			foreach ( Mobile mob in reg.GetMobiles() )
			{

				Point3D location = mob.Location;
				Map map = mob.Map;

				if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( map, location ) ) )
				{
					name = Server.Misc.Worlds.GetRegionName( map, location ) ;
					continue;
				}
				else
				{
					Region regs = mob.Region;

					if ( !regs.IsDefault )
					{	
						StringBuilder builder = new StringBuilder();

						builder.Append( regs.ToString() );
						regs= regs.Parent;

						while ( regs != null )
						{
							builder.Append( " in " + regs.ToString() );
							regs = regs.Parent;
						}

						name = builder.ToString() ;
						continue;
					}
				}

			}
			if (name == null)
				name = "Unknown Location";
			return name;
		}



		public static void CheckInfection()
		{
			if (InfectedRegions == null)
                InfectedRegions = new List<String>();

			ArrayList newregions = new ArrayList();

			newregions.Clear();

			if (InfectedRegions.Count > 0 )
				InfectedRegions.Clear();

			foreach ( Mobile mob in World.Mobiles.Values )
			{
				if ( mob is BaseCreature && mob.Map != null )
				{
					if ( (mob is BaseUndead ||  ((BaseCreature)mob).CanInfect) && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp"))
					{

						String reg = null;
						if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) ) )
						{
							reg = Worlds.GetMyWorld( mob.Map, mob.Location, mob.X, mob.Y );
						}
						else
							reg = Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) ;

						if (reg != null ) // add all infected regions to a new list called newregions
						{
							bool add = false;
							
							if (newregions.Count == 0) 
								add = true;
							else
							{
								for ( int i = 0; i < newregions.Count; i++ ) // check if newregion is in list
								{			
									String re = (String)newregions[i];
									if (reg != re) // prevents duplication
									{
										add = true;
									}
								}							
							}
		
							if (add)
								newregions.Add( reg ); // add to a new list to compare with the static

							if ( !RegionIsInfected( reg ) )
								InfectedRegions.Add( reg ); // infected mob is in a region that isn't in the list
						}
					}
				}
			}

/*			if ( InfectedRegions.Count > 0 ) // now check if any previously infected regions are no longer infected
			{
				for ( int i = 0; i < InfectedRegions.Count; i++ ) // load static regions
				{			
					String r = (String)InfectedRegions[i];
					bool keep = false;

					for ( int ii = 0; ii < newregions.Count; ii++ ) // compare static with new list
					{			
						String rr = (String)newregions[ii];

						if ( rr == r ) // previously infected region is still infected
							keep = true;
					}

					if (!keep)
						InfectedRegions.Remove( r ); // old infected region is no longer in the newregions list, therefore no longer infected
				} 
			}*/

			if (InfectedRegions.Count == 0 && AetherGlobe.invasionstage > 0 ) // mobs were pushed back
				AetherGlobe.invasionstage -=1;
				
		}

		public static void InvasionRoutine()
		{
			InvasionRoutine( false);
		}


		public static void InvasionRoutine( bool start)
		{
			bool newinvasion = false;
			if (AetherGlobe.invasionstage == 0)// currently no invasion, 5% chance of having one
			{
				if (Utility.RandomDouble() <= 0.05 || start )
				{
					AetherGlobe.invasionstage = 1;
					newinvasion = true;
				}
				else
				{
					// cleanup invasion spawns
					ArrayList infected = new ArrayList();
					foreach ( Mobile bitches in World.Mobiles.Values )
					{
						if (bitches is BaseCreature)
						{
							if ( bitches is WanderingConcubine || ( ((BaseCreature)bitches).CanInfect && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "the Toxic Swamp")))
							{
								infected.Add(bitches);
							}
						}

					}
					for ( int i = 0; i < infected.Count; i++ ) 
					{			
						Mobile re = (Mobile)infected[i];
						re.Delete();
					}	
					return;
				}
			}

			if (AetherGlobe.invasionstage >= 1 && ( Utility.RandomDouble() < 0.80 || newinvasion))  // once invasion startted, 1/5 chance of moving to another place
			{
				if (Utility.RandomDouble() <= 0.10 && AetherGlobe.invasionstage < 3 )// 10% chance of moving to next stage
					AetherGlobe.invasionstage += 1;

				AetherGlobe.intensity = 0.05 * (double)AetherGlobe.invasionstage;

				Mobile bitch = null;
				// find concubine
				foreach ( Mobile bitches in World.Mobiles.Values )
				{
					if ( bitches is WanderingConcubine ) // move the bitch
					{
						bitch = bitches;
					}
				}

				if (bitch == null)
				{
					bitch = new WanderingConcubine();
					AetherGlobe.carrier = bitch.Name;
				}

				//Step 1, pick a world to infect
				int whatever = Utility.RandomMinMax(1, 100);
				String world = null;
				if (whatever <= 25) // 25%
					world = "the Isles of Dread";
				else if (whatever <= 30) // 5%
					world = "the Land of Sosaria";
				else if (whatever <= 45) // 15%
					world = "the Land of Lodoria";
				else if (whatever <= 65) // 20%
					world = "the Savaged Empire";
				else if (whatever <= 85) // 20%
					world = "the Serpent Island";
				else if (whatever <= 95) // 10%
					world = "the Island of Umber Veil";
				else  // 5%
					world = "DarkMoor";

				// pick what to infect - 10% city, 60% overland, 30% dungeon
				whatever = Utility.RandomMinMax(1, 100);
				int distance = 50;

				Point3D gagme;
				if (whatever <= 10)
					gagme = Worlds.GetRandomTown( world, false );
				else if (whatever <= 70)
					gagme = Worlds.GetRandomLocation( world, "land" );
				else
				{
					gagme = Worlds.GetRandomDungeonSpot( Worlds.GetMyDefaultMap( world ) );
					distance = 100;
				}
				
				bitch.MoveToWorld( gagme, Worlds.GetMyDefaultMap( world ) );
				((BaseCreature)bitch).Home = bitch.Location;
				((BaseCreature)bitch).RangeHome = 40;
				bitch.OnAfterSpawn();

				foreach ( Mobile mob in bitch.GetMobilesInRange( distance ) )
				{
					if (mob is BaseCreature && !mob.Blessed && !(((BaseCreature)mob).Controlled) && !( mob is BaseUndead || ((BaseCreature)mob).CanInfect || mob is wOphidianWarrior || mob is AcidSlug || mob is wOphidianMatriarch || mob is wOphidianMage || mob is wOphidianKnight || mob is wOphidianArchmage || mob is OphidianWarrior || mob is OphidianMatriarch || mob is OphidianMage || mob is OphidianKnight || mob is OphidianArchmage || mob is MonsterNestEntity || mob is AncientLich || mob is Bogle || mob is LichLord || mob is Shade || mob is Spectre || mob is Wraith || mob is BoneKnight || mob is ZenMorgan || mob is Ghoul || mob is Mummy || mob is SkeletalKnight || mob is Skeleton || mob is Zombie || mob is RevenantLion || mob is RottingCorpse || mob is SkeletalDragon || mob is AirElemental || mob is IceElemental || mob is ToxicElemental || mob is PoisonElemental || mob is FireElemental || mob is WaterElemental || mob is EarthElemental || mob is Efreet || mob is SnowElemental || mob is AgapiteElemental || mob is BronzeElemental || mob is CopperElemental || mob is DullCopperElemental || mob is GoldenElemental || mob is ShadowIronElemental || mob is ValoriteElemental || mob is VeriteElemental || mob is BloodElemental))
					{
						Region region = Region.Find( mob.Location, mob.Map );

						if ( !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion ) ) 
						{
							Zombiex zomb = new Zombiex();
							zomb.NewZombie(mob);

							mob.Delete();
							
						}
					}
				}
				
				CheckInfection();
			}

			if (AetherGlobe.invasionstage >= 1 && Utility.RandomBool() ) // now check stage of invasion and act accordingly
			{
				//Randomly choose a mob for reinforcement

				Type reinforce = null;
				double chances = Utility.RandomDouble();

				if (AetherGlobe.invasionstage == 1)
				{
					if (chances >= 0.95)
						reinforce = typeof ( OphidianMage);
					else if (chances >= 0.85)
						reinforce = typeof ( OphidianKnight );
					else 
						reinforce = typeof ( OphidianWarrior );
				}
				else if (AetherGlobe.invasionstage == 2)
				{
					if (chances <= 0.35)
						reinforce = typeof ( OphidianWarrior );
					else if (chances <= 0.45)
						reinforce = typeof ( OphidianKnight );
					else if (chances <= 0.50)
						reinforce = typeof ( OphidianMage );
					else if (chances <= 0.75)
						reinforce = typeof ( OphidianArchmage );
					else if (chances <= 0.90)
						reinforce = typeof ( OphidianMatriarch );
					else if (chances <= 1)
						reinforce = typeof ( DeepDweller ); 
				}
				else if (AetherGlobe.invasionstage == 3)
				{
					if (chances <= 0.10)
						reinforce = typeof ( OphidianWarrior);
					else if (chances <= 0.20)
						reinforce = typeof ( OphidianKnight);
					else if (chances <= 0.35)
						reinforce = typeof ( OphidianMage);
					else if (chances <= 0.60)
						reinforce = typeof ( OphidianArchmage);
					else if (chances <= 0.75)
						reinforce = typeof ( OphidianMatriarch);
					else if (chances <= 1)
						reinforce = typeof ( DeepDweller);
				}

				ArrayList army = new ArrayList();
				foreach ( Mobile mob in World.Mobiles.Values )
				{
					if ( mob is BaseCreature && mob.Map != null && Utility.RandomDouble() <= (AetherGlobe.intensity/2) )
					{
						if ( ((BaseCreature)mob).CanInfect && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp"))
						{
							army.Add(mob);							
						}
					}
				}
				for ( int i = 0; i < army.Count; i++ ) 
				{	
					Mobile mo = (Mobile)army[i];	
					
					Mobile huz = Activator.CreateInstance(reinforce) as Mobile;
					huz.MoveToWorld(mo.Location, mo.Map);
					((BaseCreature)huz).OnAfterSpawn();

					if (AetherGlobe.invasionstage == 3 && Utility.RandomDouble() <= AetherGlobe.intensity)
					{
						MonsterNest nest = new ZombieNest();

						if (Utility.RandomDouble() > 0.90)
							nest = new DeepDwellerNest();

						nest.MoveToWorld( mo.Location, mo.Map );
					}
				}
					
			}
			if (AetherGlobe.invasionstage == 3 )
				SendGeneral();
		}

		public static void SendGeneral()
		{
				Mobile bitch = null;
				// find general
				foreach ( Mobile bitches in World.Mobiles.Values )
				{
					if ( bitches is OphidianGeneral ) // move the general
					{
						bitch = bitches;
					}
				}

				if (bitch == null)
				{
					bitch = new OphidianGeneral();
					AetherGlobe.general = bitch.Name;
				}

				//Step 1, pick a world to infect
				int whatever = Utility.RandomMinMax(1, 100);
				String world = null;
				if (whatever <= 15) // 10%
					world = "the Isles of Dread";
				else if (whatever <= 40) // 20%
					world = "the Land of Sosaria";
				else if (whatever <= 70) // 20%
					world = "the Land of Lodoria";
				else if (whatever <= 85) // 15%
					world = "the Savaged Empire";
				else if (whatever <= 100) // 20%
					world = "the Serpent Island";


				Point3D gagme = Worlds.GetRandomTown( world, false );
				
				bitch.MoveToWorld( gagme, Worlds.GetMyDefaultMap( world ) );
				((BaseCreature)bitch).Home = bitch.Location;
				((BaseCreature)bitch).RangeHome = 10;

				bitch.OnAfterSpawn();

				foreach ( Mobile mob in bitch.GetMobilesInRange( 20 ) )
				{
					if (mob is BaseCreature && !mob.Blessed && !(((BaseCreature)mob).Controlled) && !( mob is BaseUndead || ((BaseCreature)mob).CanInfect || mob is wOphidianWarrior || mob is AcidSlug || mob is wOphidianMatriarch || mob is wOphidianMage || mob is wOphidianKnight || mob is wOphidianArchmage || mob is OphidianWarrior || mob is OphidianMatriarch || mob is OphidianMage || mob is OphidianKnight || mob is OphidianArchmage || mob is MonsterNestEntity || mob is AncientLich || mob is Bogle || mob is LichLord || mob is Shade || mob is Spectre || mob is Wraith || mob is BoneKnight || mob is ZenMorgan || mob is Ghoul || mob is Mummy || mob is SkeletalKnight || mob is Skeleton || mob is Zombie || mob is RevenantLion || mob is RottingCorpse || mob is SkeletalDragon || mob is AirElemental || mob is IceElemental || mob is ToxicElemental || mob is PoisonElemental || mob is FireElemental || mob is WaterElemental || mob is EarthElemental || mob is Efreet || mob is SnowElemental || mob is AgapiteElemental || mob is BronzeElemental || mob is CopperElemental || mob is DullCopperElemental || mob is GoldenElemental || mob is ShadowIronElemental || mob is ValoriteElemental || mob is VeriteElemental || mob is BloodElemental))
					{
						Region region = Region.Find( mob.Location, mob.Map );

						if ( !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion ) ) 
						{
							Zombiex zomb = new Zombiex();
							zomb.NewZombie(mob);

							mob.Delete();
							
						}
					}
				}
				
				CheckInfection();	
		}	

		public static int DiminishingReturns( int Value, int Max)
		{
			return DiminishingReturns( Value, Max, 10);
		}

		public static int DiminishingReturns( int Value, int Max, int Steps)
		{

			double final = 0; // the final value
			double step = Max / Steps;
			double compute = (double)Value;

			if (Value < step)
				final = Value;			
			else 
			{	
				while ( compute > 0 )
				{
					if (compute > step)
					{
						compute -= step;
						final += step;

						compute *=  1 - ( final / Max) ; // diminish the amount for the next pass
					}
					else
					{
						final += compute * (1 - ( final / Max));
						compute = 0;
					}
				}
			}

			Value = Convert.ToInt32( final );
			return Value;
		}

		public static void FetchFollowers( Mobile dude )
		{
			if ( dude is PlayerMobile )
			{
				PlayerMobile master = (PlayerMobile)dude;
				List<Mobile> pets = master.AllFollowers;

				if ( pets.Count > 0 )
				{
					dude.SendMessage( "Let's see... I found {0} pet{1}.", pets.Count, pets.Count != 1 ? "s" : "" );

					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile pet = (Mobile)pets[i];

						if ( pet is IMount )
						{
							if( ((IMount)pet).Rider != null )
							{
								Server.Mobiles.EtherealMount.EthyDismount( ((IMount)pet).Rider, true );
							}
							((IMount)pet).Rider = null; // make sure it's dismounted
						}
						pet.MoveToWorld( dude.Location, dude.Map );
					}
				}
				else
				{
					dude.SendMessage( "I can't find any pets for you...." );
				}
			}
			else if ( dude is Mobile && ((Mobile)dude).Player )
			{
				Mobile master = (Mobile)dude;
				ArrayList pets = new ArrayList();

				foreach ( Mobile m in World.Mobiles.Values )
				{
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;

						if ( (bc.Controlled && bc.ControlMaster == master) || (bc.Summoned && bc.SummonMaster == master) )
							pets.Add( bc );
					}
				}

				if ( pets.Count > 0 )
				{

					dude.SendMessage( "Let's see... I found {0} pet{1}.", pets.Count, pets.Count != 1 ? "s" : "" );

					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile pet = (Mobile)pets[i];

						if ( pet is IMount )
						{
							if( ((IMount)pet).Rider != null )
							{
								Server.Mobiles.EtherealMount.EthyDismount( ((IMount)pet).Rider, true );
							}
							((IMount)pet).Rider = null; // make sure it's dismounted
						}

						pet.MoveToWorld( dude.Location, dude.Map );
					}
				}
				else
				{
					dude.SendMessage( "Hmm... I couldn't find any pets for you." );
				}
			}
			else
			{
				dude.SendMessage( "That is not a player. Try again." );
			}
		}

		public static void PopulateStones( Item stone )
		{
			if (stone == null ||  stone.Map == null)
			{
				stone.Delete();
				return;
			}

			int count = 0;
			foreach ( Item i in stone.GetItemsInRange( 15 ) )
			{
				if (i is TombStone || i is Corpse || i is CorpseItem)
					count++;
			}

			double chances = Utility.RandomDouble();
			Type reinforce = null;

			if (count <= 6)
				{
					if (chances <= 0.33)
						reinforce = typeof ( Skeleton );
					else if (chances <= 0.66)
						reinforce = typeof ( SkeletonArcher );
					else 
						reinforce = typeof ( RestlessSoul );
				}
			else if (count <= 12)
				{
					if (chances <= 0.35)
						reinforce = typeof ( SkeletalMage );
					else if (chances <= 0.45)
						reinforce = typeof ( SkeletalKnight );
					else if (chances <= 0.50)
						reinforce = typeof ( Zombie );
					else if (chances <= 0.75)
						reinforce = typeof ( ZombieMage );
					else if (chances <= 0.90)
						reinforce = typeof ( DecayingZombie );
					else if (chances <= 1)
						reinforce = typeof ( SkeletalWarrior ); 
				}
			else if (count <= 24)
				{
					if (chances <= 0.50)
						reinforce = typeof ( Lich );
					else if (chances > 0.50)
						reinforce = typeof ( LichLord );
				}
			else if (count >= 25)
				{
					if (chances <= 0.95)
						reinforce = typeof ( DoomedLich );
					else if (chances > 0.95)
						reinforce = typeof ( LichKing );
				}
			
			Mobile huz = Activator.CreateInstance(reinforce) as Mobile;
					huz.MoveToWorld(stone.Location, stone.Map);
					((BaseCreature)huz).OnAfterSpawn();

		}

		public static void CleanupInternalObjects( Mobile who, bool conservative )
		{
		    int bogusCnt = 0;
		    ArrayList toRemove = new ArrayList();

		    foreach ( Item bogus in World.Items.Values )
		    {
			    if ( bogus.Map == Map.Internal && bogus.Parent == null )
			    {
				    bogusCnt++;

				    if (conservative)
				    {
					    bool goodToRemove = (bogus is ThrowingWeapon || bogus is MageEye || bogus is BasePoon);
					    if (!goodToRemove)
						    continue;
				    }

				    /* --- Uncomment to produce debug information regarding the nature of removed items ---
				       string Label = bogus.Name;
				       System.Globalization.TextInfo cultInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
				       if ( Label != null && Label != "" ){} else { Label = MorphingItem.AddSpacesToSentence( (bogus.GetType()).Name ); }
				       if ( Server.Misc.MaterialInfo.GetMaterialName( bogus ) != "" ){ Label = Server.Misc.MaterialInfo.GetMaterialName( bogus ) + " " + bogus.Name; }
				       Label = cultInfo.ToTitleCase(Label);

				       Console.WriteLine("Will remove: " + bogus.Name + " (" + Label + ") at Map.Internal with no parent at location " + bogus.Location + ", " + bogusCnt + " so far.");
				    */

				    toRemove.Add(bogus);
			    }
		    }

		    foreach ( Item a in toRemove )
			    a.Delete();

		    who.SendMessage ("Cleaned up " + bogusCnt + " items.");
		}
		
	}
}



