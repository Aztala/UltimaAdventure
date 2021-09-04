// ZombieX by SpookyRobert....
//Edted and revised for RUNUO 2.2 by FinalTwist

//don't forget to make distro edits per .txt

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server;



namespace Server.Mobiles
{
    [CorpseName("an infected corpse")]
    public class Zombiex : BaseUndead
    {
        public override bool CanRegenHits { get { return true; } }
        public override bool IsScaredOfScaryThings { get { return false; } }
        public override bool IsScaryToPets { get { return true; } }
        public override bool ReacquireOnMovement{ get { return true; } }

        [Constructable]
        public Zombiex() : base(AIType.AI_Melee, FightMode.Closest, 8, 1, 0.2, 0.4)
        {
            if (Utility.RandomDouble() > 0.85)
			    AIFullSpeedActive = true;
            else 
                AIFullSpeedActive = false;
			AIFullSpeedPassive = false; // Force full speed
            Name = "A Contagious Zombie";
            Body = 728;
            BaseSoundID = 471;

            SetStr(300);
            SetDex(25);
            SetInt(10);

            SetHits(500);
            SetStam(150);
            SetMana(0);

            SetDamage(10, 15);

            SetDamageType(ResistanceType.Physical, 25);
            SetDamageType(ResistanceType.Cold, 25);
            SetDamageType(ResistanceType.Poison, 50);

            SetResistance(ResistanceType.Physical, 50, 80);
            SetResistance(ResistanceType.Fire, 20, 30);
            SetResistance(ResistanceType.Cold, 99, 100);
            SetResistance(ResistanceType.Poison, 99, 100);
            SetResistance(ResistanceType.Energy, 40, 60);

            SetSkill(SkillName.Poisoning, 120.0);
            SetSkill(SkillName.MagicResist, 100.0);
            SetSkill(SkillName.Tactics, 100.0);
            SetSkill(SkillName.Wrestling, 90.1, 100.0);

            PassiveSpeed = .4;
            ActiveSpeed = .8;

            Fame = 6000;
            Karma = -6000;

            VirtualArmor = 40;
			
	    CanInfect = true;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }


        public Item CloneItem(Item item)
        {
            if ((item.Layer != Layer.Bank) && (item.Layer != Layer.Backpack))
            {
                Item newItem = new Item(item.ItemID);
                newItem.Hue = item.Hue;
                newItem.Layer = item.Layer;

                return newItem;
            }

            return null;
        }

        public virtual void NewZombie(Mobile from)
        {
            //What Should the Zombie virus not affect
            if (from is BaseUndead || (from is BaseCreature && ((BaseCreature)from).CanInfect) || (from is BaseCreature && ((BaseCreature)from).Summoned))
            {
                return;
            }
            else
            {
				if (from is PlayerMobile)
					LoggingFunctions.LogZombie( from );
				
                double dhits, dstr;

                Female = from.Female;
                Body = from.Body;
                Hue = 768;
                Name = from.Name;
                Title = "*Infected*";
				AIFullSpeedActive = false;
				AIFullSpeedPassive = false; // Force full speed

                HairItemID = from.HairItemID;
                HairHue = from.HairHue;
                FacialHairItemID = from.FacialHairItemID;
                FacialHairHue = from.FacialHairHue;

                SetResistance(ResistanceType.Physical, 40, 70);
                SetResistance(ResistanceType.Fire, 20, 40);
                SetResistance(ResistanceType.Cold, 99, 100);
                SetResistance(ResistanceType.Poison, 99, 100);
                SetResistance(ResistanceType.Energy, 30, 60);

                dhits = from.HitsMax * 1.2;
                dstr = from.Str * 0.80;

                for (int i = 0; i < from.Skills.Length; ++i)
                {
                    Skills[i].Base = from.Skills[i].Base * 0.80;
                    Skills[i].Cap = from.Skills[i].Cap;
                }

                HitsMaxSeed = from.HitsMax + (int)dhits;
                Hits = from.HitsMax + (int)dhits;
                Str = from.Str + (int)dstr;

                //region moveitems
                if (from.Items != null && from.Backpack != null)
                {
                    List<Item> mobitems = new List<Item>(from.Items);
		
					foreach (Item item in mobitems)
						{
							if (item.Layer == Layer.Backpack || item.Layer == Layer.Bank || item.Insured || item.LootType == LootType.Blessed || item.LootType == LootType.Newbied) continue;
							EquipItem(item);
						}

                    
                    mobitems = new List<Item>(from.Backpack.Items);
                    if (Backpack == null)
                    {
                        Backpack backpack = new Backpack();
                        backpack.Movable = false;
                        AddItem(backpack);
                    }
                    foreach (Item item in mobitems)
                    {
                        if (item.Insured || item.LootType == LootType.Blessed || item.LootType == LootType.Newbied) continue;
                        Backpack.AddItem(item);
                    }

                    if (from.Mounted)
                    {
                        if (from.Mount is EtherealMount)
                        {
                            EtherealMount pet = from.Mount as EtherealMount;
                            pet.Internalize();
                            pet.Rider = null;
                        }
                        else if (from.Mount is BaseMount)
                        {
                            BaseMount petm = from.Mount as BaseMount;
                            petm.Rider = null;
                            BaseCreature pet = petm as BaseCreature;
                            pet.Internalize();
                            pet.ControlTarget = null;
                            pet.SetControlMaster(null);
                            pet.SummonMaster = null;
                            pet.MoveToWorld(Location, Map);
                        }
                    }
                }
                //endregion

                MoveToWorld(from.Location, from.Map);
                if (from is BaseCreature && !((BaseCreature)from).IsBonded)
                    from.Delete();

                this.OnAfterSpawn();
            }
            return;
        }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            if (Utility.RandomDouble() > 0.90)
                c.DropItem(new ZombieKillerPotion());
            if (Utility.RandomDouble() >= 0.97)
                c.DropItem(new SlayerDeed());

            switch (Utility.Random(700))
            {
                case 0: c.DropItem(new CombativeNorseHelm()); break;
                case 1: c.DropItem(new CombativePlateArms()); break;
                case 2: c.DropItem(new CombativePlateChest()); break;
                case 3: c.DropItem(new CombativePlateGloves()); break;
                case 4: c.DropItem(new CombativePlateGorget()); break;
                case 5: c.DropItem(new CombativePlateLegs()); break;
                case 6: c.DropItem(new CombativePlateSkirt()); break;
                case 7: c.DropItem(new FemaleCombativePlateChest()); break;
            }	
			if (Utility.RandomMinMax(1, 100) == 3)
				c.DropItem(new InfectionPotion());		
			


        }

        public Zombiex(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
			
            if (Utility.RandomDouble() > 0.75)
			    AIFullSpeedActive = true;
            else 
                AIFullSpeedActive = false;
			AIFullSpeedPassive = false; // Force full speed

            //Remove timer here to stop decay but don't forget the one Constructable section!
            //m_Timer = new RotTimer(this);
            //m_Timer.Start();
        }
    }
}
