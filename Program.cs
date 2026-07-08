using BlazorChatbot;
using BlazorChatbot.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.Configure<OpenAIOptions>(
    builder.Configuration.GetSection("OpenAI"));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IOpenAIChatService, OpenAIChatService>();
builder.Services.AddScoped<IPersonaService, PersonaService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();