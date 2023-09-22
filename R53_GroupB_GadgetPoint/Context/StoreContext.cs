using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using R53_GroubB_GadgetPoint.Models;
using R53_GroupB_GadgetPoint.Models;

namespace R53_GroupB_GadgetPoint.Context
{
    public class StoreContext : IdentityDbContext<AppUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Requisition> Requisitions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CustomerBasket> CustomerBasket { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CustomerBasket>().HasNoKey();

            //product relation
            modelBuilder.Entity<Product>().HasOne(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasOne(sc => sc.SubCategory).WithMany().HasForeignKey(c => c.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Product>().HasOne(b => b.Brand).WithMany().HasForeignKey(b => b.BrandId).OnDelete(DeleteBehavior.Restrict);


            //requisition relation
            modelBuilder.Entity<Requisition>().HasOne(c => c.Category).WithMany().HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Requisition>().HasOne(sc => sc.SubCategory).WithMany().HasForeignKey(c => c.SubCategoryId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Requisition>().HasOne(b => b.Brand).WithMany().HasForeignKey(b => b.BrandId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Requisition>().HasOne(s => s.Supplier).WithMany().HasForeignKey(s => s.SupplierId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Requisition>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);


            //Inspection relation
            modelBuilder.Entity<Inspection>().HasOne(r => r.Requisition).WithMany().HasForeignKey(p => p.RequistionId).OnDelete(DeleteBehavior.Restrict);


            //Stock relation
            modelBuilder.Entity<Stock>().HasOne(s => s.Supplier).WithMany().HasForeignKey(s => s.SupplierId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Stock>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);

            //return relation
            modelBuilder.Entity<Return>().HasOne(i => i.Invoice).WithMany().HasForeignKey(i => i.InvoiceId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Return>().HasOne(o => o.Order).WithMany().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Restrict);

            //Invoice relation
            modelBuilder.Entity<Invoice>().HasOne(o => o.Order).WithMany().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Restrict);



            //orderdetail relation
            //modelBuilder.Entity<OrderDetail>().HasOne(o => o.Order).WithMany(o => o.OrderDetail).HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Stock>().HasOne(p => p.Product).WithMany().HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);





            modelBuilder.Entity<OrderItem>().OwnsOne(i => i.ItemOrdered, io =>
            {
                io.WithOwner();
            });

            modelBuilder.Entity<OrderItem>().Property(i => i.Price).HasColumnType("decimal(18,2)");


            //order relation
            //modelBuilder.Entity<Order>().HasOne(i => i.Customer).WithMany().HasForeignKey(i => i.CustomerId).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Order>().HasOne(o => o.Payment).WithMany().HasForeignKey(o => o.PaymentId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>().OwnsOne(o => o.ShippingAddress, a =>
            {
                a.WithOwner();
               
            });


            modelBuilder.Entity<Order>().Property(s => s.Status)
                .HasConversion(o => o.ToString(), o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));

            modelBuilder.Entity<Order>().HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<DeliveryMethod>().Property(d => d.Price).HasColumnType("decimal(18,2)");


            //identity
            modelBuilder.Entity<AppUser>().HasOne(a => a.Address).WithOne(a => a.AppUser).HasForeignKey<Address>(a => a.AppUserId);

            //category and sub category





            modelBuilder.Entity<CustomerBasket>()
                 .HasMany(c => c.BasketItem)
                 .WithOne(b => b.CustomerBasket)
                 .HasForeignKey(b => b.CustomerId)
                 .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }

    }

}