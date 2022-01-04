using System.Collections.Generic;
using System.Linq;

namespace InfraService.Core.Base
{
    public class HttpBaseResult
    {
        public HttpBaseResult()
        {

        }

        public HttpBaseResult(string error)
        {
            Erros = new List<string> { error };
        }

        public HttpBaseResult(List<string> errors)
        {
            Erros = errors;
        }

        public List<string> Erros { get; set; }
        public bool IsValid() => Erros.Any();
    }
}
