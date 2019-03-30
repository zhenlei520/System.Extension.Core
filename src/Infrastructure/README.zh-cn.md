<a href="https://github.com/zhenlei520/System.Extension.Core/blob/master/README.zh-cn.md">回到目录</a>

## 基础类库

#### 1、生肖帮助类
        AnimalCommon.GetAnimalFromBirthday(1988);//龙

        AnimalCommon.GetAnimalEnumFromBirthday(1988);//AnimalEnum.Dragon 

#### 2、文件类型与文件编码

        Base64Common.GetBaseEncoding("image/jpeg");//data:image/jpeg;base64

#### 3、校验方法
        string name="";
        name.IsNullOrEmptyTip("姓名不能为空");//等同于throw new BusinessException("姓名不能为空");

        object user=new User();
        user.IsNullOrEmptyTip("用户不存在");

        支持字符串、object、object数组判断空或者null

#### 4、拷贝方法
        CloneableClass clone=new CloneableClass();
        clone.Clone();//浅拷贝，更改地址引用，一变都变
        clone.ShallowClone<User>();//浅拷贝，更改地址引用，一变都变

        clone.DeepClone(user);//深拷贝，原对象与目标对象值相同，但并非同一地址

#### 5、星座帮助类
        var date="1993-04-18";
        ConstellationCommon.GetConstellationFromBirthday(DateTime.Parse(date));//白羊座
        ConstellationCommon.GetConstellationEnumFromBirthday(date);//ConstellationEnum.Aries

#### 6、距离帮助类
        double lat1, double lng1, double lat2, double lng2;
        DistanceCommon.GetDistance(lat1,lng1,lat2,lng2);//得到两个坐标的间距，单位：m

#### 7、邮箱帮助类
        EMailCommon.SendEmail("目的邮件地址","发送邮件的标题","发送邮件的内容","发送者的邮箱地址","网络上的代理服务器","发送邮件的密码","发送成功提示语，成功的话应该与pPwd一致");//new {Status=true,Msg="成功"}

#### 8、枚举帮助类
        public enum Gender{
            [Description("男")]Body=0,
            [Description("女")]Girl=1
        }
        Gender.Body.GetDescription();//男
        1.IsExist(typeof(GenderEnum))；//true

        EnumCommon.ToDictionary<GenderEnum>();//得到枚举字典（key与value与字典值一致）

        EnumCommon.ToDescriptionDictionary<GenderEnum>();//得到枚举字典（key对应枚举的值，value对应枚举的注释）