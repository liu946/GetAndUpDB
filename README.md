# 数据库监控同步

标签： C# 数据库同步 线程监控

---

## Documentation

##### 1. Config

调试阶段，配置程序内定义。

抽取主表中感兴趣的字段，这些字段会以Dictionary真实字段传递给事件处理。其他字段保存在事件数据Dicitionary的["OTHERS"]字段中。数据库请勿占用此字段。

DB_TOOL - 23
``` C#
string insterestingfieldstr = "ID,AUTOID,NAME,AGE,TELE,EMAIL,TIME,DOCTOR_NAME,REGISTER_ID";
```

配置服务器，含db的为需要同步的数据库信息。


DBListener - 25
```
config["server"] = "192.168.1.99";
config["dbserver"] ="L-WIN10";
config["dbname"] ="histest";
config["dbusername"] ="root";
config["dbpassword"] ="123456";
config["postserver"] = "http://192.168.1.99/dbAPI/index.php/Home/index/";
```

##### 2. Usage

使用事件绑定，当检测到数据库更新时，会执行绑定到itemchanged的方法。

```
 private void Form1_Load(object sender, EventArgs e)
{
    // 绑定事件方法
    db = new DBListener();
    db.itemchanged += itemChangedHandlerExamplePrint;
    db.itemchanged += db.sendToServer; // 发送数据
}
```
dataitem
```
{ID:2,
AUTOID:3,
NAME:sang,
...
OTHERS:"{othercol1:1,othercol2:2,}",
childitem:
    [
    {ID:2,NAME:123,...},
    {ID:2,NAME:456,...},
    ...
    ]
}
```

定义与使用
```
private void itemChangedHandlerExamplePrint(object sender, Dictionary<string, object> dataitem)
{
    Console.WriteLine("Handle the itemchanged event\n");
    foreach (var dic in dataitem)
    {
        Console.WriteLine("{0} : {1} ", dic.Key, dic.Value);
    }
}
```
