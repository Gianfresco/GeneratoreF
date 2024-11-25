import java.util.Scanner;
import java.util.Random;

public class IndovinaIlNumero {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        Random random = new Random();
        
        int numeroSegreto = random.nextInt(100) + 1; // Genera un numero tra 1 e 100
        int tentativi = 0;
        int guess = 0;
        boolean indovinato = false;
        
        System.out.println("Benvenuto al gioco 'Indovina il Numero'!");
        System.out.println("Ho scelto un numero tra 1 e 100. Prova a indovinarlo!");
        
        while (!indovinato) {
            System.out.print("Inserisci il tuo tentativo: ");
            
            // Verifica che l'input sia un numero
            if (scanner.hasNextInt()) {
                guess = scanner.nextInt();
                tentativi++;
                
                if (guess < 1 || guess > 100) {
                    System.out.println("Per favore, inserisci un numero tra 1 e 100.");
                    continue;
                }
                
                if (guess < numeroSegreto) {
                    System.out.println("Troppo basso! Riprova.");
                } else if (guess > numeroSegreto) {
                    System.out.println("Troppo alto! Riprova.");
                } else {
                    indovinato = true;
                    System.out.println("Congratulazioni! Hai indovinato il numero in " + tentativi + " tentativi.");
                }
            } else {
                System.out.println("Input non valido. Inserisci un numero intero.");
                scanner.next(); // Consuma l'input non valido
            }
        }
        
        scanner.close();
    }
}
