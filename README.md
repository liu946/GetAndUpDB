# ���ݿ���ͬ��

��ǩ�� C# ���ݿ�ͬ�� �̼߳��

---

## Documentation

##### 1. Config

���Խ׶Σ����ó����ڶ��塣

��ȡ�����и���Ȥ���ֶΣ���Щ�ֶλ���Dictionary��ʵ�ֶδ��ݸ��¼����������ֶα������¼�����Dicitionary��["OTHERS"]�ֶ��С����ݿ�����ռ�ô��ֶΡ�

DB_TOOL - 23
``` C#
string insterestingfieldstr = "ID,AUTOID,NAME,AGE,TELE,EMAIL,TIME,DOCTOR_NAME,REGISTER_ID";
```

���÷���������db��Ϊ��Ҫͬ�������ݿ���Ϣ��


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

ʹ���¼��󶨣�����⵽���ݿ����ʱ����ִ�а󶨵�itemchanged�ķ�����

```
 private void Form1_Load(object sender, EventArgs e)
{
    // ���¼�����
    db = new DBListener();
    db.itemchanged += itemChangedHandlerExamplePrint;
    db.itemchanged += db.sendToServer; // ��������
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

������ʹ��
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
