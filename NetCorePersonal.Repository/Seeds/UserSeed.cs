using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCorePersonal.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCorePersonal.Repository.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User { 
                    Id = 1, 
                    FullName = "Welat BARAN",
                    Username = "wbaran",
                    Email = "baranvelat021@gmail.com",
                    Password = "123123", 
                    RePassword = "123123",
                    About = "bilgisayar mühendisi" ,
                    Phone = "05393711268",
                }
                );
        }
    }
}
