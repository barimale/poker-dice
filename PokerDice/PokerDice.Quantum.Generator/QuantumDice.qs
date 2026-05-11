namespace PokerDice.Quantum {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Convert;

    operation RollQuantumDie() : Int {
        // 3 qubity → 0..7
        use q0 = Qubit();
        use q1 = Qubit();
        use q2 = Qubit();

        // Hadamard na każdy qubit osobno
        H(q0);
        H(q1);
        H(q2);

        // Pomiar każdego qubitu
        let b0 = M(q0);
        let b1 = M(q1);
        let b2 = M(q2);

        // Reset qubitów
        Reset(q0);
        Reset(q1);
        Reset(q2);

        // Konwersja bitów na liczbę
        let value = ResultArrayAsInt([b0, b1, b2]);

        // Mapowanie 0..7 → 1..6
        return (value % 6) + 1;
    }
}
