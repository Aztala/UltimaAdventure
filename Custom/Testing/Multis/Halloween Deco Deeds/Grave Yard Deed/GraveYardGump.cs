using System; 
using System.Net; 
using Server; 
using Server.Accounting; 
using Server.Gumps; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;

namespace Server.Gumps
{
    public class GraveYardGump : Gump
    {
        private Mobile m_Mobile;
        private Item m_Deed;


        public GraveYardGump(Mobile from, Item deed)
            : base(30, 20)
        {
            m_Mobile = from;
            m_Deed = deed;

            Closable = true;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            AddPage(1);

            AddBackground(0, 0, 300, 400, 3000);
            AddBackground(8, 8, 284, 384, 5054);

            AddLabel(40, 12, 37, "Grave Yard Gump");

            Account a = from.Account as Account;


            AddLabel(52, 40, 0, "Grave Yard South");
            AddButton(12, 40, 4005, 4007, 1, GumpButtonType.Reply, 1);
            AddLabel(52, 60, 0, "Grave Yard West");
            AddButton(12, 60, 4005, 4007, 2, GumpButtonType.Reply, 2);
            AddImage(50, 90, 3861);
            



        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Close Gump 
                    {
                        from.CloseGump(typeof(GraveYardGump));
                        break;
                    }
                case 1: //GraveYardSouthAddonDeed
                    {
                        Item item = new GraveYardSouthAddonDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(GraveYardGump));
                        m_Deed.Delete();
                        break;
                    }
                case 2: // GraveYardWestAddonDeed
                    {
                        Item item = new GraveYardWestAddonDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(GraveYardGump));
                        m_Deed.Delete();
                        break;
                  
                    }
            }
        }
    }
}
