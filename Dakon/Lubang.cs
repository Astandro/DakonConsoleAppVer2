using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dakon
{
    class Lubang
    {
        public int index { get; set; }
        public int marblesCount { get; set; }
        public string owner { get; set; }
        public bool isStoreHouse { get; set; }
        public bool isEmpty { get; set; }
        public bool isBurnt { get; set; }

        private static Lubang lubang = null;

        public Lubang(int index, int marblesCount, string owner, bool isStoreHouse, bool isEmpty, bool isBurnt)
        {
            this.index = index;
            this.marblesCount = marblesCount;
            this.owner = owner;
            this.isStoreHouse = isStoreHouse;
            this.isEmpty = isEmpty;
            this.isBurnt = isBurnt;
        }

        public static Lubang GetInstance(int index, int marblesCount, string owner, bool isStoreHouse, bool isEmpty, bool isBurnt)
        {
            if(lubang == null)
            {
                lubang = new Lubang(index, marblesCount, owner, isStoreHouse, isEmpty, isBurnt);
            }
            return lubang;
        }
    }
}
