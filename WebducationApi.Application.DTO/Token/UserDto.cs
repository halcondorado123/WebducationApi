using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebducationApi.Application.DTO.Token
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string UserPasswordHash { get; set; } 
    }
}
