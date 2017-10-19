using Common.API;
using Common.Domain.Base;

namespace LTM.Teste.CrossCuting
{
    public class ExportExcelCustom<T> : ExportExcel<T>
    {
        public ExportExcelCustom(FilterBase filter) : base(filter)
        {



        }
    }
}
