using Medius.Crypto;
using System;
using System.Text;

public class Test {

	public static void Main(string[] args) {
		Console.WriteLine("Running C# Medius Encrypt/Decrypt tests ...");

		Sha1Test();
		Console.WriteLine();

		Ps2Rc4Test();
		Console.WriteLine();
		
	}

	private static void Sha1Test() {
		Header("SHA1 TEST");
		
		byte[] data = new byte[] {
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 1, 1, 1, 1,
			1, 1, 255, 1, 1
		};

		foreach (CipherContext context in (CipherContext[]) Enum.GetValues(typeof(CipherContext))) {
			Print("Hashed CipherContext(" + context + "): " + BitConverter.ToString(SHA1.Hash(data, context)).Replace("-", ""));
		}

	}

	private static void Ps2Rc4Test() {
		Header("PS2 RC4 TEST");
		
		byte[] key = new byte[] {
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42,
			0x42, 0x42, 0x42, 0x42,  0x42, 0x42, 0x42, 0x42
		};
		
		byte[] data = Encoding.UTF8.GetBytes("Hello World!");

		Print("Key: " + BitConverter.ToString(key).Replace("-", ""));
		Print("Data: " + BitConverter.ToString(data).Replace("-", ""));
		
		foreach (CipherContext context in (CipherContext[]) Enum.GetValues(typeof(CipherContext))) {
			PS2_UYA_RC4 rc = new PS2_UYA_RC4(key, context);
			byte[] cipher;
			byte[] hash;
			bool status = rc.Encrypt(data, out cipher, out hash);
			Print("Encrypted status: " + status);
			Print("Encrypted data: " + BitConverter.ToString(cipher).Replace("-", ""));
			Print("Encryption hash: " + BitConverter.ToString(hash).Replace("-", ""));
			byte[] decrypted;
			status = rc.Decrypt(cipher, hash, out decrypted);
			Print("Decrypted status: " + status);
			Print("Decrypted data: " + BitConverter.ToString(decrypted).Replace("-", ""));
			Print("----");
		}

	}


	private static void Header(string s) {
		Print("==== " + s + " ====");
	}

	private static void Print(string s) {
		Console.WriteLine(s);
	}
}