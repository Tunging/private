using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityFoundation.Sprite;

namespace UnityFoundation.State
{
    public class FSMBaseState
    {
        public int ID;

        FSM fsm;
        BaseSprite sprite;

        public virtual void Initialize(FSM fsm ,BaseSprite sprite) {
            this.fsm = fsm;
            this.sprite = sprite;
        }

        /// <summary>
        /// 注册该状态时调用该方法
        /// </summary>
        public virtual void OnRegister() { }

        public FSMBaseState(int id)
        {
            this.ID = id;
        }

        public virtual void Enter(System.Object info) { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}
