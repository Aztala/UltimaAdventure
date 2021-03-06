
using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class CavemanLoincloth : FurSarong
    {
        [Constructable]
        public CavemanLoincloth()
        {
            
            Hue = 351;
            Name = "Sabertooth Skin Loincloth of the Caveman";

            Attributes.AttackChance = 10;
            Attributes.BonusStam = 10;
            Attributes.BonusHits = 10;
            Attributes.BonusStr = 10;
            Attributes.BonusDex = 10;
            Attributes.DefendChance = 10;
            Attributes.BonusMana = -10;
	    Attributes.BonusInt = -10;
	    Attributes.WeaponSpeed = 30;
	    Attributes.WeaponDamage = 30;

            LootType = LootType.Regular;
        }

        public CavemanLoincloth( Serial serial ) : base( serial )
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