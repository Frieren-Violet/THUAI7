﻿namespace Preparation.Utility
{
    /// <summary>
    /// 装甲类型
    /// </summary>
    public enum ArmorType
    {
        Null = 0,
        Armor1 = 1,
        Armor2 = 2,
        Armor3 = 3,
    }
    /// <summary>
    /// 子弹类型
    /// </summary>
    public enum BulletType
    {
        Null = 0,
        Laser = 1,
        Plasma = 2,
        Shell = 3,
        Missile = 4,
        Arc = 5,
    }
    /// <summary>
    /// 建筑类型
    /// </summary>
    public enum ConstructionType
    {
        Null = 0,
        Factory = 1,
        Community = 2,
        Fort = 3,
    }
    /// <summary>
    /// 建造器类型
    /// </summary>
    public enum ConstructorType
    {
        Null = 0,
        Constructor1 = 1,
        Constructor2 = 2,
        Constructor3 = 3,
    }
    /// <summary>
    /// 游戏对象类型
    /// </summary>
    public enum GameObjType
    {
        Null = 0,
        Ship = 1,
        Bullet = 2,
        BombedBullet = 3,

        Ruin = 4,
        Shadow = 5,
        Asteroid = 6,
        Resource = 7,
        Construction = 8,
        Wormhole = 9,
        Home = 10,
        OutOfBoundBlock = 11,
    }
    /// <summary>
    /// 武器类型
    /// </summary>
    public enum GunType
    {
        Null = 0,
        LaserGun = 1,
        PlasmaGun = 2,
        ShellGun = 3,
        MissileGun = 4,
        ArcGun = 5,
    }
    /// <summary>
    /// PlaceType
    /// </summary>
    public enum PlaceType
    {
        Null = 0,
        BirthPoint = 1,
        Home = 2,
        Ruin = 3,
        Shadow = 4,
        Asteroid = 5,
        Resource = 6,
        Construction = 7,
        Wormhole = 8,
    }
    /// <summary>
    /// 采集器类型
    /// </summary>
    public enum ProducerType
    {
        Null = 0,
        Producer1 = 1,
        Producer2 = 2,
        Producer3 = 3,
    }
    /// <summary>
    /// 运动状态类型
    /// </summary>
    public enum RunningStateType
    {
        Null = 0,
        Waiting = 1,
        RunningActively = 2,
        RunningSleepily = 3,
        RunningForcibly = 4,
    }
    /// <summary>
    /// 形状类型
    /// </summary>
    public enum ShapeType
    {
        Null = 0,
        Circle = 1,
        Square = 2
    }
    /// <summary>
    /// 护盾类型
    /// </summary>
    public enum ShieldType
    {
        Null = 0,
        Shield1 = 1,
        Shield2 = 2,
        Shield3 = 3,
    }
    /// <summary>
    /// 舰船状态类型
    /// </summary>
    public enum ShipStateType
    {
        Null = 0,
        Producing = 1,
        Constructing = 2,
        Recovering = 3,
        Recycling = 4,
        Attacking = 5,
        Swinging = 6,
        Stunned = 7,
        Moving = 8,
    }
    /// <summary>
    /// 舰船类型
    /// </summary>
    public enum ShipType
    {
        Null = 0,
        CivilShip = 1,
        WarShip = 2,
        FlagShip = 3,
    }
    /// <summary>
    /// 武器类型
    /// </summary>
    public enum WeaponType
    {
        Null = 0,
        LaserGun = 1,
        PlasmaGun = 2,
        ShellGun = 3,
        MissileGun = 4,
        ArcGun = 5,
    }
}