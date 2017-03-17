using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using CarManager.Core.Domain;

namespace CarManager.Data.Mapping
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            this.HasKey(c => c.ID);
            this.Property(c => c.Name).HasMaxLength(20);
        }
    }
}
