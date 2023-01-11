using System;
namespace BaseClass
{
    [Serializable]
    public class ListMap
    {
        public Map[] tuantu;
        public Map[] vonglap;
        public Map[] renhanh;
    }
    [Serializable]
    public class Map
    {
        public string Level;
        public int Sobuoc;
        public int Star;
        public int Time;
        public int Sobuoc_an;
        public string[] BuocAo;
    }
}

