using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityFoundation.Sprite;

namespace UnityFoundation.State
{
    public class FSM
    {
        /// <summary>
        /// 已注册的状态
        /// </summary>
        public Dictionary<int, FSMBaseState> States;

        /// <summary>
        /// 当前状态
        /// </summary>
        protected FSMBaseState _curState;
        /// <summary>
        /// 所依附的sprite
        /// </summary>
        public BaseSprite sprite;

        /// <summary>
        /// 往fsm组建中添加状态
        /// </summary>
        /// <param name="state"></param>
        public void Register(FSMBaseState state)
        {
            AddState(state.ID, state);
        }

        /// <summary>
        /// 用于给sprite添加fsm组建时初始化FSM
        /// </summary>
        /// <param name="baseSprite"></param>
        public void SetUp(BaseSprite baseSprite)
        {
            States = new Dictionary<int, FSMBaseState>();
            this.sprite = baseSprite;
        }

        void AddState(int id, FSMBaseState state)
        {
            if (States.ContainsKey(id))
            {
                return;//由于这是在注册状态,所以如果已经注册过,就不再注册
            }
            else
            {
                States.Add(id, state);

                state.Initialize(this, sprite);

                state.OnRegister();
            }
        }

        void RemoveState(int id)
        {
            if (States.ContainsKey(id)) States.Remove(id);
        }


        void SwitchState(int id, System.Object parameter = null)
        {
            if (!IsExistState(id)) throw new UnityException(string.Format("切换状态失败,找不到id={0}的状态", id));
            if (_curState != null) _curState.Exit();

            _curState = States[id];

            if (_curState != null)
            {
                _curState.Enter(parameter);
            }
            else
                throw new UnityException(string.Format("切换状态失败,找不到id={0}的状态", id));
        }

        ///
        bool IsExistState(int stateID) { return States.ContainsKey(stateID); }
    }
}
