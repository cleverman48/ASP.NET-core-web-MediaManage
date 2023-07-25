using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MediaoManage.Models;

namespace MediaoManage.Data
{
    public class MediaoManageContext : DbContext
    {
        public MediaoManageContext (DbContextOptions<MediaoManageContext> options)
            : base(options)
        {
        }

        public DbSet<MediaoManage.Models.UserMedia> UserMedia { get; set; } = default!;        
    }
}
