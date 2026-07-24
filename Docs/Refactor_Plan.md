1.整合数据存储容器

 玩家数据分散在 Inventory、DeviceKu、EnemyList 等多个容器中，
 新增数据类型需新建容器类，维护成本高。

创建 PlayerDataManager 单例聚合所有数据。
ScriptableObject 仅作为"单个模板"使用。
实际操作中发现之前代码数据和逻辑耦合到一块了,现在必须先做拆分,新增1.1任务

1.1重构装置代码



2.重做发射系统
由原先的manager实例化宝石，宝石实例化并发射子弹，改为更贴合“**数据（ScriptableObject ID）与实体（Prefab 实例）分离**”的工厂模式，新增子弹工厂并将发射功能转移到aimmanager上去，宝石仅作为数据的载体，所有Prefab的生成必须经过Factory，所有瞄准器的管理必须经过Manager

3.重做地图场景结构
**砍掉“全地图堆叠”**
用场景切分，把每张地图做成独立的Scene，使用 **LoadSceneMode.Additive**（叠加加载），这样你可以只加载当前地图，而玩家和全局管理器常驻。

4.重做城镇面板
1. **将“Manager控制Canvas”改为“UI栈（UI Stack）”**：
    
    - 放弃用一个Manager去开关所有面板。改为一个 **UIManager** 只负责实例化/销毁面板Prefab。
        
    - 面板之间通过 **观察者模式（事件中心 EventCenter）** 通信。比如装备面板发出 `OnEquipChanged` 事件，角色面板监听这个事件自己刷新，完全绕过中间的Manager。


