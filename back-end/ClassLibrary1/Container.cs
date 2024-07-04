using System;

namespace container
{
    class Container
    {
        public int ID {  get; set; }
        public int priority { get; set; }
        public float tons { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float size { get; set; }

        public Container(int id, int p, float t, float s) { 
            priority = p;
            tons = t;
            ID = id;
            size = s;
        }
    }
}
