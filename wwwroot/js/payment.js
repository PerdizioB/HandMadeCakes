const stripe = Stripe(stripePublicKey);
const elements = stripe.elements();

const card = elements.create("card", {
    hidePostalCode: true
});
card.mount("#card-element");

const form = document.getElementById("payment-form");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    // Dados do pagamento (você pode alterar o valor aqui)
    const paymentData = {
        amount: 1000, // £10 em centavos
        currency: "gbp"
    };

    try {
        // Chama a API para criar o PaymentIntent
        const response = await fetch("/api/payment/pay", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(paymentData)
        });

        const data = await response.json();

        if (data.error) {
            alert(data.error);
            return;
        }

        const clientSecret = data.clientSecret;

        // Confirma o pagamento no Stripe
        const result = await stripe.confirmCardPayment(clientSecret, {
            payment_method: { card }
        });

        if (result.error) {
            alert(result.error.message);
        } else if (result.paymentIntent.status === "succeeded") {
            alert("Pagamento aprovado!");
            window.location.href = "/Checkout/Success"; // ou onde você quiser redirecionar
        }
    } catch (err) {
        alert("Erro no processamento do pagamento: " + err.message);
    }
});
