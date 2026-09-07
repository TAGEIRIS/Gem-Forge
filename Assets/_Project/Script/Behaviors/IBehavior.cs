using UnityEngine;

namespace GameData
{
    public interface IBehavior
    {
        //初始化调用，只调用一次
        void OnInitialize(GameObject owner);

        // 每帧调用（由 BehaviorContainer 的 Update 驱动）
        void OnUpdate();

        // 容器所在物体发生碰撞时调用
        void OnHit(GameObject target);

        // 策略从容器移除或容器销毁时调用
        void OnDispose();
    }
}