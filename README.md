# 🎮 Oyun Kayıt Sistemi ve Kaynak Yönetimi

## 📌 Genel Bakış
Oyuna ilk giriş yapıldığında, daha önceden kayıtlı bir karakter olup olmadığı kontrol edilir. Eğer kayıtlı bir karakter yoksa, oyun **default** değerlerle başlatılır. Oyun verileri **yerel olarak bir JSON dosyasında** tutulur ve bu işlemi yönetmek için **Newtonsoft.Json** (https://www.newtonsoft.com/json) paketi kullanılmıştır.

## 💾 Kayıt Sistemi
- **JSON dosyası şifrelenmeden** cihazda tutulur.
- Şifreleme seçeneği kodlanmış olsa da **şu an aktif değildir**.
- Daha büyük veriler gerektiğinde **SQLite gibi bir veritabanı** daha uygun olabilir, ancak:
  - JSON dosyası kullanımı **daha az kompleks**,
  - **Hızlı ve güvenilir**,
  - **Basit bir veri tutma yöntemi** olduğu için tercih edilmiştir.
- Alternatif olarak **BitStream** formatında veri kaydetmek mümkün olsa da, basitlik adına şu an JSON kullanılmaktadır.

## 📝 Oyuncu Yönetimi
- Oyuncu giriş yaptığında, **soldaki input field'da karakterin özellikleri** görüntülenir.
- **"Toggle Player"** butonu, **varsayılan bir başka karaktere** geçiş yapar.
- Yeni karakterle **"Save Game"** yapıldığında, **önceki karakterin yerine kaydedilir** (İki karakter ayrı ayrı kaydedilmez).
- **"Clear Player"** butonu, kayıtlı olan oyuncu profilini siler.

## ⛏️ Kaynak Yönetimi
- **Resourcelar ayrı bir JSON dosyasında** tutulur.
- **Tek bir JSON dosyasında tutulabilirdi**, ancak **kompleksliği azaltmak adına** ayrı dosyalar kullanılmaktadır.
- **Kapsite Yönetimi**:
  - **"+ Capacity"** butonu toplam kapasiteyi **5 birim artırır** (Maksimum: **100**).
  - **"- Capacity"** butonu kapasiteyi **5 birim azaltır**, ancak **mevcut kaynağın altına inmez**.
- **Kaynak üretim süresi**:
  - **Speed** parametresi, kaynağın ne kadar sürede çıkarılacağını belirler.
- **"Reset Resource"** butonu, **toplanan kaynak miktarını sıfırlar** (Test amaçlı kullanılabilir).

## 🕒 Oyun Kapanırken ve Açılırken İşleyiş
- **"Save Game" yapıldığında**, hem **oyuncu** hem de **kaynak bilgileri** **kayıt tarihiyle birlikte** JSON dosyasına kaydedilir.
- **Oyun tekrar açıldığında**:
  - İlk olarak kayıtlı veri var mı kontrol edilir.
  - Eğer kayıtlı veri varsa, **tarih karşılaştırması yapılır**.
  - Oyun kapalıyken geçen süre hesaplanır ve **kaynak miktarı güncellenir**.

## 📌 Sonuç
Bu sistem, **basit ve güvenilir bir kayıt yöntemi** sunarken, **oyun kapanıp açılsa bile kaynak üretiminin devam etmesini sağlar**. JSON ile veri saklamak, hız ve güvenilirlik açısından **pratik bir çözüm** sunmaktadır.

**Not:** Unity'nin tüm dosyaları katıldığında çok büyüdüğü ve GitHub'a atılması zorlaştığı için sadece Assets dosyasını pushladım. Asset dosyasının içindeki unity-package ile boş bir 2D Unity mobile oyun projesine import edilmesi en sağlıklı deneyimi sunacaktır.

