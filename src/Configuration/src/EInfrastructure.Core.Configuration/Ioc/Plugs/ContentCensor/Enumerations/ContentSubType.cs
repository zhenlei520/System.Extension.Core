using EInfrastructure.Core.Configuration.Enumerations.SeedWork;

namespace EInfrastructure.Core.Configuration.Ioc.Plugs.ContentCensor.Enumerations
{
    /// <summary>
    /// 内容细分
    /// </summary>
    public class ContentSubType : Enumeration
    {
        /// <summary>
        /// 色情
        /// </summary>
        public static ContentSubType Porn = new ContentSubType(1, "色情", ContentType.Porn.Id);

        /// <summary>
        /// 女下体
        /// </summary>
        public static ContentSubType Vaginal = new ContentSubType(2, "女下体", ContentType.Porn.Id);

        /// <summary>
        /// 女胸
        /// </summary>
        public static ContentSubType GirlBreast = new ContentSubType(3, "女胸", ContentType.Porn.Id);

        /// <summary>
        /// 男下体
        /// </summary>
        public static ContentSubType Penis = new ContentSubType(4, "男下体", ContentType.Porn.Id);

        /// <summary>
        /// 性行为
        /// </summary>
        public static ContentSubType Sexual = new ContentSubType(5, "性行为", ContentType.Porn.Id);

        /// <summary>
        /// 臀部
        /// </summary>
        public static ContentSubType Buttocks = new ContentSubType(6, "臀部", ContentType.Porn.Id);

        /// <summary>
        /// 口交
        /// </summary>
        public static ContentSubType OralSex = new ContentSubType(7, "口交", ContentType.Porn.Id);

        /// <summary>
        /// 卡通色情
        /// </summary>
        public static ContentSubType CartoonPorn = new ContentSubType(8, "卡通色情", ContentType.Porn.Id);

        /// <summary>
        /// 儿童色情
        /// </summary>
        public static ContentSubType ChildPorn = new ContentSubType(9, "儿童色情", ContentType.Porn.Id);

        /// <summary>
        /// 性感低俗
        /// </summary>
        public static ContentSubType SexyAndVulgar = new ContentSubType(10, "性感低俗", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 亲吻
        /// </summary>
        public static ContentSubType Kiss = new ContentSubType(11, "亲吻", ContentType.Other.Id);

        /// <summary>
        /// 腿部特写
        /// </summary>
        public static ContentSubType LegsCloseup = new ContentSubType(12, "腿部特写", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 赤膊
        /// </summary>
        public static ContentSubType Shirtless = new ContentSubType(13, "赤膊", ContentType.Other.Id);

        /// <summary>
        /// 胸部
        /// </summary>
        public static ContentSubType Breast = new ContentSubType(14, "胸部", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 广告
        /// </summary>
        public static ContentSubType Advertising = new ContentSubType(15, "广告", ContentType.Advertising.Id);

        /// <summary>
        /// 广告带文字
        /// </summary>
        public static ContentSubType AdvertisingAndText = new ContentSubType(16, "广告带文字", ContentType.Advertising.Id);

        /// <summary>
        /// 二维码
        /// </summary>
        public static ContentSubType QrCode = new ContentSubType(17, "二维码", ContentType.QrCode.Id);

        /// <summary>
        /// 暴恐
        /// </summary>
        /// <returns></returns>
        public static ContentSubType Violence = new ContentSubType(18, "暴恐", ContentType.Violence.Id);

        /// <summary>
        /// 暴恐图集
        /// </summary>
        public static ContentSubType TerrorAtlas = new ContentSubType(19, "暴恐图集", ContentType.Violence.Id);

        /// <summary>
        /// 暴恐旗帜
        /// </summary>
        /// <returns></returns>
        public static ContentSubType TerrorFlag = new ContentSubType(20, "暴恐旗帜", ContentType.Violence.Id);

        /// <summary>
        /// 暴恐人物
        /// </summary>
        public static ContentSubType TerrorCharacter = new ContentSubType(21, "暴恐人物", ContentType.Violence.Id);

        /// <summary>
        /// 暴恐标识
        /// </summary>
        public static ContentSubType TerrorLogo = new ContentSubType(22, "暴恐标识", ContentType.Violence.Id);

        /// <summary>
        /// 暴恐场景
        /// </summary>
        public static ContentSubType TerrorScene = new ContentSubType(23, "暴恐场景", ContentType.Violence.Id);

        /// <summary>
        /// 违禁
        /// </summary>
        public static ContentSubType Violate = new ContentSubType(24, "违禁", ContentType.Violate.Id);

        /// <summary>
        /// 违禁图集
        /// </summary>
        public static ContentSubType ProhibitedGallery = new ContentSubType(25, "违禁图集", ContentType.Violate.Id);

        /// <summary>
        /// 违禁品
        /// </summary>
        public static ContentSubType Contraband = new ContentSubType(26, "违禁品", ContentType.Violate.Id);

        /// <summary>
        /// 特殊标识
        /// </summary>
        public static ContentSubType SpecialLogo = new ContentSubType(27, "特殊标识", ContentType.Violate.Id);

        /// <summary>
        /// 血腥模式
        /// </summary>
        public static ContentSubType BloodyModel = new ContentSubType(28, "血腥模型", ContentType.Violence.Id);

        /// <summary>
        /// 公职服饰
        /// </summary>
        public static ContentSubType PublicServiceApparel = new ContentSubType(29, "公职服饰", ContentType.Violate.Id);

        /// <summary>
        /// 不文明
        /// </summary>
        public static ContentSubType Uncivilized = new ContentSubType(30, "不文明", ContentType.Violate.Id);

        /// <summary>
        /// 违禁人物
        /// </summary>
        public static ContentSubType ProhibitedCharacter = new ContentSubType(31, "违禁人物", ContentType.Violate.Id);

        /// <summary>
        /// 违禁场景
        /// </summary>
        public static ContentSubType ProhibitedScene = new ContentSubType(32, "违禁场景", ContentType.Violate.Id);

        /// <summary>
        /// 火焰
        /// </summary>
        public static ContentSubType Flame = new ContentSubType(33, "火焰", ContentType.Violence.Id);

        /// <summary>
        /// 骷髅
        /// </summary>
        public static ContentSubType Skull = new ContentSubType(34, "骷髅", ContentType.Violence.Id);

        /// <summary>
        /// 货币
        /// </summary>
        public static ContentSubType Currency = new ContentSubType(35, "货币", ContentType.Other.Id);

        /// <summary>
        /// 毒品
        /// </summary>
        public static ContentSubType Drug = new ContentSubType(36, "毒品", ContentType.Violence.Id);

        /// <summary>
        /// 涉政
        /// </summary>
        public static ContentSubType Political = new ContentSubType(37, "涉政", ContentType.Politics.Id);

        /// <summary>
        /// 涉政图集
        /// </summary>
        public static ContentSubType PoliticalAtlas = new ContentSubType(38, "涉政图集", ContentType.Politics.Id);

        /// <summary>
        /// 中国地图
        /// </summary>
        public static ContentSubType MapOfChina = new ContentSubType(39, "中国地图", ContentType.Other.Id);

        /// <summary>
        /// 涉政人物
        /// </summary>
        public static ContentSubType PoliticalFigures = new ContentSubType(40, "涉政人物", ContentType.Politics.Id);

        /// <summary>
        /// 涉政旗帜
        /// </summary>
        public static ContentSubType PoliticalBanner = new ContentSubType(41, "涉政旗帜", ContentType.Politics.Id);

        /// <summary>
        /// 涉政标识
        /// </summary>
        public static ContentSubType PoliticalLogo = new ContentSubType(42, "涉政标识", ContentType.Politics.Id);

        /// <summary>
        /// 涉政场景
        /// </summary>
        public static ContentSubType PoliticalScenes = new ContentSubType(43, "涉政场景", ContentType.Politics.Id);

        /// <summary>
        /// 其他
        /// </summary>
        public static ContentSubType Other = new ContentSubType(44, "其他", ContentType.Other.Id);

        /// <summary>
        /// 自定义用户黑名单
        /// </summary>
        public static ContentSubType CustomerUserList = new ContentSubType(45, "自定义用户黑名单", ContentType.Customer.Id);

        /// <summary>
        /// 自定义IP黑名单
        /// </summary>
        public static ContentSubType CustomerIpList = new ContentSubType(46, "自定义IP黑名单", ContentType.Customer.Id);

        /// <summary>
        /// 自焚
        /// </summary>
        public static ContentSubType SelfBurning = new ContentSubType(47, "自焚", ContentType.Violence.Id);

        /// <summary>
        /// 斩首
        /// </summary>
        public static ContentSubType BeHeaded = new ContentSubType(48, "斩首", ContentType.Violence.Id);

        /// <summary>
        /// 人群聚集
        /// </summary>
        public static ContentSubType MarchCrowed = new ContentSubType(49, "人群聚集", ContentType.Violence.Id);

        /// <summary>
        /// 警民冲突
        /// </summary>
        public static ContentSubType FightPolice = new ContentSubType(50, "警民冲突", ContentType.Violence.Id);

        /// <summary>
        /// 民众肢体接触
        /// </summary>
        public static ContentSubType FightPerson = new ContentSubType(51, "民众肢体接触", ContentType.Violence.Id);

        /// <summary>
        /// 枪
        /// </summary>
        public static ContentSubType Guns = new ContentSubType(52, "枪", ContentType.Violence.Id);

        /// <summary>
        /// 刀
        /// </summary>
        public static ContentSubType Knives = new ContentSubType(53, "刀", ContentType.Violence.Id);

        /// <summary>
        /// 动漫血腥
        /// </summary>
        public static ContentSubType AnimeBloodiness = new ContentSubType(54, "动漫血腥", ContentType.Violence.Id);

        /// <summary>
        /// 二次元刀
        /// </summary>
        public static ContentSubType AnimeKnives = new ContentSubType(55, "二次元刀", ContentType.Violence.Id);

        /// <summary>
        /// 二次元枪
        /// </summary>
        public static ContentSubType AnimeGuns = new ContentSubType(56, "二次元枪", ContentType.Violence.Id);

        /// <summary>
        /// 涉政
        /// </summary>
        public static ContentSubType DomesticStatesman = new ContentSubType(57, "国内政要", ContentType.Politics.Id);

        /// <summary>
        /// 国外政要
        /// </summary>
        public static ContentSubType ForeignStatesman = new ContentSubType(58, "国外政要", ContentType.Politics.Id);

        /// <summary>
        /// 革命英烈
        /// </summary>
        /// <returns></returns>
        public static ContentSubType ChineseMartyr = new ContentSubType(59, "革命英烈", ContentType.Politics.Id);

        /// <summary>
        /// 落马官员（政府）
        /// </summary>
        /// <returns></returns>
        public static ContentSubType AffairsOfficialGov = new ContentSubType(60, "落马官员（政府）", ContentType.Politics.Id);

        /// <summary>
        /// 落马官员（企事业）
        /// </summary>
        /// <returns></returns>
        public static ContentSubType AffairsOfficialEnt =
            new ContentSubType(61, "落马官员（企事业）", ContentType.Politics.Id);

        /// <summary>
        /// 反华分子
        /// </summary>
        /// <returns></returns>
        public static ContentSubType AntiChinaPeople = new ContentSubType(62, "反华分子", ContentType.Politics.Id);

        /// <summary>
        /// 恐怖分子
        /// </summary>
        /// <returns></returns>
        public static ContentSubType Terrorist = new ContentSubType(63, "恐怖分子", ContentType.Politics.Id);

        /// <summary>
        /// 劣迹艺人
        /// </summary>
        /// <returns></returns>
        public static ContentSubType AffairsCelebrity = new ContentSubType(64, "劣迹艺人", ContentType.Politics.Id);

        /// <summary>
        /// 一维码
        /// </summary>
        /// <returns></returns>
        public static ContentSubType BarCode = new ContentSubType(65, "一维码", ContentType.QrCode.Id);

        /// <summary>
        /// 车祸
        /// </summary>
        public static ContentSubType CarAccident = new ContentSubType(66, "车祸", ContentType.Violence.Id);

        /// <summary>
        /// SM
        /// </summary>
        public static ContentSubType Sm = new ContentSubType(67, "SM", ContentType.Porn.Id);

        /// <summary>
        /// 艺术品色情
        /// </summary>
        public static ContentSubType ArtworkPorn = new ContentSubType(68, "艺术品色情", ContentType.Porn.Id);

        /// <summary>
        /// 性玩具
        /// </summary>
        public static ContentSubType SexToys = new ContentSubType(69, "性玩具", ContentType.Porn.Id);

        /// <summary>
        /// 特殊
        /// </summary>
        public static ContentSubType Special = new ContentSubType(70, "特殊", ContentType.Porn.Id);

        /// <summary>
        /// 亲密行为
        /// </summary>
        public static ContentSubType Bosom = new ContentSubType(71, "亲密行为", ContentType.Porn.Id);

        /// <summary>
        /// 卡通亲密行为
        /// </summary>
        public static ContentSubType CartoonIntimacy = new ContentSubType(72, "卡通亲密行为", ContentType.Porn.Id);

        /// <summary>
        /// 卡通女性性感
        /// </summary>
        public static ContentSubType CartoonFemaleSexy = new ContentSubType(73, "卡通女性性感", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 女性性感
        /// </summary>
        public static ContentSubType FemaleSexy = new ContentSubType(74, "女性性感", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 男性性感
        /// </summary>
        public static ContentSubType MaleSexy = new ContentSubType(75, "男性性感", ContentType.SexyVulgar.Id);

        /// <summary>
        /// 自然男性裸露
        /// </summary>
        public static ContentSubType NaturalMaleNudity = new ContentSubType(76, "自然男性裸露", ContentType.Porn.Id);

        /// <summary>
        /// 警察部队
        /// </summary>
        public static ContentSubType PoliceForce = new ContentSubType(77, "警察部队", ContentType.Violence.Id);

        /// <summary>
        /// 尸体
        /// </summary>
        public static ContentSubType Corpse = new ContentSubType(78, "尸体", ContentType.Violence.Id);

        /// <summary>
        /// 爆炸火灾
        /// </summary>
        public static ContentSubType ExplosionFire = new ContentSubType(79, "爆炸火灾", ContentType.Violence.Id);

        /// <summary>
        /// 杀人
        /// </summary>
        public static ContentSubType Kill = new ContentSubType(80, "杀人", ContentType.Violence.Id);

        /// <summary>
        /// 暴乱
        /// </summary>
        public static ContentSubType Riot = new ContentSubType(81, "暴乱", ContentType.Violence.Id);

        /// <summary>
        /// 军事武器
        /// </summary>
        public static ContentSubType MilitaryWeapon = new ContentSubType(82, "军事武器", ContentType.Violence.Id);

        /// <summary>
        /// 血腥动物或动物尸体
        /// </summary>
        public static ContentSubType BloodyAnimal = new ContentSubType(83, "血腥动物或动物尸体", ContentType.Violence.Id);

        /// <summary>
        /// 恶心图
        /// </summary>
        public static ContentSubType Nausea = new ContentSubType(84, "恶心图", ContentType.Nausea.Id);

        /// <summary>
        /// 水印
        /// </summary>
        public static ContentSubType Watermark = new ContentSubType(85, "水印", ContentType.QrCode.Id);

        /// <summary>
        /// 公众人物
        /// </summary>
        public static ContentSubType PublicFigure = new ContentSubType(86, "公众人物", ContentType.Customer.Id);

        /// <summary>
        /// 自定义用户白名单
        /// </summary>
        public static ContentSubType CustomerWhiteUserList =
            new ContentSubType(87, "自定义用户黑名单", ContentType.Customer.Id);

        /// <summary>
        /// 自定义IP白名单
        /// </summary>
        public static ContentSubType CustomerWhiteIpList = new ContentSubType(88, "自定义IP黑名单", ContentType.Customer.Id);

        /// <summary>
        /// 低质灌水
        /// </summary>
        public static ContentSubType LowQualityIrrigation =
            new ContentSubType(89, "低质灌水", ContentType.TextAntiCheat.Id);

        /// <summary>
        /// 恶意推广
        /// </summary>
        public static ContentSubType MaliciousPromotion = new ContentSubType(90, "恶意推广", ContentType.TextAntiCheat.Id);

        /// <summary>
        /// 低俗辱骂
        /// </summary>
        public static ContentSubType VulgarAbuse = new ContentSubType(91, "低俗辱骂", ContentType.TextAntiCheat.Id);

        /// <summary>
        /// 聚众
        /// </summary>
        public static ContentSubType Crowd = new ContentSubType(92, "聚众", ContentType.Violence.Id);

        /// <summary>
        /// 游行
        /// </summary>
        public static ContentSubType Parade = new ContentSubType(93, "游行", ContentType.Violence.Id);

        /// <summary>
        /// 地标
        /// </summary>
        public static ContentSubType Location = new ContentSubType(94, "地标", ContentType.Violence.Id);

        /// <summary>
        /// 无意义图片
        /// </summary>
        public static ContentSubType MeaninglessPictures = new ContentSubType(95, "无意义图片", ContentType.BadScene.Id);

        /// <summary>
        /// 画中画
        /// </summary>
        public static ContentSubType PictureInPicture = new ContentSubType(96, "画中画", ContentType.BadScene.Id);

        /// <summary>
        /// 吸烟
        /// </summary>
        public static ContentSubType Smoking = new ContentSubType(97, "吸烟", ContentType.BadScene.Id);

        /// <summary>
        /// 车内直播
        /// </summary>
        public static ContentSubType Drivelive = new ContentSubType(98, "车内直播", ContentType.BadScene.Id);


        /// <summary>
        ///
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="name">名称</param>
        /// <param name="parentId">父内容类型id</param>
        public ContentSubType(int id, string name, int parentId) : base(id, name)
        {
            this.ParentId = parentId;
        }

        /// <summary>
        /// 内容类型id
        /// </summary>
        public int ParentId { get; private set; }
    }
}
