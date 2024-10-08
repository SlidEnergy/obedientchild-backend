﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.Domain.LifeEnergy
{
    public class LifeEnergyAccount
    {
        public int Id { get; set; }

        public int Balance { get; set; }

        public LifeEnergyAccount() { }

        public int PowerUp(int amount)
        {
            Balance += amount;
            
            if(Balance < 0)
                Balance = 0;

            return Balance;
        }

        public int PowerDown(int amount)
        {
            Balance -= amount;

            if (Balance < 0)
                Balance = 0;

            return Balance;
        }
    }
}
