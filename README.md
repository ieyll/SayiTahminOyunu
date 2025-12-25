# 🎯 Sayı Tahmin Oyunu (C# WinForms)

Kullanıcı giriş/ kayıt sistemi bulunan, süre ve hak bazlı çalışan, skorları veritabanına kaydeden
çok seviyeli bir **Sayı Tahmin Oyunu**.

Bu proje, **C# WinForms**, **SQL Server** ve temel **OOP prensipleri** kullanılarak geliştirilmiştir.

---

## 🚀 Özellikler

- 👤 Kullanıcı Giriş & Kayıt Sistemi
- 🔢 3 / 4 / 5 haneli sayı seçenekleri
- ⏱️ Süre sınırlı oyun mekanizması
- ❤️ Hak (deneme) sistemi
- ➕ / ➖ Artı – eksi puan mantığı
- 🏆 Skorların veritabanına kaydı
- 📊 Hane sayısına göre **Top 5 Skor Tablosu**
- 🎨 Renkli tahmin geri bildirimi (doğru/yanlış konum)
- 🔐 Şifreler hashlenerek saklanır

---

## 🛠️ Kullanılan Teknolojiler

- **C#**
- **.NET WinForms**
- **SQL Server**
- **ADO.NET**
- **Git & GitHub**
- **Object Oriented Programming (OOP)**

---

## 📂 Proje Yapısı

SayiTahminOyunu
│
├── DatabaseHelper.cs → Veritabanı bağlantısı
├── Hashing.cs → Şifre hashleme
├── KullaniciBilgisi.cs → Aktif kullanıcı bilgileri
│
├── Login.cs → Giriş ekranı
├── Register.cs → Kayıt ekranı
├── Form1.cs → Ana oyun ekranı
│
├── Resources/ → Görseller & ikonlar
└── README.md

---

### 📸 Ekran Görüntüleri

| Giriş Ekranı | Kayıt Ekranı | Oyun Ekranı |
| :---: | :---: | :---: |
| ![Giriş](SayiTahminOyunu/screenshots/login.png) | ![Kayıt](SayiTahminOyunu/screenshots/register.png) | ![Oyun](SayiTahminOyunu/screenshots/game.png) |

## 🎮 Oyun Kuralları

- Bilgisayar, rakamları **birbirinden farklı** gizli bir sayı tutar
- Kullanıcı tahmin yapar:
  - 🟢 **Yeşil** → Rakam doğru & yeri doğru
  - 🟡 **Sarı** → Rakam var ama yeri yanlış
  - 🔴 **Kırmızı** → Rakam yok
- Süre veya hak bittiğinde oyun sona erer
- Doğru tahmin edildiğinde skor hesaplanır ve kaydedilir

---

## 📊 Skor Sistemi

- Skor = **Kalan Hak × 100**
- Süre bilgisi de skorla birlikte kaydedilir
- Aynı hane sayısı için en iyi skorlar listelenir

---

## ⚙️ Kurulum

1. Bu repoyu klonlayın:
   ```bash
   git clone https://github.com/ieyll/Say-TahminOyunu.git
   
2.Visual Studio ile projeyi açın

3.SQL Server’da gerekli tabloları oluşturun

4.DatabaseHelper.cs içindeki connection string’i güncelleyin

5.Projeyi çalıştırın ▶️

📌 Geliştirme Notları

Proje eğitim ve portföy amaçlı geliştirilmiştir

Kod okunabilirliği ve modülerlik ön planda tutulmuştur

İleride:

Leaderboard sayfası

Oyun istatistikleri

Dark Mode

Online çok oyunculu yapı eklenebilir

👩‍💻 Geliştirici

Eylül İlbarış
Bilgisayar Programcılığı Öğrencisi

🔗 GitHub: https://github.com/ieyll
