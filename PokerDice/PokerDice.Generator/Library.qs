namespace PokerDice.Generator {
    open Microsoft.Quantum.Intrinsic;
    open Microsoft.Quantum.Measurement;
    open Microsoft.Quantum.Convert;

    operation RollDie() : Int {
        mutable result = 0;

        using (qs = Qubit[3]) {
            mutable valid = false;

            repeat {
                // Losowanie 3 kubitów
                for i in 0..2 {
                    Reset(qs[i]);
                    H(qs[i]);
                }

                // Pomiar
                let bits = [MResetZ(qs[0]), MResetZ(qs[1]), MResetZ(qs[2])];
                let value = ResultArrayAsInt(bits);

                if (value < 6) {
                    set result = value + 1;
                    set valid = true;
                }
            } until (valid)
            fixup { }
        }

        return result;
    }
}
