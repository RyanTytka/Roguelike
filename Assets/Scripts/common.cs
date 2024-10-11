public enum EnemyTypeEnum{
    //cultists
    CULTIST = 0,            
    TENTACLE,
    SPAWN_OF_THE_OLD_ONE,
    //undead
    SKELETON,
    SKELETON_KNIGHT,
    SKELETON_LORD,
    ZOMBIE,
    LICH,
    SOUL_COLLECTOR,
    //demons
    CHAIN_DEMON,
    VOID_DEMON,
    FLYING_EYE,
    IMP,
    //nature
    CARNIVOROUS_PLANT,
    GIANT_SPIDER,
    HERMIT,
    MONSTER_PLANT,
    MUSHROOM,
    WATER_SPIRIT,
    //constructs
    CLAY_GOLEM,
    STONE_GOLEM,
    ICE_GOLEM,
    //creatures
    HOUND,
    SNAKE,
    WENDIGO,
    WEREWOLF,
    VAMPIRE,
    //humanoid
    GOBLIN,
    ORC,
    WIZARD,
    //ooze
    OOZE,
    TOXIC_OOZE,
    //spirits
    GHOST_HOUND,
    GHOST_WIZARD,
    WRAITH,
    //bosses
    SKELETON_KING,      
    BONE_PILE,
    LICH_KING,
    BEHOLDER,
    ICE_DRAGON,
    ORC_KING
}

public enum EventType{
    TREASURE, //Get a random item and gold
    ENEMY, //Fight enemies
    HEAL, //Heal the party
    STAT_BUFF //Permanently buff a character's stat
}

public enum EquipmentTypeEnum{
    ARMOR = 0,
    WEAPON,
    ARTIFACT
}
public enum EquipmentRarityEnum
{
    COMMON,
    RARE,
    LEGENDARY
}

public enum EquipmentRarity{
    COMMON = 0,
    RARE,
    EPIC,
    LEGENDARY,
}

public enum EquipmentName{
    //Warrior
        //Weapons
    HEAVEY_MACE = 0, //Common. +5 Atk -1 Spd
    LONGSWORD,  //Common, +2 Atk +1 Spd
    PIKE,  //Common, +2 Atk, +2 Def
    MAGE_AXE,  //Common, +2 Atk, +2 Int
    PLATESMASHER,  //Uncommon, Melee Attacks Ignore 50% of the Target's Armor
    RUNED_BLADE,  //Uncommon, Gain 30% of your Int as Atk
    BLOODTHIRSTER_AXE, //Lgendary, +4 Atk. 15% Lifesteal on Basic Abilities. 
        //Armor
    PLATE_MAIL,  //Common, +5 Def
    SPINED_ARMOR,  //Common, +3 Def, +2 Thorns
    DRAGON_MAIL,  //Uncommon, +5 Res
    RUNED_ARMOR,  //Uncommon, Gain 40% Int as Def
    MAGE_MAIL,  //Uncommon, +3 Def, +3 Res
    DEVIL_ARMOR,  //Rare, +5 Def. Immune to Debuffs
        //Artifacts
    RING_OF_ENDURANCE,  //Common, +5 HP
    MAGE_RING,  //Common, +3 Int
    BULWARK_BELT,  //Common, +4 Def
    RING_OF_SHIELDING,  //Common, +4 Res
    HEALING_SALVE,  //Rare, Heal 2 HP each round
    AMULET_OF_VITALITY,  //
    SOUL_LANTERN,  //Legendary, Heal 10% When you kill an Enemy


        //MISC:
        //warrior
    RAGE_PENDANT, //Rare. at the end of each round, gain 1 atk
    HONING_STONE, //Rare. Your Basic Abilities deal 15% more damage
    BERSERKERS_ARMOR, //Common. Gain 25% armor for each enemy alive
    LIFEDRINKER, //Legendary. Heal for 10% of an enemy' HP hwn you kill it
    INFERNAL_ARMOR, //Epic. Gain 1 Atk- whenever you take damage
        //mage
    MAGIC_WAND, //Common. +3 Magic
    CHANNELERS_RING, //Rare. +5 Mana. Gain Magic equal to your Mana
    STAFF_OF_ELEMENTAL_DOMINATION, //Legendary. +5 Magic. At the start of combat, summon an elemental
    CLERICS_ROBES, //Rare. +3 Resilience. Your Holy Abilities have 1 less Cooldown
}

public enum StatusTypeEnum {  STRENGTH_UP = 0, STRENGTH_DOWN, MAGIC_UP, MAGIC_DOWN, MANAREGEN_UP, MANAREGEN_DOWN, //stats
                        ARMOR_UP, ARMOR_DOWN, RES_UP, RES_DOWN, SPEED_UP, SPEED_DOWN,
                        POISONED, BLEEDING, DODGE, REFLECT, THRONS, BURNING, EXHAUSTED, RESTRAINED, DOOM
}

public enum PlayerItemsEnum { }

public enum DamageTypesEnum {
    PHYSICAL=1,
    MAGICAL,
    DIRECT
}

public enum PlayerClass
{
    WARRIOR,
    ROGUE,
    MAGE,
    NEUTRAL
}