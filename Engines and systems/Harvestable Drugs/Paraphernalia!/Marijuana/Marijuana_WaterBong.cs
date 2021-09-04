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
    [FlipableAttribute(0xE28, 0xF00)]
    public class PipeWeed_WaterBong : DrugSystem_Effect
    {
        [Constructable]
        public PipeWeed_WaterBong(): base(0xE28)
        {
            Name = "a water bong";
            this.Weight = 0.4;
            this.Hue = 1289;
        }

        public PipeWeed_WaterBong(Serial serial): base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.Stam > from.StamMax / 2)
            {
            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(PipeWeed_Leaves), 1) )
 
            {              
                if (from.Body.IsHuman && !from.Mounted)
                {
                    from.Animate(34, 5, 1, true, false, 0);

                }
                from.PlaySound(Utility.Random(0x20, 2));
                from.SendMessage("You Pack A Bowl And Spark It Up!");
                from.Meditating = true;
                from.SendMessage("You Begin To Feel The Darkness Throughout Your Body!");
                from.PlaySound(from.Female ? 798 : 1070);
                from.Say("*hiccup!*");
                Highness = 120;
                new DrugSystem_StonedTimer(from, Highness).Start();
            }

            else
            {
                from.SendMessage("Your Out Of PipeWeed Leaves Bro!");
                return;
            }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
