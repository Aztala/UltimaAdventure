#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Threading;
using System.Collections;
using Server;
using Server.Network;
using Server.Scripts;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items.Crops
{
    public class PipeWeed_Crop : DrugSystem_Engine
    {
        private const int max = 1;
        private int fullGraphic;
        private int pickedGraphic;
        private DateTime lastpicked;
        private Mobile m_sower;
        private int m_yield;
        public Timer regrowTimer;
        private DateTime m_lastvisit;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime LastSowerVisit { get { return m_lastvisit; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Growing { get { return regrowTimer.Running; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Sower { get { return m_sower; } set { m_sower = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Yield { get { return m_yield; } set { m_yield = value; } }

        public int Capacity { get { return max; } }
        public int FullGraphic { get { return fullGraphic; } set { fullGraphic = value; } }
        public int PickGraphic { get { return pickedGraphic; } set { pickedGraphic = value; } }
        public DateTime LastPick { get { return lastpicked; } set { lastpicked = value; } }

        [Constructable]
        public PipeWeed_Crop() : this(null) { }

        [Constructable]
        public PipeWeed_Crop(Mobile sower): base(Utility.RandomList(0x4792, 0x4794))
        {
            Name = "PipeWeed Plant";
            Weight = 0.2;
            Hue = 167;
            Movable = false;
            Stackable = false;
            m_sower = sower;
            m_lastvisit = DateTime.Now;
            init(this, false);
        }

        public static void init(PipeWeed_Crop plant, bool full)
        {
            plant.PickGraphic = (Utility.RandomList(0x4792, 0x4794));
            plant.FullGraphic = (Utility.RandomList(0x4793, 0x4795));
            plant.LastPick = DateTime.Now;
            plant.regrowTimer = new PipeWeed_CropTimer(plant);

            if (full)
            {
                plant.Yield = plant.Capacity; ((Item)plant).ItemID = plant.FullGraphic;
            }
            else
            {
                plant.Yield = 0; ((Item)plant).ItemID = plant.PickGraphic; plant.regrowTimer.Start();
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (m_sower == null || m_sower.Deleted) m_sower = from;
            if (from != m_sower) { from.SendMessage("You Do Not Own This Plant!"); return; }

            if (from.Mounted && !DrugSystem_Helper.CanWorkMounted) { from.SendMessage("You Cannot Harvest This Crop While Mounted."); return; }
            if (DateTime.Now > lastpicked.AddSeconds(3))
            {
                lastpicked = DateTime.Now;
                int alchemyValue = (int)from.Skills[SkillName.Alchemy].Value / 20;
                if (alchemyValue == 0) { from.SendMessage("You Have No Idea How To Harvest This Crop."); return; }
                if (from.InRange(this.GetWorldLocation(), 1))
                {
                    if (m_yield < 1) { from.SendMessage("There Is Nothing Here To Harvest."); }
                    else
                    {
                        from.Direction = from.GetDirectionTo(this);
                        from.Animate(from.Mounted ? 29 : 32, 5, 1, true, false, 0);
                        m_lastvisit = DateTime.Now;
                        if (alchemyValue > m_yield) alchemyValue = m_yield + 1;
                        int pick = Utility.RandomMinMax(alchemyValue - 4, alchemyValue);
                        if (pick < 0) pick = 0;
                        if (pick == 0) { from.SendMessage("You Fail To Harvest Any Crops!"); return; }
                        m_yield -= pick;
                        from.SendMessage("You Harvest {0} Crop{1}!", pick, (pick == 1 ? "" : "s"));
                        if (m_yield < 1) ((Item)this).ItemID = pickedGraphic;

                        PipeWeed_Crop crop = new PipeWeed_Crop(pick);
                        from.AddToBackpack(new PipeWeed_Leaves(from));
                
                        if (!regrowTimer.Running) { regrowTimer.Start(); }
                    }
                }
                else { from.SendMessage("You Are Too Far Away From A Plant To Harvest Anything!"); }
            }
        }

        private class PipeWeed_CropTimer : Timer
        {
            private PipeWeed_Crop i_plant;
            public PipeWeed_CropTimer(PipeWeed_Crop plant)
                : base(TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(15))
            {
                Priority = TimerPriority.OneSecond;
                i_plant = plant;
            }
            protected override void OnTick()
            {
                if (Utility.RandomBool())
                {
                    if ((i_plant != null) && (!i_plant.Deleted))
                    {
                        int current = i_plant.Yield;
                        if (++current >= i_plant.Capacity)
                        {
                            current = i_plant.Capacity;
                            ((Item)i_plant).ItemID = i_plant.FullGraphic;
                            Stop();
                        }
                        else if (current <= 0) current = 1;
                        i_plant.Yield = current;
                    }
                    else Stop();
                }
            }
        }

        public PipeWeed_Crop(Serial serial): base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
            writer.Write(m_lastvisit);
            writer.Write(m_sower);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_lastvisit = reader.ReadDateTime();
            m_sower = reader.ReadMobile();
            init(this, true);
        }
    }
}