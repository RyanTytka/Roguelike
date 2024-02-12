public enum ENEMY_TYPE{
    GOBLIN=0,
    SKELETON,
    OGRE,
    CULTIST,
    TENTACLE,
    SKELETON_KING,
    UNDYING_SOLDIER
}

public enum EQUIPMENT_TYPE{
    ARMOR = 0,
    WEAPON,
    ARTIFACT
}

public enum StatusType {  STRENGTH_UP = 0, STRENGTH_DOWN, MAGIC_UP, MAGIC_DOWN, MANAREGEN_UP, MANAREGEN_DOWN, //stats
                        ARMOR_UP, ARMOR_DOWN, RES_UP, RES_DOWN, SPEED_UP, SPEED_DOWN,
                        POISONED, BLEEDING, STUNNED, CONFUSED, BURNING, //misc effects
                        MARKED, SHACKLED, DOOM } //enemy specific

public enum StatusIcons { }

public enum PlayerItems { }

public enum DamageTypes {
    PHYSICAL=1,
    MAGICAL,
    DIRECT
}