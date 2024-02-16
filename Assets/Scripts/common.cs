public enum EnemyTypeEnum{
    SKELETON = 0,
    GOBLIN,
    OGRE,
    SKELETON_KING,
    UNDYING_SOLDIER,
    TENTACLE,
    CULTIST
}

public enum EquipmentTypeEnum{
    ARMOR = 0,
    WEAPON,
    ARTIFACT
}

public enum StatusTypeEnum {  STRENGTH_UP = 0, STRENGTH_DOWN, MAGIC_UP, MAGIC_DOWN, MANAREGEN_UP, MANAREGEN_DOWN, //stats
                        ARMOR_UP, ARMOR_DOWN, RES_UP, RES_DOWN, SPEED_UP, SPEED_DOWN,
                        POISONED, BLEEDING, STUNNED, CONFUSED, BURNING, //misc effects
                        MARKED, SHACKLED, DOOM } //enemy specific

public enum StatusIconsEnum { }

public enum PlayerItemsEnum { }

public enum DamageTypesEnum {
    PHYSICAL=1,
    MAGICAL,
    DIRECT
}