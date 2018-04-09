using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SacramentMeetingPlanner.Models;

namespace SacramentMeetingPlanner.Models
{
    public class SacramentMeetingPlannerContext : DbContext
    {
        public SacramentMeetingPlannerContext (DbContextOptions<SacramentMeetingPlannerContext> options)
            : base(options)
        {
        }

        public DbSet<SacramentMeetingPlanner.Models.Sacrament> Sacrament { get; set; }

        public DbSet<SacramentMeetingPlanner.Models.Hymns> Hymns { get; set; }

        public DbSet<SacramentMeetingPlanner.Models.Prayers> Prayers { get; set; }

        public DbSet<SacramentMeetingPlanner.Models.Speakers> Speakers { get; set; }
    }
}
