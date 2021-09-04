using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;

namespace Server.Items
{
	public class FeyEgg : Item
	{
		[Constructable]
		public FeyEgg() : base( 0x2D8E )
		{
			Weight = 4.0;
			Name = "Fey Egg";
            

            if ( Weight > 3.0 )
			{
				ItemID = Utility.RandomList( 0x2D8E);
				Weight = 3.0;

				HaveOrb = 0;
				HaveFairyDust = 0;
				HaveSeelieFetish = 0;
				HaveShimmeringLeaf = 0;
				HaveGold = 0;

				Hue = 0xBAB;

				NeedGold = 80000;
                

                AnimalTrainerLocation = Server.Items.FeyEgg.GetRandomVet();

				PieceRumor = Server.Items.CubeOnCorpse.GetRumor();
				PieceLocation = Server.Items.CubeOnCorpse.PickDungeon();
			}
		}
        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);
            list.Add(1070722, "Take to the Faerie Glade in Sosaria");
        }
        public override void OnDoubleClick( Mobile from )
		{
			if ( Weight > 2.0 && from.Map == Map.Trammel && from.X >= 3507 && from.Y >= 1122 && from.X <= 3532 && from.Y <= 1145 )
			{
				Weight = 1.0;
			}

			if ( Weight < 1.5 )
			{
				from.CloseGump( typeof( FeyEggGump ) );
				from.SendGump( new FeyEggGump( from, this ) );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
			int iAmount = 0;
			string sEnd = ".";

			if ( from != null && Weight < 1.5 )
			{
				if ( dropped is Gold && NeedGold > HaveGold )
				{
					int WhatIsDropped = dropped.Amount;
					int WhatIsNeeded = NeedGold - HaveGold;
					int WhatIsExtra = WhatIsDropped - WhatIsNeeded; if ( WhatIsExtra < 1 ){ WhatIsExtra = 0; }
					int WhatIsTaken = WhatIsDropped - WhatIsExtra;

					if ( WhatIsExtra > 0 ){ from.AddToBackpack( new Gold( WhatIsExtra ) ); }
					iAmount = WhatIsTaken;

					if ( iAmount > 1 ){ sEnd = "s."; }

					HaveGold = HaveGold + iAmount;
					from.SendMessage( "You added " + iAmount.ToString() + " Gold coin" + sEnd );
					dropped.Delete();
					return true;
				}
			}

			return false;
		}

		public FeyEgg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version

			writer.Write( HaveOrb );
			writer.Write( HaveFairyDust );
			writer.Write( HaveSeelieFetish );
			writer.Write( HaveShimmeringLeaf );
			writer.Write( HaveGold );
			writer.Write( NeedGold );
			writer.Write( AnimalTrainerLocation );
			writer.Write( PieceLocation );
			writer.Write( PieceRumor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			HaveOrb = reader.ReadInt();
			HaveFairyDust = reader.ReadInt();
			HaveSeelieFetish = reader.ReadInt();
			HaveShimmeringLeaf = reader.ReadInt();
			HaveGold = reader.ReadInt();
			NeedGold = reader.ReadInt();
			AnimalTrainerLocation = reader.ReadString();
			PieceLocation = reader.ReadString();
			PieceRumor = reader.ReadString();
		}

        public static bool ProcessFeyEgg(Mobile m, Mobile vet, Item dropped)
        {
            FeyEgg egg = (FeyEgg)dropped;

            if (Server.Misc.Worlds.GetRegionName(vet.Map, vet.Location) != egg.AnimalTrainerLocation) { return false; }

            int vetSkill = (int)(m.Skills[SkillName.Veterinary].Value);
            if (vetSkill > 100) { vetSkill = 100; }

            int GoldReturn = 0;
            if (vetSkill > 0) { GoldReturn = (int)(egg.NeedGold * (vetSkill * 0.005)); }

            int HaveIngredients = 0;

            if (egg.HaveFairyDust >= 0) { HaveIngredients++; }
            if (egg.HaveSeelieFetish >= 0) { HaveIngredients++; }
            if (egg.HaveShimmeringLeaf >= 0) { HaveIngredients++; }
            if (egg.HaveGold >= egg.NeedGold) { HaveIngredients++; }
            if (egg.HaveOrb >= 0) { HaveIngredients++; }

            if (HaveIngredients < 5) { return false; }

            if ((m.Followers + 3) > m.FollowersMax)
            {
                vet.Say("You have too many followers with you to hatch this egg.");
                return false;
            }

            if (GoldReturn > 0) { m.AddToBackpack(new Gold(GoldReturn)); vet.Say("Here is " + GoldReturn.ToString() + " Gold back for all of your help."); }

            Type FeyMonR = null;
            
            double Dice = Utility.RandomDouble();
            {
                if (Dice <= 0.30)
                    FeyMonR = typeof (MysticalFox);
                else if
                 (Dice <= 0.55)
                    FeyMonR = typeof (Unicorn);
                else if
                     (Dice <= 0.70)
                    FeyMonR = typeof (Kirin);
                else if
                    (Dice <= 0.80)
                    FeyMonR = typeof (Pegasus);
                else if
                    (Dice <= 0.90)
                    FeyMonR = typeof(Hiryu);
                else if
                 (Dice <= 1.0)
                    FeyMonR = typeof (CuSidhe);

            }
            BaseCreature FeyMon = Activator.CreateInstance(FeyMonR) as BaseCreature;
            FeyMon.Controlled = true;
            FeyMon.ControlMaster = m;
            FeyMon.IsBonded = true;
            FeyMon.MoveToWorld( m.Location, m.Map );
            FeyMon.ControlTarget = m;
            FeyMon.Tamable = true;
            FeyMon.MinTameSkill = FeyMon.MinTameSkill / 1.5;
            FeyMon.ControlOrder = OrderType.Follow;
            

            LoggingFunctions.LogGenericQuest( m, "has hatched a Fey Egg" );

			m.PrivateOverheadMessage(MessageType.Regular, 1153, false, "Your Fey Egg has hatched.", m.NetState);
			m.PlaySound( 0x041 );

			dropped.Delete();

			return true;
		}

		public class FeyEggGump : Gump
		{
			public FeyEggGump( Mobile from, FeyEgg egg ): base( 25, 25 )
			{
				string sText = "This egg contains the embryo of a mystical creature! Faeries once used a mystical ritual to bless Fey Eggs with ancient magics, because Fey eggs were part of the faerie realm they could take centuries to hatch naturally. Since you are not a fairy and do not know their mystical ways, there are some other ways you think this egg can hatched. Hearing many rumors at the tavern, there is a particular magic orb that can be powered by magical faerie items. The blessing given from these assembled artifact should be able to hatch the egg. A faerie creature would surely emerge, however, it would not be a worthy creature to help on your journey. Instead, you can try to find a very rare Shimmering Leaf. This should mature the hatchling into a full supernatural creature. Along with these things, you will also need some gold as you will need the help of a particular animal expert and they will require payment for their services. This animal expert is at the location shown on this screen. If you have any veterinary skill, they may refund some of the gold for the help you may provide in the birth. When hatched and fully grown, these creatures will become your bonded pet. You will have to feed it and stable it when required. You can also perform some animal lore on it without having any proficiency in the skill. This will help you with information about them, like what they want to eat.";

				string sRumor = egg.PieceRumor + " " + egg.PieceLocation;

				if ( egg.HaveOrb == 0 ){ sRumor = "The Orb of the Seelie Queen " + sRumor; }
				else if ( egg.HaveFairyDust == 0 ){ sRumor = "The Fairy Dust " + sRumor; }
				else if ( egg.HaveSeelieFetish == 0 ){ sRumor = "A Seelie Fetish " + sRumor; }
				else if ( egg.HaveShimmeringLeaf == 0 ){ sRumor = "The Shimmering Leaf " + sRumor; }
				else if ( egg.HaveGold < egg.NeedGold ){ sRumor = "You have obtained everything except the Gold."; }
				else { sRumor = "You have obtained everything you need."; }

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 30521);
				AddItem(574, 32, 20305); //last value controls graphic - top right flavor image

				AddHtml( 50, 38, 207, 20, @"<BODY><BASEFONT Color=#00FF06>Fey Egg</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(376, 36, 3823); //last value controls gold coins graphic add ,0xxxx to control hue
				AddHtml( 420, 38, 180, 20, @"<BODY><BASEFONT Color=#00FF06>" + egg.HaveGold.ToString() + "/" + egg.NeedGold.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 50, 70, 520, 60, @"<BODY><BASEFONT Color=#00FF06>" + sRumor + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(41, 237, 3000);
				AddHtml( 85, 242, 622, 20, @"<BODY><BASEFONT Color=#00FF06>Bring Gathered Materials to the Animal Expert in " + egg.AnimalTrainerLocation + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(85, 145, 8893);
				if ( egg.HaveOrb > 0 ){ AddItem(84, 156, 22334, 0x859); }

				AddItem(235, 145, 8893);
				if ( egg.HaveFairyDust > 0 ){ AddItem(236, 144, 11701, 0x863); }

				AddItem(385, 145, 8893);
				if ( egg.HaveSeelieFetish > 0 ){ AddItem(387, 143, 12682, 0x878); }

				AddItem(535, 145, 8893);
				if ( egg.HaveShimmeringLeaf > 0 ){ AddItem(546, 145, 12688, 0x866); }

				AddHtml( 50, 289, 665, 319, @"<BODY><BASEFONT Color=#00FF06>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public static string GetRandomVet()
		{
			int aCount = 0;
			Region reg = null;
			string sRegion = "";

			ArrayList targets = new ArrayList();
			foreach ( Mobile target in World.Mobiles.Values )
			if ( target is BaseVendor )
			{
				reg = Region.Find( target.Location, target.Map );
				string tWorld = Worlds.GetMyWorld( target.Map, target.Location, target.X, target.Y );

				if (	tWorld == "the Land of Sosaria" || 
						tWorld == "the Land of Lodoria" || 
						tWorld == "the Serpent Island" || 
						tWorld == "the Isles of Dread" || 
						tWorld == "the Savaged Empire" || 
						tWorld == "the Island of Umber Veil" || 
						tWorld == "the Bottle World of Kuldar" )
				{
					if ( ( target is AnimalTrainer || target is Veterinarian ) && reg.IsPartOf( typeof( VillageRegion ) ))
					{
						targets.Add( target ); aCount++;
					}
				}
			}

			aCount = Utility.RandomMinMax( 1, aCount );

			int xCount = 0;
			for ( int i = 0; i < targets.Count; ++i )
			{
				Mobile vet = ( Mobile )targets[ i ];
				xCount++;

				if ( xCount == aCount )
				{
					sRegion = Server.Misc.Worlds.GetRegionName( vet.Map, vet.Location );
				}
			}

			return sRegion;
		}

		public string AnimalTrainerLocation;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_AnimalTrainerLocation { get{ return AnimalTrainerLocation; } set{ AnimalTrainerLocation = value; } }

		public string PieceLocation;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_PieceLocation { get{ return PieceLocation; } set{ PieceLocation = value; } }

		public string PieceRumor;
		[CommandProperty( AccessLevel.GameMaster )]
		public string g_PieceRumor { get{ return PieceRumor; } set{ PieceRumor = value; } }

		// ----------------------------------------------------------------------------------------

		public int NeedGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_NeedGold { get{ return NeedGold; } set{ NeedGold = value; } }

		// ----------------------------------------------------------------------------------------

		public int HaveOrb;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveOrb { get{ return HaveOrb; } set{ HaveOrb = value; } }

		public int HaveGold;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveGold { get{ return HaveGold; } set{ HaveGold = value; } }

		public int HaveFairyDust;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveFairyDust { get{ return HaveFairyDust; } set{ HaveFairyDust = value; } }

		public int HaveSeelieFetish;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveSeelieFetish { get{ return HaveSeelieFetish; } set{ HaveSeelieFetish = value; } }

		public int HaveShimmeringLeaf;
		[CommandProperty( AccessLevel.GameMaster )]
		public int g_HaveShimmeringLeaf { get{ return HaveShimmeringLeaf; } set{ HaveShimmeringLeaf = value; } }
	}
}