using Microsoft.EntityFrameworkCore;
using SubManSys.DbContexts;
using SubManSys.Models;
using SubManSys.DTOs;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SubDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/clients/{id}", (int id, SubDbContext db) =>
{
    var client = db.Client.FirstOrDefault(c => c.IdClient == id);
    var subscriptions = new List<SubscriptionDto>();
    if(client == null)
        return Results.NotFound("Client not found");
    
    var sale = db.Sale.FirstOrDefault(c => c.IdClient == id);
    if (sale != null)
    {
        List<Subscription> subscriptionsFromDb = db.Subscription.Where(s => sale.IdSubscription == s.IdSubscription).ToList();
        foreach (var sub in subscriptionsFromDb)
        {
            var payments = db.Payment.Where(p => p.IdClient == id && p.IdSubscription == sub.IdSubscription).ToList();
            subscriptions.Add(new SubscriptionDto()
            {
                IdSubscription = sub.IdSubscription,
                Name = sub.Name,
                TotalPaidAmount = Convert.ToDecimal(payments.Count() * sub.Price)
            });
        }
    }
    
    var result = new
    {
        client.FirstName,
        client.LastName,
        client.Email,
        client.Phone,
        subscriptions
    };
    
    return Results.Ok(result);
});

app.MapPost("/api/payments", (PaymentDto dto, SubDbContext db) =>
{
    var client = db.Client.FirstOrDefault(c => c.IdClient == dto.IdClient);
    if(client == null)
        return Results.NotFound("Client not found");
    
    var subscription = db.Subscription.FirstOrDefault(c => c.IdSubscription == dto.IdSubscription);
    if(subscription == null)
        return Results.NotFound("Subscription not found");
    if (subscription.EndTime < DateTime.Now)
        return Results.Conflict("Subscription has expired");
    var sale = db.Sale.FirstOrDefault(c => c.IdClient == dto.IdClient && c.IdSubscription == subscription.IdSubscription);
    if (sale != null)
    {
        var payments = db.Payment.Where(p => p.IdClient == dto.IdClient && p.IdSubscription == dto.IdSubscription).ToList();
        var nextPaymentDate = sale.CreatedAt;
        while(nextPaymentDate < DateTime.Now)
            nextPaymentDate = nextPaymentDate.AddMonths(subscription.RenewalPeriod);
        foreach (var pay in payments)
        {
            Console.Write(pay.Date+" "+nextPaymentDate.AddMonths(-1*subscription.RenewalPeriod).AddDays(-7));

            if (pay.Date >= nextPaymentDate.AddMonths(-1*subscription.RenewalPeriod).AddDays(-7))
                return Results.Conflict("Payment for this period has been already completed");
        }
    
        var discount = db.Discount.Where(d => d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now).FirstOrDefault(c => c.IdSubscription == subscription.IdSubscription);
        var discountPrice = subscription.Price;
        if(discount != null)
            discountPrice = subscription.Price * discount.Value/100;

        if (dto.Amount != discountPrice)
            return Results.Conflict("Wrong price");

        var nextPaymentId = payments.OrderByDescending(p => p.IdPayment).FirstOrDefault()!.IdPayment + 1;
        if(nextPaymentId == 0)
            nextPaymentId = 1;

        var payment = new Payment
        {
            IdPayment = nextPaymentId,
            Date = DateTime.Now,
            IdClient = dto.IdClient,
            IdSubscription = dto.IdSubscription,
        };

        db.Payment.Add(payment);
        db.SaveChanges();

        var latestPayment = db.Payment.OrderByDescending(p => p.Date).First();

        return Results.Ok(latestPayment.IdPayment);
    }

    return Results.NotFound("Sale not found");
});

app.Run();