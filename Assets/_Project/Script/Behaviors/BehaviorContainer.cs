using System.Collections.Generic;
using UnityEngine;

namespace GameData
{
    /// <summary>
    /// 挂载在需要拥有策略的游戏对象上（宝石、子弹、敌人等）
    /// </summary>
    public class BehaviorContainer : MonoBehaviour
    {
        private List<IBehavior> behaviors = new List<IBehavior>();

        /// <summary>
        /// 批量添加策略
        /// </summary>
        public void AddBehaviors(List<IBehavior> newBehaviors)
        {
            if (newBehaviors == null || newBehaviors.Count == 0) return;

            foreach (var behavior in newBehaviors)
            {
                if (behavior != null && !behaviors.Contains(behavior))
                {
                    behaviors.Add(behavior);
                    behavior.OnInitialize(gameObject);
                }
            }
        }

        /// <summary>
        /// 添加单个策略
        /// </summary>
        public void AddBehavior(IBehavior behavior)
        {
            if (behavior == null || behaviors.Contains(behavior)) return;

            behaviors.Add(behavior);
            behavior.OnInitialize(gameObject);
        }

        /// <summary>
        /// 移除策略
        /// </summary>
        public void RemoveBehavior(IBehavior behavior)
        {
            if (behavior == null || !behaviors.Contains(behavior)) return;

            behavior.OnDispose();
            behaviors.Remove(behavior);
        }

        /// <summary>
        /// 清空所有策略
        /// </summary>
        public void ClearBehaviors()
        {
            foreach (var behavior in behaviors)
            {
                behavior.OnDispose();
            }
            behaviors.Clear();
        }

        /// <summary>
        /// 获取当前所有策略（只读）
        /// </summary>
        public IReadOnlyList<IBehavior> GetBehaviors()
        {
            return behaviors.AsReadOnly();
        }

        /// <summary>
        /// 检查是否包含某类型策略
        /// </summary>
        public bool HasBehavior<T>() where T : IBehavior
        {
            foreach (var behavior in behaviors)
            {
                if (behavior is T) return true;
            }
            return false;
        }

        /// <summary>
        /// 获取第一个匹配类型的策略
        /// </summary>
        public T GetBehavior<T>() where T : IBehavior
        {
            foreach (var behavior in behaviors)
            {
                if (behavior is T result) return result;
            }
            return default;
        }

        /// <summary>
        /// 获取所有匹配类型的策略
        /// </summary>
        public List<T> GetBehaviors<T>() where T : IBehavior
        {
            var results = new List<T>();
            foreach (var behavior in behaviors)
            {
                if (behavior is T result) results.Add(result);
            }
            return results;
        }

        private void Update()
        {
            // 从后往前遍历，防止策略在 OnUpdate 中触发移除导致索引错乱
            for (int i = behaviors.Count - 1; i >= 0; i--)
            {
                if (behaviors[i] != null)
                {
                    behaviors[i].OnUpdate();
                }
                else
                {
                    behaviors.RemoveAt(i);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            for (int i = behaviors.Count - 1; i >= 0; i--)
            {
                if (behaviors[i] != null)
                {
                    behaviors[i].OnHit(other.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            ClearBehaviors();
        }

        /// <summary>
        /// 手动触发碰撞事件（用于非物理碰撞的情况）
        /// </summary>
        public void TriggerHit(GameObject target)
        {
            for (int i = behaviors.Count - 1; i >= 0; i--)
            {
                if (behaviors[i] != null)
                {
                    behaviors[i].OnHit(target);
                }
            }
        }
    }
}