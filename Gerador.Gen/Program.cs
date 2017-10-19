using Common.Gen;
using LTM.Teste.Gen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTM.Teste.Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            HelperFlow.Flow(args, null, new HelperSysObjects(new ConfigContext().GetConfigContext()));
        }

    }
}
