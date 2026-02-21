using System;
using System.Collections.Generic;
using System.Text;

namespace PokedexApp.Models
{
    public class Item
    {
        public string Name { get; set; }
        public string FlavorText { get; set; }
        public ItemCategory Category { get; set; }
        public int Cost { get; set; }
    }

    public enum ItemCategory
    {
        Stat_Boosts,
        Effort_Drop,
        Medicine,
        Other,
        In_A_Pinch,
        Picky_Healing,
        Type_Protection,
        Baking_Only,
        Collectible,
        Evolution,
        Spelunking,
        Held_Items,
        Choice,
        Effort_Training,
        Bad_Held_Items,
        Training,
        Plates,
        Species_Specific,
        Type_Enhancement,
        Event_Items,
        Gameplay,
        Plot_Advancement,
        Unused,
        Loot,
        All_Mail,
        Vitamins,
        Healing,
        PP_Recovery,
        Revival,
        Status_Cures,

    }
}
