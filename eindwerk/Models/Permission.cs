using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eindwerk.Models
{
    public class Permission
    {
      public  int Id { get; set; }
     public   string Name { get; set; }

     public   bool ViewAccountManagent { get;set; }
      public  bool ViewContentManagent { get; set; }


    }
}
