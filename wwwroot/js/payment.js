const stripe = Stripe(stripePublicKey);
const elements = stripe.elements();

const card = elements.create("card", { hidePostalCode: true });
card.mount("#card-element");

const form = document.getElementById("payment-form");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    // Aqui monta o objeto com os dados do pagamento
    const paymentData = {
        amount: 1000, // valor em centavos, ex: £10.00
        currency: "gbp",
        orderId: orderId // variável definida na view
    };

    try {
        // 1. Chama o backend para criar o PaymentIntent (API que  implementou: /api/payment/pay)
        const response = await fetch("https://45c1ce367562.ngrok-free.app/api/payment/pay", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(paymentData),
            credentials: "include"
        });

        const data = await response.json();

        if (data.error) {
            alert(data.error);
            return;
        }

        const clientSecret = data.clientSecret;

        // 2. Usa o clientSecret para confirmar o pagamento com o cartão
        const result = await stripe.confirmCardPayment(clientSecret, {
            payment_method: { card }
        });

        if (result.error) {
            alert(result.error.message);
        } else if (result.paymentIntent.status === "succeeded") {
            alert("Payment approved!");
            window.location.href = "/Checkout/Confirmation";
        }
    } catch (err) {
        alert("Error processing the payment: " + err.message);
    }
});
