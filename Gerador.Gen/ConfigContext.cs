using Common.Gen;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTM.Teste.Gen
{
    public class ConfigContext
    {
        #region Config Contexts

        private Context ConfigContextCore()
        {
            return new Context
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["Core"].ConnectionString,

                Namespace = "LTM.Teste",
                ContextName = "Core",
                ShowKeysInFront = true,
                LengthBigField = 250,

                OutputClassDomain = ConfigurationManager.AppSettings[string.Format("outputClassDomain")],
                OutputClassInfra = ConfigurationManager.AppSettings[string.Format("outputClassInfra")],
                OutputClassDto = ConfigurationManager.AppSettings[string.Format("outputClassDto")],
                OutputClassApp = ConfigurationManager.AppSettings[string.Format("outputClassApp")],
                OutputClassApi = ConfigurationManager.AppSettings[string.Format("outputClassApi")],
                OutputClassFilter = ConfigurationManager.AppSettings[string.Format("outputClassFilter")],
                OutputClassSummary = ConfigurationManager.AppSettings[string.Format("outputClassSummary")],
                OutputAngular = ConfigurationManager.AppSettings["OutputAngular"],
                OutputClassSso = ConfigurationManager.AppSettings["OutputClassSso"],

                Arquiteture = ArquitetureType.DDD,
                CamelCasing = true,
                MakeFront = true,

                TableInfo = new UniqueListTableInfo
                {

                   new TableInfo { TableName = "Teste", MakeDomain = true, MakeApp = true, MakeDto = true, MakeCrud = true, MakeApi= true, MakeSummary = true , MakeFront= true},

                }
            };
        }



        public IEnumerable<Context> GetConfigContext()
        {

            return new List<Context>
            {

                ConfigContextCore(),

            };

        }

        #endregion
    }
}
