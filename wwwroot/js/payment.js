const stripe = Stripe(stripePublicKey);
const elements = stripe.elements();

const card = elements.create("card", {
    hidePostalCode: true
});
card.mount("#card-element");

const form = document.getElementById("payment-form");

form.addEventListener("submit", async (e) => {
    e.preventDefault();

    const paymentData = {
        amount: 1000,  // valor em centavos, ajuste para o seu valor real
        currency: "gbp"
    };

    try {
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

        const result = await stripe.confirmCardPayment(clientSecret, {
            payment_method: { card }
        });

        if (result.error) {
            alert(result.error.message);
        } else if (result.paymentIntent.status === "succeeded") {
            alert("Pagamento aprovado!");
            window.location.href = "/Checkout/Confirmation"; // redireciona para a confirmação
        }
    } catch (err) {
        alert("Erro no processamento do pagamento: " + err.message);
    }
});
