using ElectionCommonLayer.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectionRepositoryLayer.Context
{
   public class AuthenticationContext : IdentityDbContext
    {
        public AuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ApplicationModel> AccountTable { get; set; }

        public DbSet<StateModel> States { get; set; }

        public DbSet<PartyModel> Parties { get; set; }

        public DbSet<ConstituencyModel> Constituencies { get; set; }

        public DbSet<CandidateModel> Candidates { get; set; }

        public DbSet<ResultModel> Result { get; set; }

        public DbSet<PartywiseResultModel> PartywiseResults { get; set; }
    }
}
