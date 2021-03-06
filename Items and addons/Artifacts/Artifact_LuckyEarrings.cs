using System;
using Server;


namespace Server.Items
{
    public class LuckyEarrings : GoldEarrings, ITokunoDyable
    {
        [Constructable]
        public LuckyEarrings()
        {
            Name = "Lucky Earrings";
            Hue = 1174;            
            Attributes.Luck = 350;
            Attributes.RegenMana = 3;
			Attributes.RegenStam = 3;
			Attributes.RegenHits = 3;
            Attributes.AttackChance = 10;
            Attributes.DefendChance = 10;
            Attributes.WeaponSpeed = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


        public LuckyEarrings (Serial serial) : base( serial )
        {
        }


        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int) 0 );
        }


        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }
    } 
}
