using System;
using System.Collections.Generic;
using System.Text;

namespace PicPayChallenge.Core.DTOs
{
    public class AuthRequestDTO
    {
        public string Identifier { get; set; }
        public string Secret { get; set; }
    }
}
