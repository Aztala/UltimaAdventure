using System; 
using Server; 
using Server.Gumps; 
using Server.Network; 

namespace Server.Items 
{ 

public class WeirdDecoDeed : Item 
{ 

[Constructable] 
public WeirdDecoDeed() : this( null ) 
{ 
} 

[Constructable] 
public WeirdDecoDeed ( string name ) : base ( 0x14F0 ) 
{ 
Name = "Weird Deco Deed"; 
LootType = LootType.Blessed; 
Hue = 212; 
}

    public WeirdDecoDeed(Serial serial)
        : base(serial) 
{ 
} 

public override void OnDoubleClick( Mobile from ) 
{ 
if ( !IsChildOf( from.Backpack ) ) 
{ 
from.SendLocalizedMessage( 1042001 ); 
} 
else 
{
    from.SendGump(new WeirdDecoGump(from, this)); 
} 
} 

public override void Serialize ( GenericWriter writer) 
{ 
base.Serialize ( writer ); 

writer.Write ( (int) 0); 
} 

public override void Deserialize( GenericReader reader ) 
{ 
base.Deserialize ( reader ); 

int version = reader.ReadInt(); 
} 
} 
}
