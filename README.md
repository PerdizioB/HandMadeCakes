# 🧁 HandMadeCakes

An e-commerce system for selling handmade cakes, built with **ASP.NET Core MVC**, **Entity Framework Core**, and integrated with **Stripe** for online payments.

## 🌟 Features

- User registration and login  
- Listing and viewing cakes  
- Shopping cart with quantity control  
- Order checkout  
- Card payment via Stripe  
- Payment confirmation via Webhook  
- Admin panel (in progress)  

## 🛠️ Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core (Code First)  
- ASP.NET Identity  
- SQL Server / SQLite (configurable)  
- Stripe (payments)  
- Bootstrap 5 (frontend)  

## ⚙️ Installation

1. Clone the repository:

```bash
git clone https://github.com/PerdizioB/HandMadeCakes.git

2. Navigate to the project folder:

```bash
cd HandMadeCakes
```

3. Configure your **Stripe keys** and database connection string in `appsettings.json`:

```json
"Stripe": {
  "SecretKey":  "your_secret_key",
  "PublishableKey":  "your_publishable_key",
  "WebhookSecret": "your_webhook_secret"
}
```

4. Run migrations and update the database:
```bash
dotnet ef database update
```

5. Run the application:
```bash
dotnet run
```

Access it at: [https://localhost:port](https://localhost:port)

---

## 🧪 Testing Payment

1.Run the Stripe CLI:

```bash
stripe listen --forward-to localhost:5001/api/payment/webhook
```

2. Use test card: `4242 4242 4242 4242` (any valid expiration and CVV) 

---

## 📦 Estrutura de Pastas

- `Controllers/` — Control logic (Cart, Checkout, Admin, etc.)
- `Models/` —Domain classes: Cake, Order, User, etc.
- `Views/` — Razor pages
- `Data/` — Database context and seed data
- `Services/` —  Business logic like email sending or payment processing
---

## 📌 Notes

- Stripe webhooks require HTTPS. For development, use **ngrok**:

```bash
ngrok http https://localhost:5001
```

- When running locally via Visual Studio (F5), **webhooks will not work correctly without an external tunnel.**

---

## 👩‍💻 Author
Project created by **Fernanda Bezerra**  
🔗 [linkedin.com/in/fernandabezerra](https://linkedin.com/in/fernandabezerra)
