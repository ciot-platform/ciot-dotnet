using CiotNetNS.Shared;
using System.Threading.Tasks;

namespace CiotNetNS.Domain.Interfaces
{
    public interface ISerialScanner
    {
        Task<Result> Scan();
    }
}
