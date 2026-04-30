using Microsoft.EntityFrameworkCore;
using Tempero_do_Dragao.Data;
using Tempero_do_Dragao.Interface;
using Tempero_do_Dragao.Repositories;
using Tempero_do_Dragao.Repository.Tempero_do_Dragao.Repositories;
using Tempero_do_Dragao.Services;
using Tempero_do_Dragao.Repositories.Interfaces;
using static Tempero_do_Dragao.Interface.InterfaceIrepository;

var builder = WebApplication.CreateBuilder(args);

/* =========================================================
   DATABASE - ENTITY FRAMEWORK CORE
========================================================= */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/* =========================================================
   REPOSITORIES - DEPENDENCY INJECTION
========================================================= */
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IDifficultyRepository, DifficultyRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();

/* =========================================================
   SERVICES - DEPENDENCY INJECTION
========================================================= */
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<RatingService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<DifficultyService>();
builder.Services.AddScoped<IngredientService>();
builder.Services.AddScoped<MeasurementService>();

/* =========================================================
   RAZOR PAGESH
========================================================= */
builder.Services.AddRazorPages();

/* =========================================================
   SESSION (✅ TEM QUE VIR ANTES DO BUILD)
========================================================= */
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


// ===============================
// BUILD
// ===============================

var app = builder.Build();


/* =========================================================
   MIDDLEWARE PIPELINE
========================================================= */
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// ✅ sessão ativada aqui
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();