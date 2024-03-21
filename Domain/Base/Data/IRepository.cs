using Domain.Base.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base.Data
{
    public interface IRepository : IDisposable, IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
