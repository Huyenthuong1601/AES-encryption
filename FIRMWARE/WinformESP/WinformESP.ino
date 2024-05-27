#include "mbedtls/aes.h"

void encrypt(char * plainText, char * key, unsigned char * outputBuffer) {
    mbedtls_aes_context aes;

    mbedtls_aes_init(&aes);
    mbedtls_aes_setkey_enc(&aes, (const unsigned char*) key, strlen(key) * 8);
    mbedtls_aes_crypt_ecb(&aes, MBEDTLS_AES_ENCRYPT, (const unsigned char*)plainText, outputBuffer);
    mbedtls_aes_free(&aes);
}

void decrypt(unsigned char * cipherText, char * key, unsigned char * outputBuffer) {
    mbedtls_aes_context aes;

    mbedtls_aes_init(&aes);
    mbedtls_aes_setkey_dec(&aes, (const unsigned char*) key, strlen(key) * 8);
    mbedtls_aes_crypt_ecb(&aes, MBEDTLS_AES_DECRYPT, (const unsigned char*)cipherText, outputBuffer);
    mbedtls_aes_free(&aes);
}

void setup() {
    Serial.begin(115200);
    while (!Serial) {
        ; 
    }

    
}

void loop() {
    if (Serial.available() > 0) {
        char plainText[17] = {0}; // Buffer for plain text input
        char key[] = "abcdefghijklmnop"; // 16-byte key
        unsigned char cipherTextOutput[16];
        unsigned char decipheredTextOutput[16];

        int length = Serial.readBytes(plainText, 16); // Read up to 16 characters
        plainText[length] = '\0'; // Null-terminate the string

        // Ensure the input length is 16 characters
        if (length != 16) {
            return;
        }

        encrypt(plainText, key, cipherTextOutput);
        decrypt(cipherTextOutput, key, decipheredTextOutput);

       
        for (int i = 0; i < 16; i++) {
            char str[3];
            sprintf(str, "%02x", (int)cipherTextOutput[i]);
            Serial.print(str);
        }
        for (int i = 0; i < 16; i++) {
           
        }
        
    }
}