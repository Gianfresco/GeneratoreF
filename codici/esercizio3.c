int isPrime(int n) {
    if (n <= 1)
        return 0; // I numeri <= 1 non sono primi
    if (n == 2)
        return 1; // 2 è il solo numero primo pari
    if (n % 2 == 0)
        return 0; // Numeri pari maggiori di 2 non sono primi

    int sqrt_n = (int)sqrt((double)n);
    for (int i = 3; i <= sqrt_n; i += 2) {
        if (n % i == 0)
            return 0; // Trovato un divisore, non è primo
    }
    return 1; // Nessun divisore trovato, è primo
}