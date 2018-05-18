using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstateCRMConsumer.Models
{
    public abstract class IEntity
    {
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
    }
}