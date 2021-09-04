/////////////////////////////////////
//**Generated by Ryan**//
/////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class NoxTunic : LeatherChest
	{
	 	public override int ArtifactRarity{ get{ return 10; } }
	 	public override int InitMinHits{ get{ return 255; } }
	 	public override int InitMaxHits{ get{ return 255; } }

	 	[Constructable]
	 	public NoxTunic()
	 	{
	 	 	Name = "Tunic of the Swamp Queen";
	 	 	Hue = 1272;
	 	 	ArmorAttributes.MageArmor = 1;
	 	 	ArmorAttributes.SelfRepair = 8;
            PhysicalBonus = 10;
            PoisonBonus = 10;
            EnergyBonus = 10;
            ColdBonus = 10;
            FireBonus = 10;
			Attributes.BonusDex = 5;
			Attributes.BonusHits = 5;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 1;
			Attributes.DefendChance = 10;
			Attributes.Luck = 125;
			Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 10;
			Attributes.RegenHits = 2;
			Attributes.RegenMana = 2;
			Attributes.SpellDamage = 10;
			Attributes.WeaponDamage = 15;
			Attributes.WeaponSpeed = 10;
	 	}

	 	public NoxTunic(Serial serial) : base( serial )
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
