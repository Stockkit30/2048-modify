using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Data
{
    public class GameUser
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int BestScore { get; set; }
    }

}

