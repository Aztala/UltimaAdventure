#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Items.Crops;

namespace Server.Mobiles
{
    public class SBDrugSystem : SBInfo
    {
        private List<GenericBuyInfo> m_BuyInfo = new InternalBuyInfo();
        private IShopSellInfo m_SellInfo = new InternalSellInfo();

        public SBDrugSystem()
        {
        }

       public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
        public override List<GenericBuyInfo> BuyInfo { get { return m_BuyInfo; } }

        public class InternalBuyInfo : List<GenericBuyInfo>
        {
            public InternalBuyInfo()
            {
                #region Items To Buy! - Vendor Inventory

                #region Directions On Adding New Items
                /*
                Add(new GenericBuyInfo("string name", typeof (type), int price, int amount, int itemID, int hue));    
                */
                #endregion Directions On Adding New Items

                Add(new GenericBuyInfo("PipeWeed Kit", typeof(PipeWeed_StarterKit), 2250, 100, 0x0E76, 0));

                Add(new GenericBuyInfo("PipeWeed Leaves", typeof(PipeWeed_Leaves), 1000, 100, 0xF88, 167));
                Add(new GenericBuyInfo("PipeWeed Seeds", typeof(PipeWeed_Seed), 250, 100, 0x1AA2, 167));
                Add(new GenericBuyInfo("Rolling Paper", typeof(PipeWeed_RollingPaper), 250, 100, 0x12A5, 1153)); //0x12AB
                Add(new GenericBuyInfo("A Joint", typeof(PipeWeed_Joint), 2500, 100, 0x1420, 1153));
                Add(new GenericBuyInfo("A WaterBong", typeof(PipeWeed_WaterBong), 5000, 100, 0xE28, 1289));
                
                Add(new GenericBuyInfo(typeof(MortarPestle), 8, 10, 0xE9B, 0));
                Add(new GenericBuyInfo(typeof(HeatingStand), 2, 100, 0x1849, 0));

                #endregion Items To Buy! - Vendor Inventory
            }
        }

        public class InternalSellInfo : GenericSellInfo
        {
            public InternalSellInfo()
            {
                #region Items To Sell - Vendor Inventory

                #region Directions On Adding New Items
                /*
				Add( typeof( type ), price );                    
                */
                #endregion Directions On Adding New Items

                Add( typeof( PipeWeed_Leaves), 750);
                Add( typeof( PipeWeed_Seed), 175);
				Add( typeof( PipeWeed_Joint), 1250);
                
                Add( typeof( MortarPestle ), 9 );
                Add( typeof( HeatingStand ), 3 ); 

                #endregion Items To Sell - Vendor Inventory
            }
        }
    }
}
