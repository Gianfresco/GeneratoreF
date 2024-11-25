% Richiede all'utente di inserire i coefficienti
a = input('Inserisci il coefficiente a: ');
b = input('Inserisci il coefficiente b: ');
c = input('Inserisci il coefficiente c: ');

% Calcola il discriminante
D = b^2 - 4*a*c;

% Controlla il discriminante e calcola le radici di conseguenza
if D > 0
    % Due radici reali e distinte
    x1 = (-b + sqrt(D)) / (2*a);
    x2 = (-b - sqrt(D)) / (2*a);
    fprintf('Le radici sono reali e distinte:\n x1 = %.2f\n x2 = %.2f\n', x1, x2);
elseif D == 0
    % Una radice reale ripetuta
    x = -b / (2*a);
    fprintf('La radice è reale e ripetuta:\n x = %.2f\n', x);
else
    % Radici complesse
    parteReale = -b / (2*a);
    parteImmaginaria = sqrt(-D) / (2*a);
    fprintf('Le radici sono complesse:\n x1 = %.2f + %.2fi\n x2 = %.2f - %.2fi\n', parteReale, parteImmaginaria, parteReale, parteImmaginaria);
end