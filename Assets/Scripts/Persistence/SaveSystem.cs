using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace MergeStudio.Persistence {
    // AES-CBC + encrypt-then-HMAC. Local key deters casual editing; it is not server authority.
    public sealed class SaveSystem {
        private readonly string _path;
        private readonly byte[] _key;
        public SaveSystem(string directory) {
            Directory.CreateDirectory(directory); _path = Path.Combine(directory, "save.dat");
            string keyPath = Path.Combine(directory, "save.key");
            if (!File.Exists(keyPath)) { var key = new byte[64]; using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(key); File.WriteAllBytes(keyPath, key); }
            _key = File.ReadAllBytes(keyPath); if (_key.Length != 64) throw new InvalidDataException("Invalid local key.");
        }
        public bool HasSave() => File.Exists(_path) || File.Exists(_path + ".bak");
        public void Save(SaveData data) {
            byte[] plain = Encoding.UTF8.GetBytes(Serialization.ToJson(data));
            using (var aes = Aes.Create()) {
                aes.Key = Slice(_key, 0, 32); aes.GenerateIV();
                byte[] cipher; using (var encryptor = aes.CreateEncryptor()) cipher = encryptor.TransformFinalBlock(plain, 0, plain.Length);
                byte[] payload = new byte[16 + cipher.Length]; Buffer.BlockCopy(aes.IV, 0, payload, 0, 16); Buffer.BlockCopy(cipher, 0, payload, 16, cipher.Length);
                byte[] tag; using (var hmac = new HMACSHA256(Slice(_key, 32, 32))) tag = hmac.ComputeHash(payload);
                using (var stream = new FileStream(_path + ".tmp", FileMode.Create, FileAccess.Write, FileShare.None)) { stream.Write(tag, 0, tag.Length); stream.Write(payload, 0, payload.Length); stream.Flush(true); }
                if (File.Exists(_path)) File.Replace(_path + ".tmp", _path, _path + ".bak"); else File.Move(_path + ".tmp", _path);
            }
        }
        public SaveData Load() {
            if (!HasSave()) return new SaveData();
            try { return Read(_path); }
            catch (Exception e) when (e is IOException || e is InvalidDataException || e is CryptographicException || e is ArgumentException) {
                if (File.Exists(_path + ".bak")) return Read(_path + ".bak"); throw;
            }
        }
        private SaveData Read(string path) {
            byte[] bytes = File.ReadAllBytes(path); if (bytes.Length < 64) throw new InvalidDataException("Truncated save.");
            byte[] payload = Slice(bytes, 32, bytes.Length - 32), tag;
            using (var hmac = new HMACSHA256(Slice(_key, 32, 32))) tag = hmac.ComputeHash(payload);
            int diff = 0; for (int i = 0; i < 32; i++) diff |= bytes[i] ^ tag[i];
            if (diff != 0) throw new CryptographicException("Save authentication failed.");
            using (var aes = Aes.Create()) { aes.Key = Slice(_key, 0, 32); aes.IV = Slice(payload, 0, 16); using (var decryptor = aes.CreateDecryptor()) return Serialization.FromJson(Encoding.UTF8.GetString(decryptor.TransformFinalBlock(payload, 16, payload.Length - 16))); }
        }
        public void DeleteSave() { foreach (string suffix in new[] { "", ".bak", ".tmp" }) if (File.Exists(_path + suffix)) File.Delete(_path + suffix); }
        private static byte[] Slice(byte[] source, int offset, int count) { var result = new byte[count]; Buffer.BlockCopy(source, offset, result, 0, count); return result; }
    }
}
