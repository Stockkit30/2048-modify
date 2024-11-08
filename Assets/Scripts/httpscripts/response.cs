using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class Response
    {

        public string Message { get; set; } = "";
        public List<GameUser>? Content { get; set; }
        public bool IsSuccess { get; set; }
    }
}
