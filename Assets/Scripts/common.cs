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

public enum EquipmentTypeEnum{
    ARMOR = 0,
    WEAPON,
    ARTIFACT
}

public enum StatusTypeEnum {  STRENGTH_UP = 0, STRENGTH_DOWN, MAGIC_UP, MAGIC_DOWN, MANAREGEN_UP, MANAREGEN_DOWN, //stats
                        ARMOR_UP, ARMOR_DOWN, RES_UP, RES_DOWN, SPEED_UP, SPEED_DOWN,
                        POISONED, BLEEDING, STUNNED, EXHAUSTED, BURNING, FROZEN,//(Next attack deals extra dmg), //misc effects
                        MARKED, SHACKLED, DOOM } //enemy specific

public enum PlayerItemsEnum { }

public enum DamageTypesEnum {
    PHYSICAL=1,
    MAGICAL,
    DIRECT
}