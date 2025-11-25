using Microsoft.AspNetCore.Identity;
using WebBanNuoc.Models;

namespace WebBanNuoc.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed Roles
            string[] roleNames = { "Admin", "User" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@webbannuoc.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Administrator",
                    PhoneNumber = "0123456789",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Cà phê", Description = "Các loại cà phê đặc sản" },
                    new Category { Name = "Trà", Description = "Trà trái cây và trà sữa" },
                    new Category { Name = "Nước ép", Description = "Nước ép trái cây tươi" },
                    new Category { Name = "Sinh tố", Description = "Sinh tố các loại" },
                    new Category { Name = "Bánh ngọt", Description = "Bánh ngọt và snack" }
                };

                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();
            }

            // Seed Products
            if (!context.Products.Any())
            {
                var cafeCategory = context.Categories.First(c => c.Name == "Cà phê");
                var traCategory = context.Categories.First(c => c.Name == "Trà");
                var nuocEpCategory = context.Categories.First(c => c.Name == "Nước ép");

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Cà phê đen đá",
                        Description = "Cà phê đen truyền thống, đậm đà",
                        Price = 25000,
                        CategoryId = cafeCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-coffee.jpg"
                    },
                    new Product
                    {
                        Name = "Cà phê sữa",
                        Description = "Cà phê sữa đá ngọt ngào",
                        Price = 30000,
                        CategoryId = cafeCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-coffee.jpg"
                    },
                    new Product
                    {
                        Name = "Bạc xỉu",
                        Description = "Cà phê sữa nhiều sữa, ít cà phê",
                        Price = 30000,
                        CategoryId = cafeCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-coffee.jpg"
                    },
                    new Product
                    {
                        Name = "Trà đào cam sả",
                        Description = "Trà trái cây thơm mát",
                        Price = 35000,
                        CategoryId = traCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-tea.jpg"
                    },
                    new Product
                    {
                        Name = "Trà sữa trân châu",
                        Description = "Trà sữa trân châu đường đen",
                        Price = 40000,
                        CategoryId = traCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-tea.jpg"
                    },
                    new Product
                    {
                        Name = "Nước ép cam",
                        Description = "Nước ép cam tươi 100%",
                        Price = 35000,
                        CategoryId = nuocEpCategory.Id,
                        IsActive = true,
                        ImageUrl = "/images/products/default-juice.jpg"
                    }
                };

                context.Products.AddRange(products);
                await context.SaveChangesAsync();

                // Seed Product Options
                foreach (var product in products)
                {
                    var options = new List<ProductOption>
                    {
                        new ProductOption { ProductId = product.Id, Group = "Size", Name = "S", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Size", Name = "M", PriceAdjustment = 5000 },
                        new ProductOption { ProductId = product.Id, Group = "Size", Name = "L", PriceAdjustment = 10000 },
                        new ProductOption { ProductId = product.Id, Group = "Đường", Name = "0%", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Đường", Name = "50%", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Đường", Name = "100%", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Đá", Name = "Ít đá", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Đá", Name = "Bình thường", PriceAdjustment = 0 },
                        new ProductOption { ProductId = product.Id, Group = "Đá", Name = "Nhiều đá", PriceAdjustment = 0 }
                    };

                    context.ProductOptions.AddRange(options);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
