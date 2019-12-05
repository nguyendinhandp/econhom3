using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace ecoNhom3.Models
{
    public class MyDbContext: DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions options) : base (options)
        {

        }

       
        
        public virtual DbSet<ChiTietHd> ChiTietHds { get; set; }
        public virtual DbSet<HangHoa> HangHoas { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }   
        public virtual DbSet<ThanhVien> ThanhViens { get; set; }
        public virtual DbSet<Loai> Loais { get; set; }       
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }
        public virtual DbSet<PhanLoaiTV> PhanLoaiTVs { get; set; }
        public virtual DbSet<TrangThai> TrangThais { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("workstation id = econhom3.mssql.somee.com; packet size = 4096; user id = econhom3_SQLLogin_1; pwd = m5deyge5q2; data source = econhom3.mssql.somee.com; persist security info = False; initial catalog = econhom3");

                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=QLNUOC;Integrated Security=True");
            }
        }

        internal Task SignInAsync(string authenticationScheme, ClaimsPrincipal claimsPrincipal)
        {
            throw new NotImplementedException();
        }
    }
}
