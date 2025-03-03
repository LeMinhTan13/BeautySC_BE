using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace BeautySCProject.Data.Entities;

public partial class BeautyscDbContext : DbContext
{
    public BeautyscDbContext()
    {
    }

    public BeautyscDbContext(DbContextOptions<BeautyscDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Function> Functions { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductFunction> ProductFunctions { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }

    public virtual DbSet<ProductSkinType> ProductSkinTypes { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Routine> Routines { get; set; }

    public virtual DbSet<RoutineDetail> RoutineDetails { get; set; }

    public virtual DbSet<RoutineStep> RoutineSteps { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<ShippingPriceTable> ShippingPriceTables { get; set; }

    public virtual DbSet<SkinTest> SkinTests { get; set; }

    public virtual DbSet<SkinType> SkinTypes { get; set; }

    public virtual DbSet<SkinTypeAnswer> SkinTypeAnswers { get; set; }

    public virtual DbSet<SkinTypeQuestion> SkinTypeQuestions { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseMySql(GetConnectionString(), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:BeautySCDbConnection"];

        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("account");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasColumnType("enum('Manager','Staff','Customer')")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.ToTable("brand");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .HasColumnName("brand_name");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customer");

            entity.HasIndex(e => e.AccountId, "account_Id").IsUnique();

            entity.HasIndex(e => e.SkinTypeId, "skin_type_id");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AccountId).HasColumnName("account_Id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.ConfirmedEmail).HasColumnName("confirmed_email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.SkinTypeId).HasColumnName("skin_type_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            entity.HasOne(d => d.Account).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_ibfk_1");

            entity.HasOne(d => d.SkinType).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SkinTypeId)
                .HasConstraintName("customer_ibfk_2");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PRIMARY");

            entity.ToTable("feedback");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_ibfk_1");

            entity.HasOne(d => d.Product).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_ibfk_2");
        });

        modelBuilder.Entity<Function>(entity =>
        {
            entity.HasKey(e => e.FunctionId).HasName("PRIMARY");

            entity.ToTable("function");

            entity.Property(e => e.FunctionId).HasColumnName("function_id");
            entity.Property(e => e.FunctionName)
                .HasMaxLength(50)
                .HasColumnName("function_name");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PRIMARY");

            entity.ToTable("ingredient");

            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.IngredientName)
                .HasMaxLength(255)
                .HasColumnName("ingredient_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.PaymentMethodId, "payment_method_id");

            entity.HasIndex(e => e.VoucherId, "voucher_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderCode)
                .HasMaxLength(100)
                .HasColumnName("order_code");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .HasColumnName("phone_number");
            entity.Property(e => e.ShippingPrice)
                .HasPrecision(10, 2)
                .HasColumnName("shipping_price");
            entity.Property(e => e.Status)
                .HasColumnType("enum('Pending','Confirmed','Shipping','Complete','Cancel','Returned','Denied')")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasColumnName("total_amount");
            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_ibfk_1");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_ibfk_3");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Orders)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("order_ibfk_2");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.ToTable("order_detail");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_detail_ibfk_1");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PRIMARY");

            entity.ToTable("payment_method");

            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentMethodName)
                .HasMaxLength(255)
                .HasColumnName("payment_method_name");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.BrandId, "brand_id");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Discount)
                .HasPrecision(4, 2)
                .HasColumnName("discount");
            entity.Property(e => e.IsRecommended).HasColumnName("is_recommended");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size)
                .HasMaxLength(10)
                .HasColumnName("size");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.Summary)
                .HasColumnType("text")
                .HasColumnName("summary");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_2");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ibfk_1");
        });

        modelBuilder.Entity<ProductFunction>(entity =>
        {
            entity.HasKey(e => e.ProductFunctionId).HasName("PRIMARY");

            entity.ToTable("product_function");

            entity.HasIndex(e => e.FunctionId, "function_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.ProductFunctionId).HasColumnName("product_function_id");
            entity.Property(e => e.FunctionId).HasColumnName("function_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Function).WithMany(p => p.ProductFunctions)
                .HasForeignKey(d => d.FunctionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_function_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductFunctions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_function_ibfk_1");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PRIMARY");

            entity.ToTable("product_image");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.ProductImageId).HasColumnName("product_image_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_image_ibfk_1");
        });

        modelBuilder.Entity<ProductIngredient>(entity =>
        {
            entity.HasKey(e => e.ProductIngredientId).HasName("PRIMARY");

            entity.ToTable("product_ingredient");

            entity.HasIndex(e => e.IngredientId, "ingredient_id");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.ProductIngredientId).HasColumnName("product_ingredient_id");
            entity.Property(e => e.Concentration)
                .HasPrecision(5, 2)
                .HasColumnName("concentration");
            entity.Property(e => e.IngredientId).HasColumnName("ingredient_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ProductIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductIngredients)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_ingredient_ibfk_1");
        });

        modelBuilder.Entity<ProductSkinType>(entity =>
        {
            entity.HasKey(e => e.ProductSkinTypeId).HasName("PRIMARY");

            entity.ToTable("product_skin_type");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.HasIndex(e => e.SkinTypeId, "skin_type_id");

            entity.Property(e => e.ProductSkinTypeId).HasColumnName("product_skin_type_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SkinTypeId).HasColumnName("skin_type_id");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductSkinTypes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_skin_type_ibfk_1");

            entity.HasOne(d => d.SkinType).WithMany(p => p.ProductSkinTypes)
                .HasForeignKey(d => d.SkinTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_skin_type_ibfk_2");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PRIMARY");

            entity.ToTable("refresh_token");

            entity.HasIndex(e => e.AccountId, "account_id");

            entity.Property(e => e.RefreshTokenId).HasColumnName("refresh_token_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");

            entity.HasOne(d => d.Account).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("refresh_token_ibfk_1");
        });

        modelBuilder.Entity<Routine>(entity =>
        {
            entity.HasKey(e => e.RoutineId).HasName("PRIMARY");

            entity.ToTable("routine");

            entity.HasIndex(e => e.SkinTypeId, "skin_type_id");

            entity.Property(e => e.RoutineId).HasColumnName("routine_id");
            entity.Property(e => e.RoutineName)
                .HasMaxLength(50)
                .HasColumnName("routine_name");
            entity.Property(e => e.SkinTypeId).HasColumnName("skin_type_id");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.SkinType).WithMany(p => p.Routines)
                .HasForeignKey(d => d.SkinTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("routine_ibfk_1");
        });

        modelBuilder.Entity<RoutineDetail>(entity =>
        {
            entity.HasKey(e => e.RoutineDetailId).HasName("PRIMARY");

            entity.ToTable("routine_detail");

            entity.HasIndex(e => e.RoutineId, "routine_id");

            entity.Property(e => e.RoutineDetailId).HasColumnName("routine_detail_id");
            entity.Property(e => e.RoutineDetailName)
                .HasMaxLength(50)
                .HasColumnName("routine_detail_name");
            entity.Property(e => e.RoutineId).HasColumnName("routine_id");

            entity.HasOne(d => d.Routine).WithMany(p => p.RoutineDetails)
                .HasForeignKey(d => d.RoutineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("routine_detail_ibfk_1");
        });

        modelBuilder.Entity<RoutineStep>(entity =>
        {
            entity.HasKey(e => e.RoutineStepId).HasName("PRIMARY");

            entity.ToTable("routine_step");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.RoutineDetailId, "routine_detail_id");

            entity.Property(e => e.RoutineStepId).HasColumnName("routine_step_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Instruction)
                .HasColumnType("text")
                .HasColumnName("instruction");
            entity.Property(e => e.RoutineDetailId).HasColumnName("routine_detail_id");
            entity.Property(e => e.Step).HasColumnName("step");

            entity.HasOne(d => d.Category).WithMany(p => p.RoutineSteps)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("routine_step_ibfk_2");

            entity.HasOne(d => d.RoutineDetail).WithMany(p => p.RoutineSteps)
                .HasForeignKey(d => d.RoutineDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("routine_step_ibfk_1");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.ShippingAddressId).HasName("PRIMARY");

            entity.ToTable("shipping_address");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Customer).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shipping_address_ibfk_1");
        });

        modelBuilder.Entity<ShippingPriceTable>(entity =>
        {
            entity.HasKey(e => e.ShippingPriceTableId).HasName("PRIMARY");

            entity.ToTable("shipping_price_table");

            entity.Property(e => e.ShippingPriceTableId).HasColumnName("shipping_price_table_id");
            entity.Property(e => e.FromWeight).HasColumnName("from_weight");
            entity.Property(e => e.InRegion)
                .HasPrecision(10, 2)
                .HasColumnName("in_region");
            entity.Property(e => e.OutRegion)
                .HasPrecision(10, 2)
                .HasColumnName("out_region");
            entity.Property(e => e.Pir)
                .HasPrecision(10, 2)
                .HasColumnName("pir");
            entity.Property(e => e.Por)
                .HasPrecision(10, 2)
                .HasColumnName("por");
            entity.Property(e => e.ToWeight).HasColumnName("to_weight");
        });

        modelBuilder.Entity<SkinTest>(entity =>
        {
            entity.HasKey(e => e.SkinTestId).HasName("PRIMARY");

            entity.ToTable("skin_test");

            entity.Property(e => e.SkinTestId).HasColumnName("skin_test_id");
            entity.Property(e => e.SkinTestName)
                .HasMaxLength(50)
                .HasColumnName("skin_test_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<SkinType>(entity =>
        {
            entity.HasKey(e => e.SkinTypeId).HasName("PRIMARY");

            entity.ToTable("skin_type");

            entity.Property(e => e.SkinTypeId).HasColumnName("skin_type_id");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.SkinTypeName)
                .HasMaxLength(50)
                .HasColumnName("skin_type_name");
        });

        modelBuilder.Entity<SkinTypeAnswer>(entity =>
        {
            entity.HasKey(e => e.SkinTypeAnswerId).HasName("PRIMARY");

            entity.ToTable("skin_type_answer");

            entity.HasIndex(e => e.SkinTypeId, "skin_type_id");

            entity.HasIndex(e => e.SkinTypeQuestionId, "skin_type_question_id");

            entity.Property(e => e.SkinTypeAnswerId).HasColumnName("skin_type_answer_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.SkinTypeId).HasColumnName("skin_type_id");
            entity.Property(e => e.SkinTypeQuestionId).HasColumnName("skin_type_question_id");

            entity.HasOne(d => d.SkinType).WithMany(p => p.SkinTypeAnswers)
                .HasForeignKey(d => d.SkinTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("skin_type_answer_ibfk_2");

            entity.HasOne(d => d.SkinTypeQuestion).WithMany(p => p.SkinTypeAnswers)
                .HasForeignKey(d => d.SkinTypeQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("skin_type_answer_ibfk_1");
        });

        modelBuilder.Entity<SkinTypeQuestion>(entity =>
        {
            entity.HasKey(e => e.SkinTypeQuestionId).HasName("PRIMARY");

            entity.ToTable("skin_type_question");

            entity.HasIndex(e => e.SkinTestId, "skin_test_id");

            entity.Property(e => e.SkinTypeQuestionId).HasColumnName("skin_type_question_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.SkinTestId).HasColumnName("skin_test_id");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.SkinTest).WithMany(p => p.SkinTypeQuestions)
                .HasForeignKey(d => d.SkinTestId)
                .HasConstraintName("skin_type_question_ibfk_1");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PRIMARY");

            entity.ToTable("transaction");

            entity.HasIndex(e => e.OrderId, "order_id");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transaction_ibfk_1");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.HasKey(e => e.VoucherId).HasName("PRIMARY");

            entity.ToTable("voucher");

            entity.Property(e => e.VoucherId).HasColumnName("voucher_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.DiscountAmount)
                .HasPrecision(10, 2)
                .HasColumnName("discount_amount");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.MinimumPurchase)
                .HasPrecision(10, 2)
                .HasColumnName("minimum_purchase");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("status");
            entity.Property(e => e.VoucherCode)
                .HasMaxLength(255)
                .HasColumnName("voucher_code");
            entity.Property(e => e.VoucherName)
                .HasMaxLength(255)
                .HasColumnName("voucher_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
