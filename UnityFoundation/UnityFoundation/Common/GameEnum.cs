using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityFoundation.State
{
    /// <summary>
    /// 状态机状态
    /// </summary>
    public class StateEnum
    {
        /// <summary>
        /// 没有状态
        /// </summary>
        public const int NONE = 0;
        /// <summary>
        /// 移动 走
        /// </summary>
        public const int MOVE_WALK = 1;
        /// <summary>
        /// 移动 跑
        /// </summary>
        public const int MOVE_RUN = 2;
        /// <summary>
        /// 移动 伤害
        /// </summary>
        public const int HURT = 3;
        /// <summary>
        /// 站立
        /// </summary>
        public const int STAND = 4;
        /// <summary>
        /// 空闲状态
        /// </summary>
        public const int IDLE = 5;
    }

   public enum SpriteEnum
    {
        Hero,
        Monster
    }
}
