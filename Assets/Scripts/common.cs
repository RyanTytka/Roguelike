public enum EnemyTypeEnum{
    GOBLIN=0,
    GOBLIN_SHAMAN,
    GOBLIN_APPRENTICE,
    SHIELD_GOBLIN,
    OGRE,
    SKELETON_KING,
    UNDYING_SOLDIER,
    BONE_PILE,
    CULTIST,
    TENTACLE,
    SPAWN_OF_THE_OLD_ONE,
    SKELETON,
    ZOMBIE,
    GHOST,
    WITCH,
    BONE_HOUND,
    VOID_DEMON,
    FLAME_DEMON,
    SLAVER,
    IMP,
    ASSASSIN,
    BERSERKER,
    WITCH_DOCTOR,
    WIZARD,
    DRUID,
    KNIGHT
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