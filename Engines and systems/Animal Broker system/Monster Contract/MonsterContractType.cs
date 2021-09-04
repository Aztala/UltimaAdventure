using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class MonsterContractType
	{
		public static MonsterContractType[] Get = new MonsterContractType[]
		{
			new MonsterContractType (typeof(Bird), 				"Forest Birds", 			9),
			new MonsterContractType (typeof(DesertBird), 			"Desert Birds", 			15),
			new MonsterContractType (typeof(SwampBird), 			"Swamp Birds", 				15),
			new MonsterContractType (typeof(Chicken), 			"Chickens", 				4),
			new MonsterContractType (typeof(Eagle), 			"Eagles", 				17),
			new MonsterContractType (typeof(Hawk), 				"Hawks", 				17),
			new MonsterContractType (typeof(SabretoothBearRiding), 		"SabreTooth Bears",			70),
			new MonsterContractType (typeof(GrizzlyBearRiding), 		"Grizzly Bears",			34),
			new MonsterContractType (typeof(ElderPolarBearRiding), 		"Elder Polar Bears",			77),			
			new MonsterContractType (typeof(ElderBrownBearRiding), 		"Elder Brown Bears",			77),			
			new MonsterContractType (typeof(ElderBlackBearRiding), 		"Elder Black Bears",			77),			
			new MonsterContractType (typeof(CaveBearRiding), 		"Cave Bears",				69),			
			new MonsterContractType (typeof(SabreclawCub), 			"Sabreclaw Cubs",			25),	
			new MonsterContractType (typeof(PolarBear), 			"Polar Bears",				36),
			new MonsterContractType (typeof(Panda), 			"Panda",				23),
			new MonsterContractType (typeof(GreatBear), 			"Greater Bears",			60),
			new MonsterContractType (typeof(DireBear), 			"Dire Bears",				72),
			new MonsterContractType (typeof(BrownBear), 			"Brown Bears",				20),
			new MonsterContractType (typeof(TimberWolf), 			"Timber Wolves",			20),
			new MonsterContractType (typeof(WhiteWolf), 			"White Wolves",				21),
			new MonsterContractType (typeof(Jackal), 			"Jackals",				20),			
			new MonsterContractType (typeof(Fox), 				"Foxes",				13),	
			new MonsterContractType (typeof(DireWolf), 			"Dire Wolves",				36),			
			new MonsterContractType (typeof(BlackWolf), 			"Black Wolves",				22),			
			new MonsterContractType (typeof(BullradonRiding), 		"Ridding Bulls",			45),
			new MonsterContractType (typeof(Bobcat), 			"Bobcats",				30),	
			new MonsterContractType (typeof(Cougar), 			"Cougars",				28),
			new MonsterContractType (typeof(CragCat), 			"Crag Cats",				90),	
			new MonsterContractType (typeof(HellCat), 			"Hell Cats",				27),
			new MonsterContractType (typeof(Jaguar), 			"Jaguars",				29),
			new MonsterContractType (typeof(LionRiding), 			"Lions",				40),
			new MonsterContractType (typeof(Panther), 			"Panthers",				20),
			new MonsterContractType (typeof(PredatorHellCat), 		"Hell Lions",				35),	
			new MonsterContractType (typeof(SabretoothCub), 		"SabreTooth Cub",			19),
			new MonsterContractType (typeof(SabretoothTiger), 		"Sabretooth Tigers",			90),
			new MonsterContractType (typeof(SnowLion), 			"Snow Lions",				40),	
			new MonsterContractType (typeof(SnowLeopard), 			"Snow Leopards",			19),
			new MonsterContractType (typeof(WhitePanther), 			"White Panthers",			90),	
			new MonsterContractType (typeof(WhiteCat), 			"White Cats",				8),	
			new MonsterContractType (typeof(WhiteTiger), 			"White Tiger",				33),	
			new MonsterContractType (typeof(DireBoar), 			"Dire Boars",				15),	
			new MonsterContractType (typeof(Elephant), 			"Elephants",				50),	
			new MonsterContractType (typeof(Mammoth), 			"Mammoths",				60),				
			new MonsterContractType (typeof(Mastadon), 			"Mastadons",				70),	
			new MonsterContractType (typeof(Sheep), 			"Sheep",				5),	
			new MonsterContractType (typeof(GreatHart), 			"Great Harts",				18),	
			new MonsterContractType (typeof(Antelope), 			"Antelopes",				11),	
			new MonsterContractType (typeof(Gorilla), 			"Gorillas",				35),	
			new MonsterContractType (typeof(YoungRoc), 			"Young Rocs",				100),	
			new MonsterContractType (typeof(SnowOstard), 			"Snow Ostards",				25),
			new MonsterContractType (typeof(SilverSteed), 			"Silver Steeds",			15),	
			new MonsterContractType (typeof(Roc), 				"Rocs",					109),	
			new MonsterContractType (typeof(Horse), 			"Horses",				18),	
			new MonsterContractType (typeof(GiantRaven), 			"Giant Ravens",				66),	
			new MonsterContractType (typeof(GiantHawk), 			"Giant Hawks",				45),	
			new MonsterContractType (typeof(FrenziedOstard)	, 		"Frenzied Ostards",			80),	
			new MonsterContractType (typeof(ForestOstard), 			"Forest Ostards",			25),	
			new MonsterContractType (typeof(DesertOstard),	 		"Desert Ostards",			22),	
			new MonsterContractType (typeof(Weasel), 			"Weasels",				15),	
			new MonsterContractType (typeof(JackRabbit), 			"Jack Rabbits",				15),	
			new MonsterContractType (typeof(Ferret), 			"Ferrets",				20),	
			new MonsterContractType (typeof(Tarantula), 			"Tarantulas",				78),	
			new MonsterContractType (typeof(LargeSpider), 			"Large Spiders",			45),	
			new MonsterContractType (typeof(GiantSpider), 			"GiantSpiders",				30),
			new MonsterContractType (typeof(FrostSpider), 			"Frost Spiders",			66),	
			new MonsterContractType (typeof(Scorpion), 			"Scorpions",				30),
			new MonsterContractType (typeof(Mantis), 			"Mantises",				40),
			new MonsterContractType (typeof(DeadlyScorpion), 		"Deadly Scorpions",			50),
			new MonsterContractType (typeof(WaterBeetleRiding), 		"Water Beetle",				40),
			new MonsterContractType (typeof(TigerBeetleRiding), 		"Tiger Beetle",				45),
			new MonsterContractType (typeof(RuneBeetle), 			"Rune Beetles",				100),
			new MonsterContractType (typeof(PoisonBeetleRiding), 		"Poison Beetles",			55),
			new MonsterContractType (typeof(GlowBeetleRiding), 		"Glow Beetles",				55),			
			new MonsterContractType (typeof(FireBeetle), 			"Fire Beetles",				60),
			new MonsterContractType (typeof(DeathwatchBeetle), 		"DeathWatch Beetles",			30),
			new MonsterContractType (typeof(Anhkheg), 			"Anhkhegs",				30),
			new MonsterContractType (typeof(Beetle), 			"Beetles",				80),
			new MonsterContractType (typeof(AlienSpider), 			"Alien Spiders",			65),	
			new MonsterContractType (typeof(AlienSmall), 			"Alien Hatchling",			65),
			new MonsterContractType (typeof(Alien), 			"Aliens",				90),
			new MonsterContractType (typeof(Imp), 				"Imps",					45),
			new MonsterContractType (typeof(FireMephit), 			"Fire Mephits",				45),
			new MonsterContractType (typeof(GemDragon), 			"Dragyns",				90),
			new MonsterContractType (typeof(BabyDragon), 			"Baby Dragons",				50),
			new MonsterContractType (typeof(FireWyrmling), 			"fire wyrmlings",			50),
			new MonsterContractType (typeof(Wyvern), 			"Wyverns",				70),			
			new MonsterContractType (typeof(AncientWyvern), 		"Ancient Wyverns",			110),
			new MonsterContractType (typeof(Wyrms), 			"Wyrms",				110),
			new MonsterContractType (typeof(SeaDragon), 			"Sea Dragons",				105),
			new MonsterContractType (typeof(ReanimatedDragon), 		"Reanimated Dragons",			115),
			new MonsterContractType (typeof(PrimevalVolcanicDragon),	"Cinder Dragons",			117),
			new MonsterContractType (typeof(PrimevalStygianDragon), 	"Stygian Dragons",			119),
			new MonsterContractType (typeof(PrimevalSilverDragon), 		"primeval silver dragons",		115),
			new MonsterContractType (typeof(PrimevalSeaDragon), 		"primeval sea dragon",			119),
			new MonsterContractType (typeof(PrimevalRunicDragon), 		"Rune Dragons",				119),
			new MonsterContractType (typeof(PrimevalRoyalDragon), 		"Royal Dragons",			119),
			new MonsterContractType (typeof(PrimevalRedDragon), 		"Primeval Red Dragons",			119),
			new MonsterContractType (typeof(PrimevalDragon), 		"Primeval Dragons",			115),
			new MonsterContractType (typeof(Wyvra), 			"Wyvras",				45),
			new MonsterContractType (typeof(Hydra), 			"Hydras",				80),
			new MonsterContractType (typeof(SwampDrakeRiding), 		"Swamp Drakes",				80),
			new MonsterContractType (typeof(Drake), 			"Drakes",				70),
			new MonsterContractType (typeof(AncientDrake), 			"Ancient Drakes",			95),
			new MonsterContractType (typeof(Dragons), 			"Dragons",				98),
			new MonsterContractType (typeof(DragonTurtle), 			"Dragon Turtles",			110),
			new MonsterContractType (typeof(Cow), 				"Cows",					5)
  //Attention pas de virgule à la dernière ligne
		};
	
		public static int Random()
		{
			int test = Utility.RandomMinMax(0, 135);
			int result = 0;
			for(int i=0;i<50;i++)
			{
				result = Utility.Random(MonsterContractType.Get.Length);
				if( test > ( MonsterContractType.Get[result].Rarety ) )
					break;
			}
			
			return result;
		}
		
		private Type m_Type;
		public Type Type
		{
			get { return m_Type;}
			set { m_Type = value;}
		}
		
		private string m_Name;
		public string Name
		{
			get { return m_Name;}
			set { m_Name = value;}
		}
		
		private int m_Rarety;
		public int Rarety
		{
			get { return m_Rarety;}
			set { m_Rarety = value;}
		}
		
		public MonsterContractType(Type type, string name, int rarety)
		{
			Type = type;
			Name = name;
			Rarety = rarety;
		}
	}
}
