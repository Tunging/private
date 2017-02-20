using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityFoundation.State;

namespace UnityFoundation.Sprite
{
    class SpriteFactory
    {
        public BaseSprite CreateSprite(SpriteEnum spriteType)
        {
            BaseSprite sprite = null;
            switch (spriteType)
            {
                case SpriteEnum.Hero:
                    sprite = new HeroSprite();
                    break;
                case SpriteEnum.Monster:
                    sprite = new MonsterSprite();
                    break;
                default:
                    break;
            }

            return sprite;
        }
    }
}
