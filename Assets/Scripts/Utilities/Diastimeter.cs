using UnityEngine;

namespace Utilities
{
    public class Diastimeter
    {
        public Vector2 begin;

        public void Begin(Vector2 begin)
        {
            this.begin = begin;
        }

        public Vector2 End(Vector2 end)
        {
            return end - begin;
        }
    }
}