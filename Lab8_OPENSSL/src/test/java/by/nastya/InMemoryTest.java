package by.nastya;

import by.nastya.rsa.Rsa4096;
import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

public class InMemoryTest {

    // openssl genpkey -out private_key.pem -algorithm RSA -pkeyopt rsa_keygen_bits:4096

    // openssl rsa -pubout -outform pem -in private_key.pem -out public_key.pem

    @BeforeEach
    public void setUp() {
    }

    @Test
    public void test_in_memory_encryption_decryption()
    throws Exception
    {
        // Setup
        Rsa4096 rsa = new Rsa4096(
              "./private_key.pem"
            , "./public_key.pem"
        );
        String expected
            = "Septilko Anastasiya";

        // Test
        String encryptedAndEncoded
            = rsa.encryptToBase64(expected);
        String actual
            = rsa.decryptFromBase64(encryptedAndEncoded);

        // Assert
        Assertions.assertEquals(expected, actual);
    }
}
