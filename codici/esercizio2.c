#include <stdio.h>

// Funzione per calcolare il fattoriale
unsigned long long fattoriale(int n) {
    if (n == 0 || n == 1)
        return 1;
    unsigned long long risultato = 1;
    for(int i = 2; i <= n; i++) {
        risultato *= i;
    }
    return risultato;
}

int main() {
    int numero;
    unsigned long long risultato;

    // Richiede all'utente di inserire un numero
    printf("Inserisci un numero intero positivo: ");
    if (scanf("%d", &numero) != 1) {
        printf("Input non valido. Assicurati di inserire un numero intero.\n");
        return 1;
    }

    // Controlla se il numero è negativo
    if (numero < 0) {
        printf("Errore: il fattoriale non è definito per numeri negativi.\n");
        return 1;
    }

    // Calcola il fattoriale
    risultato = fattoriale(numero);

    // Visualizza il risultato
    printf("Il fattoriale di %d è %llu\n", numero, risultato);

    return 0;
}
