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
        public string level;
        public int sobuoc;
        public int star;
        public int time;
        public int playertime;
        public int sobuoc_an;
        public string[] buocAo;
    }
}

